using GeeKShooping.Infra;
using GeekShopping.API.Data.Dto;
using GeekShopping.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository 
                                ?? throw new ArgumentNullException(nameof(productRepository));
        }

        /// <summary>
        /// FindAll Produtos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> FindAll()
        {
            var products = await _productRepository.FindAll();
            if (products == null) return NotFound();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> FindById(long id)
        {
            var products = await _productRepository.FindById(id);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());//Diz que esta inserir algo que não existe

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(ProductDto productDto)
        {
            var products = await _productRepository.Create(productDto);

            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());//Diz que esta inserir algo que não existe

          
            return Ok(products);
        }

        /// <summary>
        /// Atualizar um ativo.
        /// </summary>
        /// <param name="ativoView"></param>
        /// <returns>Ativo atualizado com sucesso!</returns>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="200">Ativo atualizado com sucesso!</response>
        /// <param name="ativoView">Dados do cliente a ser atualizado</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult<ProductDto>> Update(ProductDto productDto)
        {
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());//Diz que esta inserir algo que não existe

            var products = await _productRepository.Update(productDto);
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _productRepository.Delete(id);

            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());//Diz que esta inserir algo que não existe

            return Ok(status);
        }

    }
}
