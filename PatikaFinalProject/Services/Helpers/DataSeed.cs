using Data.Concrete.EntityFramework.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    //proje çalıştığında tablolarda veri yoksa demo data kaydetmek için;
    public static class DataSeed
    {
        public static async Task SeedAsync(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Colours.Any())
            {
                context.Colours.AddRange(new Entities.Concrete.Colour[] {
                new Entities.Concrete.Colour()
                {
                    Name = "Mavi",
                },
                new Entities.Concrete.Colour()
                {
                    Name = "Kırmızı",

                },
                new Entities.Concrete.Colour()
                {
                    Name = "Sarı",
                },
                new Entities.Concrete.Colour()
                {
                    Name = "Siyah",
                },
                new Entities.Concrete.Colour()
                {
                    Name = "Yeşil",
                },
                new Entities.Concrete.Colour()
                {
                    Name = "Turuncu",
                },
                new Entities.Concrete.Colour()
                {
                    Name = "Mor",

                },
                });
                context.SaveChanges();
            }
            if (!context.Brands.Any())
            {
                context.Brands.AddRange(new Entities.Concrete.Brand[] {
                new Entities.Concrete.Brand()
                {
                    Name = "Marka A",
                },
                new Entities.Concrete.Brand()
                {
                    Name = "Marka B",
                },
                new Entities.Concrete.Brand()
                {
                    Name = "Marka C",
                },
                new Entities.Concrete.Brand()
                {
                    Name = "Marka D",
                },
                });
                context.SaveChanges();
            }
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new Entities.Concrete.Category[]
                {
                    new Entities.Concrete.Category()
                    {
                        Name = "Elbise"
                    },
                    new Entities.Concrete.Category()
                    {
                        Name = "Gömlek"
                    },
                    new Entities.Concrete.Category()
                    {
                        Name = "Pantolon"
                    },
                    new Entities.Concrete.Category()
                    {
                        Name = "Kazak"
                    },
                });
                context.SaveChanges();
            }
            if (!context.Statuses.Any())
            {
                context.Statuses.AddRange(new Entities.Concrete.Status[]
                {
                    new Entities.Concrete.Status()
                    {
                        Name = "Kullanılmış"
                    },
                    new Entities.Concrete.Status()
                    {
                        Name = "Az Kullanılmış"
                    },
                    new Entities.Concrete.Status()
                    {
                        Name = "Kullanılmamış"
                    },
                });
                context.SaveChanges();
            }
        }
    }
}
