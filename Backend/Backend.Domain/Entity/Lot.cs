using Backend.Domain.Enum;
using Backend.Domain.Response;

namespace Backend.Domain.Entity;

/// <summary>
/// Лот
/// </summary>
public class Lot {
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

    /// <summary>
    /// Изображения лота
    /// </summary>
    public List<byte[]> Images { get; init; }

    /// <summary>
    /// Статус лота
    /// </summary>
    public StatusState State { get; private set; } = StatusState.Awaiting;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="name">Название лота</param>
    /// <param name="description">Описание лота</param>
    /// <param name="startPrice">Начальная цена</param>
    /// <param name="betStep">Шаг ставки</param>
    /// <param name="images">Изображения лота</param>
    public Lot(string name, string description, decimal startPrice, decimal betStep, List<byte[]> images) {
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
    /// <returns>IBaseResponse - обрабатывает успех или неудачу</returns>
    public IBaseResponse<bool> UpdateLotInformation(string name, string description, decimal startPrice,
        decimal betStep) {
        if (State == StatusState.Completed) {
            return new BaseResponse<bool>() {
                Data = false,
                Description = "Вы не можете изменить информацию, лот продан",
                StatusCode = StatusCode.Fail
            };
        }

        Name = name;
        Description = description;
        StartPrice = startPrice;
        BetStep = betStep;

        return new BaseResponse<bool>() {
            Data = true,
            Description = "Лот успешно изменен",
            StatusCode = StatusCode.Ok
        };
    }

    /// <summary>
    /// Установить цену выкупа лота
    /// </summary>
    /// <param name="price">Цена выкупа</param>
    /// <returns>IBaseResponse - обрабатывает успех или неудачу</returns>
    public IBaseResponse<bool> SetBuyoutPrice(decimal price) {
        if (State != StatusState.Completed) {
            return new BaseResponse<bool>() {
                Data = false,
                Description = "Вы не можете установить цену выкупа, лот еще не продан",
                StatusCode = StatusCode.Fail
            };
        }

        BuyoutPrice = price;

        return new BaseResponse<bool>() {
            Data = true,
            Description = "Цена выкупа успешно установлена",
            StatusCode = StatusCode.Ok
        };
    }

    /// <summary>
    /// Попытаться сделать ставку
    /// </summary>
    /// <param name="userId">Уникальный идентификатор пользователя</param>
    /// <returns>IBaseResponse - обрабатывает успех или неудачу</returns>
    public IBaseResponse<bool> TryDoBet(Guid userId) {
        if (State == StatusState.Completed) {
            return new BaseResponse<bool>() {
                Data = false,
                Description = "Лот продан, сделать ставку невозможно",
                StatusCode = StatusCode.Fail
            };
        }

        var value = _bets.Count > 0
            ? _bets.Max(b => b.Value) + BetStep
            : BetStep;

        var bet = new Bet() {
            Value = value,
            LotId = Id,
            UserId = userId,
            DateTime = DateTime.Now
        };

        _bets.Add(bet);

        return new BaseResponse<bool>() {
            Data = true,
            Description = "Ставка успешно сделана",
            StatusCode = StatusCode.Ok
        };
    }

    /// <summary>
    /// Изменить статус лота
    /// </summary>
    /// <param name="state">Состояние лота</param>
    /// <returns>IBaseResponse - обрабатывает успех или неудачу</returns>
    public IBaseResponse<bool> ChangeStatus(StatusState state) {
        if (State == StatusState.Completed) {
            return new BaseResponse<bool>() {
                Data = false,
                Description = "Лот продан, изменение статуса невозможно",
                StatusCode = StatusCode.Fail
            };
        }

        State = state;

        return new BaseResponse<bool>() {
            Data = true,
            Description = "Статус успешно изменен",
            StatusCode = StatusCode.Ok
        };
    }
}