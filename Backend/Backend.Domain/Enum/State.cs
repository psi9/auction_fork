namespace Backend.Domain.Enum;

/// <summary>
/// Статусы состояния Аукциона и Лота
/// </summary>
public enum State
{
    /// <summary>
    /// Лот/Аукцион подготавливается
    /// </summary>
    Awaiting = 0,

    /// <summary>
    /// Лот/Аукцион редактируется
    /// </summary>
    Editing = 1,

    /// <summary>
    /// Лот/Аукцион активен (Ведутся торги)
    /// </summary>
    Running = 2,

    /// <summary>
    /// Лот/Аукцион отменен
    /// </summary>
    Canceled = 3,

    /// <summary>
    /// Лот/Аукцион завершен
    /// </summary>
    Completed = 4,
}