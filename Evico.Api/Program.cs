using Evico.Api;
using Evico.Api.QueryBuilder;
using Evico.Api.Services;
using Evico.Api.Services.Auth;
using Microsoft.EntityFrameworkCore;

const string allowAnyCorsOrigin = "Allow any";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("Default"),
        b => b.MigrationsAssembly("Evico.Api")));

builder.Services.AddScoped<EventQueryBuilder>();
builder.Services.AddScoped<PlaceQueryBuilder>();
builder.Services.AddScoped<ProfileQueryBuilder>();

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<PlaceService>();

builder.Services.AddScoped<JwtTokensService>();
builder.Services.AddScoped<VkAuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowAnyCorsOrigin,
        policy => policy.WithOrigins("*"));
});
builder.Services.Configure<RouteOptions>(options =>
    options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowAnyCorsOrigin);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();