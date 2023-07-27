using Backend.Application.Interfaces;
using Backend.Application.UseCases.ChangeState;
using Backend.Application.UseCases.CreateItems;
using Backend.Application.UseCases.DeleteItems;
using Backend.Application.UseCases.GetItems;
using Backend.Application.UseCases.UpdateItems;
using Backend.Database.PostgreSQL;
using Backend.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<PgsqlConnection>()
    .Bind(builder.Configuration.GetSection("Config:PgsqlConnection"));

builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();
builder.Services.AddSingleton<ILotRepository, LotRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

builder.Services.AddSingleton<PgsqlHandler>();

builder.Services.AddSingleton<BuyoutLotHandler>();
builder.Services.AddSingleton<ChangeAuctionStatusHandler>();
builder.Services.AddSingleton<ChangeLotStatusHandler>();
builder.Services.AddSingleton<DoBetHandler>();
builder.Services.AddSingleton<SetDateEndAuctionHandler>();
builder.Services.AddSingleton<SetDateStartAuctionHandler>();

builder.Services.AddSingleton<CreateAuctionHandler>();
builder.Services.AddSingleton<CreateLotHandler>();
builder.Services.AddSingleton<CreateUserHandler>();

builder.Services.AddSingleton<DeleteAuctionHandler>();
builder.Services.AddSingleton<DeleteLotHandler>();
builder.Services.AddSingleton<DeleteUserHandler>();

builder.Services.AddSingleton<GetAuctionsHandler>();
builder.Services.AddSingleton<GetAuctionByIdHandler>();
builder.Services.AddSingleton<GetUsersHandler>();
builder.Services.AddSingleton<GetUserByIdHandler>();

builder.Services.AddSingleton<UpdateAuctionHandler>();
builder.Services.AddSingleton<UpdateLotHandler>();
builder.Services.AddSingleton<UpdateUserHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();