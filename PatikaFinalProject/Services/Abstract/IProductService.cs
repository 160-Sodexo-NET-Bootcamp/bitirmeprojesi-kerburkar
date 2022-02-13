using Entities.Dtos;
using Services.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public  interface IProductService
    {
        Task<IDataResult<ProductGetDto>> Get(int productId);
        Task<IDataResult<IList<ProductDto>>> GetAll();
        Task<IResult> Add(ProductDto productDto, string userId);
        Task<IResult> Update(ProductDto productDto);
        Task<IResult> AddImage(ProductImageDto productImageDto);
        Task<IResult> DeleteImage(int productId);
    }
}
