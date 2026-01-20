using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient; //Para fazer chamadas HTTP
        public const string BasePath = "api/v1/product"; //Caminho base da API de produtos

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(_httpClient));
        }
        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            var response =  _httpClient.GetAsync(BasePath);
            return await response.Result.ReadContentAs<IEnumerable<ProductModel>>();
        }

        public async Task<ProductModel> FindProductById(long id)
        {
            var response = _httpClient.GetAsync($"{BasePath}/{id}");
            return await response.Result.ReadContentAs<ProductModel>();
        }
        public async Task<ProductModel> CreateProduct(ProductModel model)
        {
            var response = _httpClient.PostAsJson(BasePath, model);
            return await response.Result.ReadContentAs<ProductModel>();
        }
        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var response = _httpClient.PutAsJson(BasePath, model);
            return await response.Result.ReadContentAs<ProductModel>();
        }
        public async Task<bool> DeleteProductById(long id)
        {
            var response = _httpClient.DeleteAsync($"{BasePath}/{id}");
            return await response.Result.ReadContentAs<bool>();
        }

        public async Task<ProductModel> GetProductByParametersAsync(string endpoint, string name)
        {
            var url = $"{BasePath}/{endpoint}/{name}";
            var response = _httpClient.GetAsync(url);
            var result = await response.Result.ReadContentAs<ProductModel>();
            return result;

            //// Adiciona o token de autorização (exemplo JWT)
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //// Faz a requisição GET
            //HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            //if (response.IsSuccessStatusCode)
            //{
            //    // Lê o conteúdo da resposta como string
            //    string jsonResponse = await response.Content.ReadAsStringAsync();

            //    // Desserializa o JSON para um objeto C#
            //    // Usa System.Text.Json (padrão)
            //    var dados = JsonSerializer.Deserialize<ProductModel>(jsonResponse);
            //    return dados;
            //}
            //else
            //{
            //    // Trata erros (404, 500, etc.)
            //    throw new Exception($"Erro ao chamar a API: {response.StatusCode}");
            //}
        }


    }
}
