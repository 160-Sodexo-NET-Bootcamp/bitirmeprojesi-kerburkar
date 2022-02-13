using AutoMapper;
using Data.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Services.Abstract;
using Services.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class ProductService : IProductService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IResult> Add(ProductDto productDto,string userId)
        {
           
            var product = _mapper.Map<Product>(productDto);
            product.UserId = Convert.ToInt32(userId);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Ürün Başarıyla Eklendi.");
        }

        public async Task<IDataResult<ProductGetDto>> Get(int productId)
        {
            var product = await _unitOfWork.Products.GetAsync(q => q.Id == productId, q => q.Category, q => q.Colour, q => q.Brand, q => q.Status);
            var resultDto = _mapper.Map<ProductGetDto>(product);
            return new SuccessDataResult<ProductGetDto>(resultDto);
        }

        public async Task<IDataResult<IList<ProductDto>>> GetAll()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var resultDto = _mapper.Map<IList<ProductDto>>(products);
            return new SuccessDataResult<IList<ProductDto>>(resultDto);
        }

        public async Task<IResult> Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Ürün Başarıyla Güncellendi.");
        }

        public async Task<IResult> AddImage(ProductImageDto productImageDto)
        {
            var product = await _unitOfWork.Products.GetAsync(q=>q.Id==productImageDto.ProductId);
            if (product == null)
            {
                return new ErrorResult("Ürün Bulunamadı");
            }

            product.ProductPicture = productImageDto.ProductImage.FileName;
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Ürün Resmi Başarıyla Eklendi.");
        }

        public async Task<IResult> DeleteImage(int productId)
        {
            var product = await _unitOfWork.Products.GetAsync(q => q.Id == productId);
            if (product == null)
            {
                return new ErrorResult("Ürün Bulunamadı");
            }

            product.ProductPicture = null;
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Ürün Resmi Başarıyla Silindi.");
        }
    }
}
