using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var chave = builder.Configuration["ServiceUrls:ProductAPI"]; // Pega a URL da API de produtos do appsettings.json

//Injetando o serviço de produtos na aplicação
builder.Services
        .AddHttpClient<IProductService, ProductService>(C => 
                        C.BaseAddress = new Uri(chave)); //Adiciona suporte ao HttpClient para fazer chamadas HTTP a camada da api


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
