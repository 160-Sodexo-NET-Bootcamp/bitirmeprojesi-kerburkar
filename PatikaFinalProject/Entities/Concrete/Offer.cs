using Entities.Abstract;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Offer : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal OfferedPrice { get; set; }
        
        [ForeignKey("User")]
        public int OfferedUserId { get; set; }
        public User User { get; set; }
      
        public DateTime OfferDate { get; set; }
        public OfferStatus OfferStatus { get; set; }
    }

}
