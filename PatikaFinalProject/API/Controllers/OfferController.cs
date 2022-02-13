using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }


        [HttpPost("giveOffer")]
        public async Task<IActionResult> GiveOffer([FromBody] OfferDto productDto)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var userIntId = Convert.ToInt32(userId);
            var result = await _offerService.GiveOffer(productDto, userIntId);
            return Ok(result);
        }


        [HttpPost("offerableproducts")]
        public async Task<IActionResult> GetOfferableProducts()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var userIntId = Convert.ToInt32(userId);
            var result = await _offerService.GetOfferableProducts(userIntId);
            return Ok(result);
        }

        [HttpPost("canceloffer/{productid}")]
        public async Task<IActionResult> CancelOffer([FromRoute] int productid)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var userIntId = Convert.ToInt32(userId);
            var result = await _offerService.CancelOffer(productid, userIntId);
            return Ok(result);
        }

        [HttpPost("directbuy/{productid}")]
        public async Task<IActionResult> DirectBuy([FromRoute] int productid)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı Bulunamadı");
            }
            var userIntId = Convert.ToInt32(userId);
            var result = await _offerService.DirectBuy(productid, userIntId);
            return Ok(result);
        }

    }
}
