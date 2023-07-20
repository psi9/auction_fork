using Backend.Application.Repositories;
using Backend.Database.Interfaces;
using Backend.Database.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<PgsqlConnection>()
    .Bind(builder.Configuration.GetSection("Config:PgsqlConnection"));

builder.Services.AddTransient<IAuctionRepository, IAuctionRepository>();
builder.Services.AddTransient<IBetRepository, BetRepository>();
builder.Services.AddTransient<ILotRepository, LotRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

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