using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.Get(id);
            return Ok(result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAll();
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] ProductDto productDto)
        {
            var result = await _productService.Update(productDto);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ProductDto productDto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var result = await _productService.Add(productDto,userId);
            return Ok(result);
        }

        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImage([FromForm] ProductImageDto productImageDto)
        {             
            var result = await _productService.AddImage(productImageDto);
            return Ok(result);
        }

        [HttpDelete("DeleteImage/{productid}")]
        public async Task<IActionResult> DeleteImage([FromRoute]int productid)
        {
            var result = await _productService.DeleteImage(productid);
            return Ok(result);
        }
    }
}
