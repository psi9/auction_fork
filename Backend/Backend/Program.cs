using Backend.Application.AuctionData.IRepository;
using Backend.Application.AuctionData.UseCases;
using Backend.Application.LotData.IRepository;
using Backend.Application.LotData.UseCases;
using Backend.Application.UserData.IRepository;
using Backend.Application.UserData.UseCases;
using Backend.Database.PostgreSQL;
using Backend.Database.Repositories;
using Backend.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<PgsqlConnection>()
    .Bind(builder.Configuration.GetSection("Config:PgsqlConnection"));

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

builder.Services.AddSingleton<CreateLotHandler>();
builder.Services.AddSingleton<DeleteLotHandler>();
builder.Services.AddSingleton<UpdateLotHandler>();

builder.Services.AddSingleton<SignUpUserHandler>();
builder.Services.AddSingleton<SignInUserHandler>();

builder.Services.AddSingleton<DeleteUserHandler>();
builder.Services.AddSingleton<GetUsersHandler>();
builder.Services.AddSingleton<GetUserByIdHandler>();
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

    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<AuctionHub>("/custom");
app.MapHub<UserHub>("/custom");

app.Run();