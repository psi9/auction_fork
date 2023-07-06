namespace Backend.Domain.Enum;

/// <summary>
/// Статус результата запроса
/// </summary>
public enum StatusCode
{
    /// <summary>
    /// Данные получены/добавлены корректно
    /// </summary>
    Ok = 0,

    /// <summary>
    /// Данные не были найдены
    /// </summary>
    DataNotFound = 1,

    /// <summary>
    /// Данные не были добавлены
    /// </summary>
    DataDidNotAdd = 2,

    /// <summary>
    /// Произошла ошибка
    /// </summary>
    Fail = 3,
}