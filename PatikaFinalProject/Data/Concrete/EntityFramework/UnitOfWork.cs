using Data.Abstract;
using Data.Concrete.EntityFramework.Contexts;
using Data.Concrete.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly EfBrandRepository _brandRepository;
        private readonly EfCategoryRepository _categoryRepository;
        private readonly EfColourRepository _colourRepository;
        private readonly EfProductRepository _productRepository;
        private readonly EfStatusRepository _statusRepository;
        private readonly EfUserRepository _userRepository;
        private readonly EFOfferRepository _offerRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IBrandRepository Brands => _brandRepository ?? new EfBrandRepository(_context);
        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);
        public IColourRepository Colours => _colourRepository ?? new EfColourRepository(_context);
        public IProductRepository Products => _productRepository ?? new EfProductRepository(_context);
        public IStatusRepository Statuses => _statusRepository ?? new EfStatusRepository(_context);
        public IUserRepository Users => _userRepository ?? new EfUserRepository(_context);
        public IOfferRepository Offers => _offerRepository ?? new EFOfferRepository(_context);


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
