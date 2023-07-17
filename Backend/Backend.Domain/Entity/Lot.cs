using Backend.Domain.Enum;
using FluentResults;

namespace Backend.Domain.Entity;

/// <summary>
/// Лот
/// </summary>
public class Lot
{
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
    public decimal StartPrice { get; private set; }

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
    private readonly List<Bet> _bets = new List<Bet>();

    public IReadOnlyCollection<Bet> Bets => _bets;

    /// <summary>
    /// Изображения лота
    /// </summary>
    public IReadOnlyCollection<string> Images { get; init; }

    /// <summary>
    /// Статус лота
    /// </summary>
    public State State { get; private set; } = State.Awaiting;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="startPrice">Начальная цена</param>
    /// <param name="betStep">Шаг ставки</param>
    /// <param name="images">Изображения лота</param>
    public Lot(string name, string description, decimal startPrice, decimal betStep, IReadOnlyCollection<string> images)
    {
        Name = name;
        Description = description;
        StartPrice = startPrice;
        BetStep = betStep;
        Images = images;
    }

    /// <summary>
    /// Обновить информацию о лоте
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="startPrice">Начальная цена</param>
    /// <param name="betStep">Шаг ставки</param>
    /// <returns>Успех или неудача</returns>
    public Result UpdateLotInformation(string name, string description, decimal startPrice,
        decimal betStep)
    {
        if (State == State.Completed)
        {
            return Result.Fail("Вы не можете изменить информацию, лот продан");
        }

        Name = name;
        Description = description;
        StartPrice = startPrice;
        BetStep = betStep;

        return Result.Ok();
    }

    /// <summary>
    /// Установить цену выкупа лота
    /// </summary>
    /// <param name="price">Цена выкупа</param>
    /// <returns>Успех или неудача</returns>
    public Result SetBuyoutPrice(decimal price)
    {
        if (State != State.Completed)
        {
            return Result.Fail("Вы не можете установить цену выкупа, лот еще не продан");
        }

        BuyoutPrice = price;

        return Result.Ok();
    }

    /// <summary>
    /// Попытаться сделать ставку
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя</param>
    /// <returns>Успех или неудача</returns>
    public Result TryDoBet(Guid userId)
    {
        if (State == State.Completed)
        {
            return Result.Fail("Лот продан, сделать ставку невозможно");
        }

        var value = _bets.Count > 0
            ? _bets.Max(b => b.Value) + BetStep
            : BetStep;

        var bet = new Bet()
        {
            Value = value,
            LotId = Id,
            UserId = userId,
            DateTime = DateTime.Now
        };

        _bets.Add(bet);

        return Result.Ok();
    }

    /// <summary>
    /// Изменить статус лота
    /// </summary>
    /// <param name="state">Состояние лота</param>
    /// <returns>Успех или неудача</returns>
    public Result ChangeStatus(State state)
    {
        if (State == State.Completed)
        {
            return Result.Fail("Лот продан, изменение статуса невозможно");
        }

        State = state;

        return Result.Ok();
    }
}