using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductPicture { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }
        public bool IsOfferable { get; set; }

        //bir ürünün bir kategorisi olabilir, o yüzden FK
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //bir ürünün tek rengi olabilir, o yüzden FK
        public int ColourId { get; set; }
        public Colour Colour { get; set; }

        //bir ürünün status'u tek olabilir, o yüzden FK
        public int StatusId { get; set; }
        public Status Status { get; set; }

        //bir ürünün bir markası olabilir, o yüzden FK
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        //bir ürün bir kullanıcıya ait olabilir, o yüzden FK
        public int UserId { get; set; }
        public User User { get; set; }

        //bir ürüne birden fazla teklif alabilir, o yüzden liste
        public virtual IList<Offer> Offers { get; set; }





    }
}
