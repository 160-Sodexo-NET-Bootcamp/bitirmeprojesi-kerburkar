using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Status: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //aynı status'de birden fazla ürün olabileceği için liste
        public virtual IList<Product> Products { get; set; }
    }
}
