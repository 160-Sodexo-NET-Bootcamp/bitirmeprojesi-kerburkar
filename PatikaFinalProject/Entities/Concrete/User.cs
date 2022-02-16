using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User: IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; } = false;
        public byte[] PasswordHash { get; set; }

        //tuzlama işlemi için;
        public byte[] PasswordSalt { get; set; }

        //bir kullanıcının birden fazla ürünü olabileceği için liste;
        public virtual IList<Product> Products { get; set; }

        //hatalı girilen şifre sayısı;
        public int FailedLoginCount { get; set; }
    }
}
