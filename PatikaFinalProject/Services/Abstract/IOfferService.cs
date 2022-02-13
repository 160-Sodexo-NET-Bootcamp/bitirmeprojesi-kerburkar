using Entities.Dtos;
using Services.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IOfferService
    {
        Task<IDataResult<List<ProductGetDto>>> GetOfferableProducts(int userId);
        Task<IResult> GiveOffer(OfferDto offerDto ,int userId);
        Task<IResult> CancelOffer(int productId, int userId);
        Task<IResult> DirectBuy(int productId, int userId);

    }
}
