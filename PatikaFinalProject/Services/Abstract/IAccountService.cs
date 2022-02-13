using Entities.Concrete;
using Entities.Dtos;
using Services.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IAccountService
    {
        Task<IDataResult<List<OfferListDto>>> MyOffers(int userId);
        Task<IDataResult<List<OfferListDto>>> IncomingOffers(int userId);
        Task<IResult> ChangeOfferStatus(int offerId, int statusId);
        Task<IResult> BuyProduct(int offerId, int userId);

    }
}
