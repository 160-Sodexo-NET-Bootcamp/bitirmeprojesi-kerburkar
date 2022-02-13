using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public  class OfferListDto
    {
        public int Id { get; set; }
        public ProductGetDto Product { get; set; }
        public decimal OfferedPrice { get; set; }
 
        public DateTime OfferDate { get; set; }
        public OfferStatus OfferStatus { get; set; }
    }
}
