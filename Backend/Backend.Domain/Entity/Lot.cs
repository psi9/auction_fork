using Backend.Domain.Enum;
using FluentResults;

namespace Backend.Domain.Entity;

/// <summary>
/// Лот
/// </summary>
public class Lot
{
    /// <summary>
    /// Примитив синхронизации потоков
    /// </summary>
    private readonly object _locker = new();

    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Название лота
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Описание лота
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Стартовая цена лота
    /// </summary>
    public decimal StartPrice { get; init; }

    /// <summary>
    /// Цена выкупа лота
    /// </summary>
    public decimal? BuyoutPrice { get; private set; }

    /// <summary>
    /// Шаг ставки лота
    /// </summary>
    public decimal BetStep { get; private set; }

    /// <summary>
    /// Ставки лота
    /// </summary>
    private readonly List<Bet> _bets = new();

    public IReadOnlyCollection<Bet> Bets => _bets;

    /// <summary>
    /// Изображения лота
    /// </summary>
    private readonly List<Image> _images = new();

    public IReadOnlyCollection<Image> Images => _images;

    /// <summary>
    /// Статус лота
    /// </summary>
    public State State { get; private set; } = State.Awaiting;

    /// <summary>
    /// Проверка актуальности лота
    /// </summary>
    public bool IsPurchased => State is (State.Canceled or State.Completed);

    /// <summary>
    /// Проверка редактируемости лота
    /// </summary>
    public bool IsEditable => State is not (State.Running or State.Canceled or State.Completed);

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="startPrice">Начальная цена</param>
    /// <param name="betStep">Шаг ставки лота</param>
    /// <param name="images">Изображения лота</param>
    public Lot(string name, string description, decimal startPrice, decimal betStep, IEnumerable<Image> images)
    {
        Name = name;
        Description = description;
        StartPrice = startPrice;
        BetStep = betStep;

        foreach (var image in images)
        {
            _images.Add(new Image
            {
                Id = image.Id,
                LotId = image.LotId,
                Path = image.Path
            });
        }
    }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="id">Уникальный идентификатор лота</param>
    /// <param name="name">Название лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="startPrice">Начальная цена</param>
    /// <param name="buyoutPrice">Цена выкупа лота</param>
    /// <param name="betStep">Шаг ставки лота</param>
    /// <param name="state">Состояние лота</param>
    public Lot(Guid id, string name, string description, decimal startPrice, decimal buyoutPrice, decimal betStep,
        State state)
    {
        Id = id;
        Name = name;
        Description = description;
        StartPrice = startPrice;
        BuyoutPrice = buyoutPrice;
        BetStep = betStep;
        State = state;
    }

    /// <summary>
    /// Обновить информацию о лоте
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="betStep">Шаг ставки</param>
    /// <param name="images">Изображения лота</param>
    /// <returns>Успех или неудача</returns>
    public Result UpdateInformation(string name, string description, decimal betStep, IEnumerable<Image> images)
    {
        if (!IsEditable)
            return Result.Fail("Вы не можете изменить информацию, лот не редактируем");

        Name = name;
        Description = description;
        BetStep = betStep;

        _images.Clear();

        foreach (var image in images)
        {
            _images.Add(new Image
            {
                Id = image.Id,
                LotId = image.LotId,
                Path = image.Path
            });
        }

        return Result.Ok();
    }

    /// <summary>
    /// Установить цену выкупа лота
    /// </summary>
    /// <returns>Успех или неудача</returns>
    public Result SetBuyoutPrice()
    {
        if (!IsPurchased)
            return Result.Fail("Вы не можете установить цену выкупа, лот не продан");

        BuyoutPrice = _bets.Count > 0 ? _bets.Max()?.Value : 0;

        return Result.Ok();
    }

    /// <summary>
    /// Попытаться сделать ставку
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя</param>
    /// <returns>Успех или неудача</returns>
    public Result TryDoBet(Guid userId)
    {
        if (IsPurchased)
            return Result.Fail("Вы не можете изменить информацию, лот не продан");

        lock (_locker)
        {
            var value = _bets.Count > 0
                ? _bets.Max(b => b.Value) + BetStep
                : BetStep;

            var bet = new Bet
            {
                Value = value,
                LotId = Id,
                UserId = userId,
                DateTime = DateTime.Now
            };

            _bets.Add(bet);
        }

        return Result.Ok();
    }

    /// <summary>
    /// Изменить статус лота
    /// </summary>
    /// <param name="state">Состояние лота</param>
    /// <returns>Успех или неудача</returns>
    public Result ChangeStatus(State state)
    {
        if (!IsEditable)
            return Result.Fail("Вы не можете изменить статус, лот не редактируем");

        State = state;

        return Result.Ok();
    }
}