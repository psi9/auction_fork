using Backend.Domain.Enum;
using Backend.Domain.Response;

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
    public StatusState State { get; private set; } = StatusState.Awaiting;

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
    /// <returns>IBaseResponse - обрабатывает успех или неудачу</returns>
    public IBaseResponse<bool> SetDateStart(DateTime dateStart)
    {
        if (State == StatusState.Completed)
        {
            return new BaseResponse<bool>()
            {
                Data = false,
                Description = "Аукцион завершен, установить дату начала невозможно",
                StatusCode = StatusCode.Fail
            };
        }

        DateStart = dateStart;

        return new BaseResponse<bool>()
        {
            Data = true,
            Description = "Дата начала успешно установлена",
            StatusCode = StatusCode.Ok
        };
    }

    /// <summary>
    /// Установить дату завершения аукциона
    /// </summary>
    /// <param name="dateEnd">Дата завершения</param>
    /// <returns>IBaseResponse - обрабатывает успех или неудачу</returns>
    public IBaseResponse<bool> SetDateEnd(DateTime dateEnd)
    {
        if (State == StatusState.Running)
        {
            return new BaseResponse<bool>()
            {
                Data = false,
                Description = "Аукцион активен, установить дату завершения невозможно",
                StatusCode = StatusCode.Fail
            };
        }

        DateEnd = dateEnd;

        return new BaseResponse<bool>()
        {
            Data = true,
            Description = "Дата завершения успешно установлена",
            StatusCode = StatusCode.Ok
        };
    }

    /// <summary>
    /// Изменить статус аукциона
    /// </summary>
    /// <param name="state">Состояние лота</param>
    /// <returns>IBaseResponse - обрабатывает успех или неудачу</returns>
    public IBaseResponse<bool> ChangeStatus(StatusState state)
    {
        if (State == StatusState.Completed)
        {
            return new BaseResponse<bool>()
            {
                Data = false,
                Description = "Лот продан, изменение статуса невозможно",
                StatusCode = StatusCode.Fail
            };
        }

        State = state;

        return new BaseResponse<bool>()
        {
            Data = true,
            Description = "Статус успешно изменен",
            StatusCode = StatusCode.Ok
        };
    }
}