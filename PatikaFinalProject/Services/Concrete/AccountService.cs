using AutoMapper;
using Data.Abstract;
using Entities.Dtos;
using Entities.Enums;
using Services.Abstract;
using Services.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //teklifin durumunu (onayla/reddet) değiştirmek için;
        public async Task<IResult> ChangeOfferStatus(int offerId, int statusId)
        {
            var offer = await _unitOfWork.Offers.GetAsync(q => q.Id == offerId);
            if (offer== null)
            {
                return new ErrorResult("Teklif Bulunamadı.");
            }
            offer.OfferStatus = (OfferStatus)statusId;
            await _unitOfWork.Offers.UpdateAsync(offer);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Teklif Durumu Başarıyla Güncellendi.");
        }

        //satın alma için;
        public async Task<IResult> BuyProduct(int offerId, int userId)
        {
            var offer = await _unitOfWork.Offers.GetAsync(q => q.Id == offerId);
            if (offer == null)
            {
                return new ErrorResult("Teklif Bulunamadı.");
            }
            if (offer.OfferedUserId != userId)
            {
                return new ErrorResult("Teklif Size Ait Değil.");
            }
            var product = await _unitOfWork.Products.GetAsync(q => q.Id == offer.ProductId);
            offer.OfferStatus = OfferStatus.Sold;          
            await _unitOfWork.Offers.UpdateAsync(offer);
         
            product.IsSold = true;
            await _unitOfWork.Products.UpdateAsync(product);
           
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Başarıyla Satın Alındı.");
        }

        //kullanıcıya gelen offer'ları listelemek için;
        public async Task<IDataResult<List<OfferListDto>>> IncomingOffers(int userId)
        {
            var incommingOffers = await _unitOfWork.Offers.GetAllAsync(q=>q.Product.UserId==userId, q => q.Product, q => q.Product.Category, q => q.Product.Brand, q => q.Product.Colour, q => q.Product.Status);
            var resultDto = _mapper.Map<List<OfferListDto>>(incommingOffers);
            return new SuccessDataResult<List<OfferListDto>>(resultDto);
        }
        
        //kullanıcının yaptığı offer'ları listelemek için;
        public async Task<IDataResult<List<OfferListDto>>> MyOffers(int userId)
        {
            var myOffers = await _unitOfWork.Offers.GetAllAsync(q=>q.OfferedUserId == userId,q=>q.Product,q =>q.Product.Category, q => q.Product.Brand, q => q.Product.Colour, q => q.Product.Status);
            var resultDto = _mapper.Map<List<OfferListDto>>(myOffers);
            return new SuccessDataResult<List<OfferListDto>>(resultDto);

        }
    }
}
