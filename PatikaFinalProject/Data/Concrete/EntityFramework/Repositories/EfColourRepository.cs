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
    public class EfColourRepository : EfEntityRepositoryBase<Colour>, IColourRepository
    {
        public EfColourRepository(DbContext context) : base(context)
        {
        }
    }
}
