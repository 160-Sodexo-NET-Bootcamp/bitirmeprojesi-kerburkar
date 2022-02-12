using Entities.Dtos;
using Services.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryGetDto>> Get(int categoryId);
        Task<IDataResult<IList<CategoryDto>>> GetAll();
        Task<IResult> Add(CategoryDto categoryDto);
        Task<IResult> Update(CategoryDto categoryDto);
    }
}
