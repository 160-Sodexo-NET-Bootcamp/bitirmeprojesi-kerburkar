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
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ColourId { get; set; }
        public Colour Colour { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public virtual IList<Offer> Offers { get; set; }





    }
}
