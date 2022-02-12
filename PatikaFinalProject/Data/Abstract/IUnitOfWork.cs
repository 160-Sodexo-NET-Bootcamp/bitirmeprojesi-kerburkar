using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IUnitOfWork
    {
        IBrandRepository Brands { get; }
        ICategoryRepository Categories { get; }
        IColourRepository Colours { get; }
        IProductRepository Products { get; }
        IStatusRepository Statuses   { get; }
        IUserRepository Users { get; }

        //veri tabanındaki bütün save işlemleri için;
        Task<int> SaveAsync();
    }
}
