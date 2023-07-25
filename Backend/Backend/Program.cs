using Backend.Application.Interfaces;
using Backend.Database.PostgreSQL;
using Backend.Database.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<PgsqlConnection>()
    .Bind(builder.Configuration.GetSection("Config:PgsqlConnection"));

builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();
builder.Services.AddSingleton<IBetRepository, BetRepository>();
builder.Services.AddSingleton<ILotRepository, LotRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IImageRepository, ImageRepository>();

builder.Services.AddSingleton<PgsqlHandler>();

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