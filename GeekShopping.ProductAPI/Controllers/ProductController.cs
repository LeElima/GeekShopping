using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using GeekShopping.ProductAPI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if(product== null) return NotFound();
            return Ok(product);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindByAll()
        {
            var product = await _productRepository.FindAll();
            return Ok(product);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Create([FromBody]ProductVO productVO)
        {
            if (productVO == null) return BadRequest();
            var product = await _productRepository.Create(productVO);
            return Ok(product);
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductVO>> Update([FromBody] ProductVO productVO)
        {
            if (productVO == null) return BadRequest();
            var product = await _productRepository.Update(productVO);
            return Ok(product);
        }
        [HttpDelete]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _productRepository.Delete(id);
            if(!status) return BadRequest();
            return Ok(status);
        }
    }
}
