using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ProductImageDto
    {
        public int ProductId { get; set; }
        public IFormFile ProductImage { get; set; }
    }
}
