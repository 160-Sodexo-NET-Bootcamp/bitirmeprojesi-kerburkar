using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    //bu arayüz de tüm DAL class'larımızda ortak kullanacağımız metodları bu repository'e ekleniyor.
    public interface IEntityRepository<T> where T : class, IEntity, new()  //imzası, sadece veri tabanı nesnelerinin gelebilmesi için. class olan, IEntity'i miras alan ve new'lenebilir.
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties); //var kullanici = repository.GetAsync
                                                                                                                       //(k=>k.Id==15);

        //listeye ihtiyacımız olduğunda;
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);

        //eklemek için;
        Task<T> AddAsync(T entity);

        //güncellemek için;
        Task<T> UpdateAsync(T entity);

        //silmek için;
        Task DeleteAsync(T entity);

        //"böyle bir şey var mı?" kontrol etmek için;
        //predicate neye göre soracağımız
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        //verileri sayısal olarak sıralamak istersek;
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    }
}
