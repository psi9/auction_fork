using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs;

/// <summary>
/// Хаб аукциона для отправки уведомлений
/// </summary>
public class AuctionHub : Hub
{
    /// <summary>
    /// Уведомление о ставке
    /// </summary>
    [HubMethodName("MadeBet")]
    public async Task SendMadeBetNoticeAsync()
        => await Clients.All.SendAsync("MadeBet");

    /// <summary>
    /// Уведомление о создании лота
    /// </summary>
    [HubMethodName("CreatedLot")]
    public async Task SendCreatedLotNoticeAsync()
        => await Clients.All.SendAsync("CreatedLot");

    /// <summary>
    /// Уведомление о смене статуса аукциона
    /// </summary>
    [HubMethodName("ChangedAuctionStatus")]
    public async Task SendChangedAuctionStatusNoticeAsync()
        => await Clients.All.SendAsync("ChangedAuctionStatus");

    /// <summary>
    /// Уведомление о смене статуса лота
    /// </summary>
    [HubMethodName("ChangedLotStatus")]
    public async Task SendChangedLotStatusNoticeAsync()
        => await Clients.All.SendAsync("ChangedLotStatus");

    /// <summary>
    /// Уведомление о продаже лота
    /// </summary>
    [HubMethodName("SoldLot")]
    public async Task SendSoldLotNoticeAsync()
        => await Clients.All.SendAsync("SoldLot");
}