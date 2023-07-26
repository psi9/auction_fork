using Backend.Domain.Enum;
using FluentResults;

namespace Backend.Domain.Entity;

/// <summary>
/// Аукцион
/// </summary>
public class Auction
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Название аукциона
    /// </summary>
    public string? Name { get; private set; }

    /// <summary>
    /// Описание аукциона
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Начало аукциона
    /// </summary>
    public DateTime DateStart { get; private set; }

    /// <summary>
    /// Завершение аукциона
    /// </summary>
    public DateTime DateEnd { get; private set; }

    /// <summary>
    /// Уникальный идентификатор пользователя-создателя
    /// </summary>
    public Guid AuthorId { get; init; }

    /// <summary>
    /// Статус ставки
    /// </summary>
    public State State { get; private set; } = State.Awaiting;

    /// <summary>
    /// Лоты аукциона
    /// </summary>
    private Dictionary<Guid, Lot> Lots { get; } = new();

    /// <summary>
    /// Проверка актуальности аукциона
    /// </summary>
    public bool IsActive => State is not (State.Canceled or State.Completed);

    /// <summary>
    /// Проверка редактируемости аукциона
    /// </summary>
    public bool IsEditable => State is not (State.Running or State.Canceled or State.Completed);

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="name">Название аукциона</param>
    /// <param name="description">Описание аукциона</param>
    /// <param name="authorId">Уникальный идентификатор автора</param>
    public Auction(string? name, string? description, Guid authorId)
    {
        Name = name;
        Description = description;
        AuthorId = authorId;
    }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    /// <param name="name">Название аукциона</param>
    /// <param name="description">Описание аукциона</param>
    /// <param name="dateStart">Дата начала аукицона</param>
    /// <param name="dateEnd">Дата конца аукциона</param>
    /// <param name="authorId">Уникальный идентификатор автора</param>
    /// <param name="state">Текущее состояние аукциона</param>
    public Auction(Guid id, string? name, string? description, DateTime dateStart, DateTime dateEnd, Guid authorId,
        State state)
    {
        Id = id;
        Name = name;
        Description = description;
        DateStart = dateStart;
        DateEnd = dateEnd;
        AuthorId = authorId;
        State = state;
    }

    /// <summary>
    /// Обновить информацию аукциона
    /// </summary>
    /// <param name="name">Название аукциона</param>
    /// <param name="description">Описание аукциона</param>
    /// <returns>Успех или неудача</returns>
    public Result UpdateInformation(string? name, string? description)
    {
        if (!IsEditable)
            return Result.Fail("Вы не можете изменить информацию, аукцион не редактируем");

        Name = name;
        Description = description;

        return Result.Ok();
    }

    /// <summary>
    /// Установить дату начала аукциона
    /// </summary>
    /// <returns>Успех или неудача</returns>
    public Result SetDateStart()
    {
        if (!IsActive)
            return Result.Fail("Аукцион завершен или отменен, установить дату начала нельзя");

        DateStart = DateTime.Now;

        return Result.Ok();
    }

    /// <summary>
    /// Установить дату завершения аукциона
    /// </summary>
    /// <returns>Успех или неудача</returns>
    public Result SetDateEnd()
    {
        if (IsActive)
            return Result.Fail("Аукцион активен, установить дату завершения нельзя");

        DateEnd = DateTime.Now;

        var lotsWithBets = Lots.Values.Where(l => l.Bets.Count > 0).ToArray();
        var maxBetDate = lotsWithBets.SelectMany(l => l.Bets).Max(s => s.DateTime).AddSeconds(30);

        DateEnd = DateEnd >= maxBetDate ? DateEnd : maxBetDate;

        return Result.Ok();
    }

    /// <summary>
    /// Выкупить лот
    /// </summary>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <returns>Успех или неудача</returns>
    public Result BuyoutLot(Guid lotId)
    {
        if (!IsActive)
            return Result.Fail("Аукцион не активен, выкупить лот нельзя");

        return Lots.TryGetValue(lotId, out var lot)
            ? lot.SetBuyoutPrice()
            : Result.Fail("Лот не найден");
    }

    /// <summary>
    /// Изменить статус аукциона
    /// </summary>
    /// <param name="state">Состояние аукциона</param>
    /// <returns>Успех или неудача</returns>
    public Result ChangeStatus(State state)
    {
        if (!IsEditable)
            return Result.Fail("Аукцион не редактируем, изменять статус нельзя");

        State = state;

        return Result.Ok();
    }

    /// <summary>
    /// Изменить статус лота
    /// </summary>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <param name="state">Новое состояние</param>
    /// <returns>Успех или неудача</returns>
    public Result ChangeLotStatus(Guid lotId, State state)
    {
        if (!IsEditable)
            return Result.Fail("Аукцион не редактируем, изменять статус лота нельзя");

        return Lots.TryGetValue(lotId, out var lot)
            ? lot.ChangeStatus(state)
            : Result.Fail("Лот не найден");
    }

    /// <summary>
    /// Сделать ставку
    /// </summary>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <param name="userId">Уникальный идентификатор пользователя</param>
    /// <returns>Успех или неудача</returns>
    public Result DoBet(Guid lotId, Guid userId)
    {
        if (!IsActive)
            return Result.Fail("Аукцион не активен, выкупить лот нельзя");

        return Lots.TryGetValue(lotId, out var lot)
            ? lot.TryDoBet(userId)
            : Result.Fail("Лот не найден");
    }

    /// <summary>
    /// Добавить изображения лота
    /// </summary>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <param name="images">Изображения</param>
    /// <returns>Успех или неудача</returns>
    public Result AddLotImages(Guid lotId, IEnumerable<Image> images)
    {
        if (!IsEditable)
            return Result.Fail("Аукцион не редактируем, изменять изображения лота нельзя");

        return Lots.TryGetValue(lotId, out var lot)
            ? lot.SetImages(images)
            : Result.Fail("Лот не найден");
    }

    /// <summary>
    /// Добавить лот
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="startPrice">Стартовая цена</param>
    /// <param name="betStep">Шаг ставки</param>
    /// <param name="images">Изображения лота</param>
    /// <returns></returns>
    public Result AddLot(string name, string description, decimal startPrice, decimal betStep,
        IEnumerable<Image> images)
    {
        if (!IsActive)
            return Result.Fail("Аукцион не активен, выкупить лот нельзя");

        var lot = new Lot(name, description, startPrice, betStep, images);

        Lots.Add(lot.Id, lot);

        return Result.Ok();
    }

    /// <summary>
    /// Обновить лот
    /// </summary>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <param name="name">Новое название</param>
    /// <param name="description">Новое описание</param>
    /// <param name="betStep">Новый шаг ставки</param>
    /// <param name="images">Изображения лота</param>
    /// <returns>Успех или неудача</returns>
    public Result UpdateLot(Guid lotId, string name, string description, decimal betStep,
        IEnumerable<Image> images)
    {
        if (!IsEditable)
            return Result.Fail("Вы не можете изменить информацию, аукцион не редактируем");

        return Lots.TryGetValue(lotId, out var lot)
            ? lot.UpdateInformation(name, description, betStep, images)
            : Result.Fail("Лот не найден");
    }

    /// <summary>
    /// Удалить лот
    /// </summary>
    /// <param name="lotId">Уникальный идентификатор лота</param>
    /// <returns>Успех или неудача</returns>
    public Result RemoveLot(Guid lotId)
    {
        if (!IsActive)
            return Result.Fail("Аукцион не активен, выкупить лот нельзя");

        if (!Lots.ContainsKey(lotId))
            return Result.Fail("Лот не найден");

        Lots.Remove(lotId);

        return Result.Ok();
    }
}