using Data.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EntityFramework.Repositories
{
    public class EFOfferRepository : EfEntityRepositoryBase<Offer>, IOfferRepository
    {
        public EFOfferRepository(DbContext context) : base(context)
        {
        }
    }
}
