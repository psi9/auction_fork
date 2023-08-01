using Backend.Application;
using Backend.Application.LotData.Dto;
using Backend.Application.LotData.UseCases;
using Backend.Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

/// <summary>
/// Контроллер лота
/// </summary>
[ApiController]
[Route("api/lot/")]
public class LotController : ControllerBase
{
    /// <summary>
    /// Обработчик удаления лота
    /// </summary>
    private readonly DeleteLotHandler _deleteHandler;

    /// <summary>
    /// Обработчик создания лота
    /// </summary>
    private readonly CreateLotHandler _createHandler;

    /// <summary>
    /// Обработчик выкупа лота
    /// </summary>
    private readonly BuyoutLotHandler _buyoutHandler;

    /// <summary>
    /// Обработчик изменения статуса лота
    /// </summary>
    private readonly ChangeLotStatusHandler _changeStatusHandler;

    /// <summary>
    /// Обработчик ставки
    /// </summary>
    private readonly DoBetHandler _doBetHandler;

    /// <summary>
    /// Обработчик обновления лота
    /// </summary>
    private readonly UpdateLotHandler _updateHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="deleteHandler">Обработчик удаления лота</param>
    /// <param name="createHandler">Обработчик создания лота</param>
    /// <param name="buyoutHandler">Обработчик выкупа лота</param>
    /// <param name="changeStatusHandler">Обработчик изменения статуса лота</param>
    /// <param name="doBetHandler">Обработчик ставки</param>
    /// <param name="updateHandler">Обработчик обновления лота</param>
    public LotController(DeleteLotHandler deleteHandler, CreateLotHandler createHandler, BuyoutLotHandler buyoutHandler,
        ChangeLotStatusHandler changeStatusHandler, DoBetHandler doBetHandler, UpdateLotHandler updateHandler)
    {
        _deleteHandler = deleteHandler;
        _createHandler = createHandler;
        _buyoutHandler = buyoutHandler;
        _changeStatusHandler = changeStatusHandler;
        _doBetHandler = doBetHandler;
        _updateHandler = updateHandler;
    }

    /// <summary>
    /// Запрос на удаление лота
    /// </summary>
    /// <param name="id">Уникальный индентификатор аукциона</param>
    [HttpDelete("delete/{id:guid}/")]
    public async Task DeleteLotAsync(Guid id)
    {
        await _deleteHandler.DeleteLotAsync(id);
    }

    /// <summary>
    /// Запрос на создание лота
    /// </summary>
    /// <param name="lot">Лот</param>
    [HttpPost("create/")]
    public async Task CreateLotASync([FromBody] LotDto lot)
    {
        await _createHandler.CreateLotAsync(lot);
    }

    /// <summary>
    /// Запрос на выкуп лота
    /// </summary>
    /// <param name="auctionId">Уникальный индентификатор аукциона</param>
    /// <param name="lotId">Уникальный индентификатор лота</param>
    [HttpPut("buyout/{auctionId:guid}/{lotId:guid}/")]
    public async Task BuyoutLotAsync(Guid auctionId, Guid lotId)
    {
        await _buyoutHandler.BuyoutLotAsync(auctionId, lotId);
    }

    /// <summary>
    /// Запрос на изменение статуса лота
    /// </summary>
    /// <param name="auctionId">Уникальный индентификатор аукциона</param>
    /// <param name="lotId">Уникальный индентификатор лота</param>
    /// <param name="state">Новое состояние</param>
    [HttpPut("change_status/{auctionId:guid}/{lotId:guid}/{state:int}/")]
    public async Task ChangeLotStatusAsync(Guid auctionId, Guid lotId, State state)
    {
        await _changeStatusHandler.ChangeLotStatusAsync(auctionId, lotId, state);
    }

    /// <summary>
    /// Запрос на ставку
    /// </summary>
    /// <param name="auctionId">Уникальный индентификатор аукциона</param>
    /// <param name="lotId">Уникальный индентификатор лота</param>
    /// <param name="userId">Уникальный индентификатор пользователя</param>
    [HttpPut("do_bet/{auctionId:guid}/{lotId:guid}/{userId:guid}/")]
    public async Task DoBetAsync(Guid auctionId, Guid lotId, Guid userId)
    {
        await _doBetHandler.DoBetAsync(auctionId, lotId, userId);
    }

    /// <summary>
    ///  Запрос на обновление лота
    /// </summary>
    /// <param name="lot"></param>
    [HttpPut("update/")]
    public async Task UpdateLotAsync([FromBody] LotDto lot)
    {
        await _updateHandler.UpdateLotAsync(lot);
    }
}