using Backend.Application.LotData.IRepository;
using Backend.Domain.Entity;
using Backend.Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Backend.Application.LotData.UseCases;

/// <summary>
/// Создание лота
/// </summary>
public class CreateLotHandler
{
    /// <summary>
    /// Репозиторий лота
    /// </summary>
    private readonly ILotRepository _lotRepository;

    /// <summary>
    /// Обработчик уведомлений
    /// </summary>
    private readonly INotificationHandler _notificationHandler;

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="lotRepository">Репозиторий лота</param>
    /// <param name="notificationHandler">Обработчик уведомлений</param>
    public CreateLotHandler(ILotRepository lotRepository, INotificationHandler notificationHandler)
    {
        _lotRepository = lotRepository;
        _notificationHandler = notificationHandler;
    }

    /// <summary>
    /// Создать лот
    /// </summary>
    /// <param name="formCollection">Изображения лота</param>
    public async Task CreateLotAsync(IFormCollection formCollection)
    {
        var lot = new Lot(
            Guid.NewGuid(),
            formCollection["name"],
            formCollection["description"],
            Guid.Parse(formCollection["auctionId"]),
            decimal.Parse(formCollection["startPrice"]),
            0,
            decimal.Parse(formCollection["betStep"]),
            State.Awaiting);

        var imageFiles = formCollection.Files.GetFiles("images");
        var images = new List<Image>();
        var number = 0;
        var imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "Backend.Images", lot.Name);

        foreach (var image in imageFiles)
        {
            if (image.Length <= 0) return;

            var fileName = $"{number}{Path.GetExtension(image.FileName)}";
            var absolutePath = Path.Combine(imagesDirectory, fileName);

            if (!Directory.Exists(imagesDirectory)) Directory.CreateDirectory(imagesDirectory);

            await using var stream = new FileStream(absolutePath, FileMode.Create);
            await image.CopyToAsync(stream);

            number++;

            images.Add(new Image
            {
                Id = Guid.NewGuid(),
                LotId = lot.Id,
                Path = absolutePath
            });
        }

        lot.SetImages(images);
        await _lotRepository.CreateAsync(lot);

        await _notificationHandler.CreatedLotNoticeAsync();
    }
}