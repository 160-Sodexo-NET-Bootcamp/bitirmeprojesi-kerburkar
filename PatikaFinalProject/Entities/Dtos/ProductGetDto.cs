using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public  class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductPicture { get; set; }
        public decimal Price { get; set; }
        public decimal OfferedPrice { get; set; }
        public bool IsSold { get; set; }
        public bool IsOfferable { get; set; }
        public CategoryDto Category { get; set; }
        public ColourDto Colour { get; set; }
        public StatusDto Status { get; set; }
        public BrandDto Brand { get; set; }

    }
}
