using Backend.Application.AuctionData.Dto;
using Backend.Application.AuctionData.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

/// <summary>
/// Контроллер аукциона
/// </summary>
[Authorize]
[ApiController]
[Route("api/auction/")]
public class AuctionController : ControllerBase
{
    /// <summary>
    /// Обработчик удаления
    /// </summary>
    private readonly DeleteAuctionHandler _deleteHandler;

    /// <summary>
    /// Обработчик получения аукциона по уникальному идентификатору
    /// </summary>
    private readonly GetAuctionByIdHandler _getByIdHandler;

    /// <summary>
    /// Обработчик получения списка аукционов
    /// </summary>
    private readonly GetAuctionsHandler _getHandler;

    /// <summary>
    /// Обработчик создания аукциона
    /// </summary>
    private readonly CreateAuctionHandler _createHandler;

    /// <summary>
    /// Обработчик установки даты конца
    /// </summary>
    private readonly SetDateEndAuctionHandler _setDateEndHandler;

    /// <summary>
    /// Обработчик установки даты начала
    /// </summary>
    private readonly SetDateStartAuctionHandler _setDateStartHandler;

    /// <summary>
    /// Обработчик обновления аукциона
    /// </summary>
    private readonly UpdateAuctionHandler _updateHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="deleteHandler">Обработчик удаления</param>
    /// <param name="getByIdHandler">Обработчик получения аукциона по уникальному идентификатору</param>
    /// <param name="getHandler">Обработчик получения списка аукционов</param>
    /// <param name="createHandler">Обработчик создания аукциона</param>
    /// <param name="setDateEndHandler">Обработчик установки даты конца</param>
    /// <param name="setDateStartHandler">Обработчик установки даты начала</param>
    /// <param name="updateHandler">Обработчик обновления аукциона</param>
    public AuctionController(DeleteAuctionHandler deleteHandler, GetAuctionByIdHandler getByIdHandler,
        GetAuctionsHandler getHandler, CreateAuctionHandler createHandler, SetDateEndAuctionHandler setDateEndHandler,
        SetDateStartAuctionHandler setDateStartHandler, UpdateAuctionHandler updateHandler)
    {
        _deleteHandler = deleteHandler;
        _getByIdHandler = getByIdHandler;
        _getHandler = getHandler;
        _createHandler = createHandler;
        _setDateEndHandler = setDateEndHandler;
        _setDateStartHandler = setDateStartHandler;
        _updateHandler = updateHandler;
    }

    /// <summary>
    /// Запрос на удаление аукциона
    /// </summary>
    /// <param name="id">Уникальный идентификатор аукциона</param>
    [HttpDelete("delete/{id:guid}/")]
    public async Task DeleteAuctionAsync(Guid id)
    {
        await _deleteHandler.DeleteAuctionAsync(id);
    }

    /// <summary>
    /// Запрос на получение аукциона по уникальному идентификатору
    /// </summary>
    /// <param name="id">Уникальный индентификатор аукциона</param>
    [HttpGet("get_by_id/{id:guid}/")]
    public async Task<AuctionDto> GetAuctionByIdAsync(Guid id)
    {
        return await _getByIdHandler.GetAuctionByIdAsync(id);
    }

    /// <summary>
    /// Запрос на получение списка аукционов
    /// </summary>
    /// <returns>Список аукционов</returns>
    [HttpGet("get_list/")]
    public async Task<IEnumerable<AuctionDto>> GetAuctionsAsync()
    {
        return await _getHandler.GetAuctions();
    }

    /// <summary>
    /// Запрос на создание аукциона
    /// </summary>
    /// <param name="entity">Аукцион</param>
    [HttpPost("create/")]
    public async Task CreateAuctionAsync([FromBody] AuctionDto entity)
    {
        await _createHandler.CreateAuctionAsync(entity);
    }

    /// <summary>
    /// Запрос на установку даты завершения аукциона
    /// </summary>
    /// <param name="id">Уникальный индентификатор аукциона</param>
    [HttpPut("date_end/{id:guid}/")]
    public async Task SetDateEndAsync(Guid id)
    {
        await _setDateEndHandler.SetDateEndAuctionAsync(id);
    }

    /// <summary>
    /// Запрос на установку даты начала аукциона
    /// </summary>
    /// <param name="id">Уникальный индентификатор аукциона</param>
    [HttpPut("date_start/{id:guid}/")]
    public async Task SetDateStartAsync(Guid id)
    {
        await _setDateStartHandler.SetDateStartAuctionAsync(id);
    }

    /// <summary>
    /// Запрос на обновление аукциона
    /// </summary>
    /// <param name="entity">Аукцион</param>
    [HttpPut("update/")]
    public async Task UpdateAsync([FromBody] AuctionDto entity)
    {
        await _updateHandler.UpdateAuctionAsync(entity);
    }
}