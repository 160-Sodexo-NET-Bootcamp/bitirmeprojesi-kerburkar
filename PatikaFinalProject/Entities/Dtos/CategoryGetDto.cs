using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //kategoriyi getirirken o kategoriye ait ürünleri de getirmesi için;
        public virtual IList<Product> Products { get; set; }
    }
}
