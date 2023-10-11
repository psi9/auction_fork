using Backend.Application;
using Backend.Application.AuctionData.IRepository;
using Backend.Application.AuctionData.UseCases;
using Backend.Application.LotData.IRepository;
using Backend.Application.LotData.UseCases;
using Backend.Application.UserData.IRepository;
using Backend.Application.UserData.UseCases;
using Backend.Controllers;
using Backend.Database.PostgreSQL;
using Backend.Database.Repositories;
using Backend.Hubs;
using Backend.Notifications;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<PgsqlConnection>()
    .Bind(builder.Configuration.GetSection("Config:PgsqlConnection"));

builder.Services.AddOptions<AuthorityHandler>()
    .Bind(builder.Configuration.GetSection("Config:AuthorityHandler"));

builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();
builder.Services.AddSingleton<ILotRepository, LotRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<PgsqlHandler>();

builder.Services.AddSingleton<ChangeAuctionStatusHandler>();
builder.Services.AddSingleton<SetDateEndAuctionHandler>();
builder.Services.AddSingleton<SetDateStartAuctionHandler>();

builder.Services.AddSingleton<CreateAuctionHandler>();
builder.Services.AddSingleton<DeleteAuctionHandler>();
builder.Services.AddSingleton<GetAuctionsHandler>();
builder.Services.AddSingleton<GetAuctionByIdHandler>();
builder.Services.AddSingleton<UpdateAuctionHandler>();

builder.Services.AddSingleton<BuyoutLotHandler>();
builder.Services.AddSingleton<ChangeLotStatusHandler>();
builder.Services.AddSingleton<DoBetHandler>();
builder.Services.AddSingleton<GetLotsHandler>();

builder.Services.AddSingleton<CreateLotHandler>();
builder.Services.AddSingleton<DeleteLotHandler>();
builder.Services.AddSingleton<UpdateLotHandler>();
builder.Services.AddSingleton<GetLotsByAuctionHandler>();

builder.Services.AddSingleton<SignUpUserHandler>();
builder.Services.AddSingleton<SignInUserHandler>();
builder.Services.AddSingleton<AuthorityHandler>();

builder.Services.AddSingleton<DeleteUserHandler>();
builder.Services.AddSingleton<GetUsersHandler>();
builder.Services.AddSingleton<GetUserByIdHandler>();
builder.Services.AddSingleton<UpdateUserHandler>();

builder.Services.AddSingleton<INotificationHandler, NotificationHandler>();
builder.Services.AddSingleton<AuctionController>();
builder.Services.AddSingleton<LotController>();
builder.Services.AddSingleton<UserController>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials());
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<AuctionHub>("/auction");

app.Run();