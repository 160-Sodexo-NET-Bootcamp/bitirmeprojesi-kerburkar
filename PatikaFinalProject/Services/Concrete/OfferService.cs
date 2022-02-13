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
    public class OfferService : IOfferService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OfferService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

      
        public async Task<IResult> DirectBuy(int productId, int userId)
        {
            var product = await _unitOfWork.Products.GetAsync(q=>q.Id == productId);
            if (product == null)
            {
                return new ErrorResult("Ürün Bulunamadı");
            }

            var offerExist = await _unitOfWork.Offers.GetAsync(q=>q.OfferedUserId == userId && q.ProductId==productId);
            if (offerExist == null)
            {
                var offer = new Offer()
                {
                    ProductId = productId,
                    OfferedUserId = userId,
                    OfferStatus = Entities.Enums.OfferStatus.Sold,
                    OfferDate = DateTime.Now,
                    OfferedPrice = product.Price,
                };
                await _unitOfWork.Offers.AddAsync(offer);
            }
            else
            {
                offerExist.OfferStatus = Entities.Enums.OfferStatus.Sold;
                offerExist.OfferDate = DateTime.Now;
                offerExist.OfferedPrice = product.Price;
                await _unitOfWork.Offers.UpdateAsync(offerExist);
            }
           
        
            product.IsSold = true;          
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Ürün Satın Alındı");
        }

        public async Task<IDataResult<List<ProductGetDto>>> GetOfferableProducts(int userId)
        {
           var offeredProducts = await _unitOfWork.Products.GetAllAsync(q=>q.UserId != userId && q.IsSold == false && q.IsOfferable==true, q => q.Category, q => q.Colour, q => q.Brand, q => q.Status);
            var resultDto = _mapper.Map<List<ProductGetDto>>(offeredProducts);
            return new SuccessDataResult<List<ProductGetDto>>(resultDto);
        }

        public async Task<IResult> GiveOffer(OfferDto offerDto, int userId)
        {
            var offerExist = await _unitOfWork.Offers.GetAsync(q => q.OfferedUserId == userId && q.ProductId == offerDto.ProductId);
            if(offerExist == null)
            {
                var offer = _mapper.Map<Offer>(offerDto);
                offer.OfferDate = DateTime.Now;
                offer.OfferStatus = Entities.Enums.OfferStatus.Offered;
                offer.OfferedUserId = userId;
                await _unitOfWork.Offers.AddAsync(offer);
                await _unitOfWork.SaveAsync();
                return new SuccessResult("Teklif Başarıyla gönderildi.");
            }
            else
            {
                return new ErrorResult("Bu Ürüne Daha Önce teklif gönderildi.");

            }
         
        }

        public async Task<IResult> CancelOffer(int productId, int userId)
        {
            var offer = await _unitOfWork.Offers.GetAsync(q=>q.ProductId == productId && q.OfferedUserId==userId);
            if (offer == null)
            {
                return new ErrorResult("Teklif Bulunamadı");
            }
            offer.OfferStatus = Entities.Enums.OfferStatus.Canceled;
            await _unitOfWork.Offers.UpdateAsync(offer);
            await _unitOfWork.SaveAsync();
            return new SuccessResult("Teklif İptal edildi");
        }

    }
}
