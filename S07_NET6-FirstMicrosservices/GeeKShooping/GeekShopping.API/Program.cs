using AutoMapper;
using GeekShopping.API.Config;
using GeekShopping.API.Model.Context;
using GeekShopping.API.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//var connection = builder.Configuration.GetConnectionString("MySQLConnectionString");
//builder.Services.AddDbContext<MySQLContext>(options =>
//    options.UseMySql(connection, ServerVersion.AutoDetect(connection)));


var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options =>
    options.
    UseMySql(connection, new MySqlServerVersion(new Version(9,0,0))));


//configure automapper
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GeekShopping Product API",
        Version = "v1"
    });
});

//confgure repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//configure service


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => 
                   options.OpenApiVersion =
                    OpenApiSpecVersion.OpenApi3_1);

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
