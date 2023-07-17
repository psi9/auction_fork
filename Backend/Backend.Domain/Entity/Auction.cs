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
    public string Name { get; private set; }

    /// <summary>
    /// Описание аукциона
    /// </summary>
    public string Description { get; private set; }

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
    /// .ctor
    /// </summary>
    /// <param name="name">Название аукциона</param>
    /// <param name="description">Описание аукциона</param>
    /// <param name="authorId">Уникальный идентификатор автора</param>
    public Auction(string name, string description, Guid authorId)
    {
        Name = name;
        Description = description;
        AuthorId = authorId;
    }

    /// <summary>
    /// Установить дату начала аукциона
    /// </summary>
    /// <param name="dateStart">Дата начала</param>
    /// <returns>Успех или неудача</returns>
    public Result SetDateStart(DateTime dateStart)
    {
        if (State is State.Completed or State.Canceled)
        {
            return Result.Fail("Аукцион завершен ли отменен, установить дату начала невозможно");
        }

        DateStart = dateStart;

        return Result.Ok();
    }

    /// <summary>
    /// Установить дату завершения аукциона
    /// </summary>
    /// <param name="dateEnd">Дата завершения</param>
    /// <returns>Успех или неудача</returns>
    public Result SetDateEnd(DateTime dateEnd)
    {
        if (State is State.Running)
        {
            return Result.Fail("Аукцион активен, установить дату завершения невозможно");
        }

        DateEnd = dateEnd;

        return Result.Ok();
    }

    /// <summary>
    /// Изменить статус аукциона
    /// </summary>
    /// <param name="state">Состояние аукциона</param>
    /// <returns>Успех или неудача</returns>
    public Result ChangeStatus(State state)
    {
        if (State is State.Completed)
        {
            return Result.Fail("Аукцион окончен, изменение статуса невозможно");
        }

        State = state;

        return Result.Ok();
    }
}