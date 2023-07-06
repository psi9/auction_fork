using Backend.Domain.Enum;

namespace Backend.Domain.Response;

/// <summary>
/// Класс-оболочка результата запросов
/// </summary>
/// <typeparam name="T">Передаваемый тип данных</typeparam>
public class BaseResponse<T> : IBaseResponse<T>
{
    /// <summary>
    /// Данные запроса
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Описание результата
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Статус успешности выполнения запроса
    /// </summary>
    public StatusCode StatusCode { get; set; }
}

/// <summary>
/// Интерфейс класса-оболочки результата запросов
/// Будет возвращать данные запроса, описание результата и статус успешности
/// </summary>
/// <typeparam name="T">Передаваемый тип данных</typeparam>
public interface IBaseResponse<T>
{
    /// <summary>
    /// Данные запроса
    /// </summary>
    T? Data { get; set; }

    /// <summary>
    /// Описание результата
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Статус успешности выполнения запроса
    /// </summary>
    public StatusCode StatusCode { get; set; }
}