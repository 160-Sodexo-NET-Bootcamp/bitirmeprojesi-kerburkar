using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }    
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int ColourId { get; set; }
        public int StatusId { get; set; }
        public int BrandId { get; set; }
    }
}
