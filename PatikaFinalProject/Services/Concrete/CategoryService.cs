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
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Kategori Başarıyla Eklendi.");
        }

        public async Task<IDataResult<CategoryGetDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(q=>q.Id == categoryId,q=>q.Products);
            var resultDto = _mapper.Map<CategoryGetDto>(category);
            return new SuccessDataResult<CategoryGetDto>(resultDto);
        }

        public async Task<IDataResult<IList<CategoryDto>>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var resultDto = _mapper.Map<IList<CategoryDto>>(categories);
            return new SuccessDataResult<IList<CategoryDto>>(resultDto);
        }

        public async Task<IResult> Update(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Kategori Başarıyla Güncellendi.");
        }
    }
}
