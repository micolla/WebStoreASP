using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebStore.DAL.SQLDBData;
using WebStore.Model.Entity;

namespace WebStore.DAL
{
    public class WebStoreDataInitialize
    {
        private readonly WebStoreDBContext _db;
        public WebStoreDataInitialize(WebStoreDBContext db) => _db = db;

        public async Task InitialAsync()
        {
            var db = _db.Database;
            
            await db.MigrateAsync(); // Автоматическое создание и миграция базы до последней версии

            if (await _db.Products.AnyAsync()) return;

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _db.Brands.AddRangeAsync(new[]
                            {
                                new Brand {Id = 1, Name = "Acne", Order = 0},
                                new Brand {Id = 2, Name = "Grune Erde", Order = 1},
                                new Brand {Id = 3, Name = "Albiro", Order = 2},
                                new Brand {Id = 4, Name = "Ronhill", Order = 3},
                                new Brand {Id = 5, Name = "Oddmolly", Order = 4},
                                new Brand {Id = 6, Name = "Boudestijn", Order = 5},
                                new Brand {Id = 7, Name = "Rosch creative culture", Order = 6},
                            });
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await _db.SaveChangesAsync();
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                transaction.Commit();
            }

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _db.Sections.AddRangeAsync(new[]
                            {
                                new Section {Id = 1, Name = "Sportswear", Order = 0},
                                new Section {Id = 2, Name = "Nike", Order = 0, ParentId = 1},
                                new Section {Id = 3, Name = "Under Armour", Order = 1, ParentId = 1},
                                new Section {Id = 4, Name = "Adidas", Order = 2, ParentId = 1},
                                new Section {Id = 5, Name = "Puma", Order = 3, ParentId = 1},
                                new Section {Id = 6, Name = "ASICS", Order = 4, ParentId = 1},
                                new Section {Id = 7, Name = "Mens", Order = 1},
                                new Section {Id = 8, Name = "Fendi", Order = 0, ParentId = 7},
                                new Section {Id = 9, Name = "Guess", Order = 1, ParentId = 7},
                                new Section {Id = 10, Name = "Valentino", Order = 2, ParentId = 7},
                                new Section {Id = 11, Name = "Dior", Order = 3, ParentId = 7},
                                new Section {Id = 12, Name = "Versace", Order = 4, ParentId = 7},
                                new Section {Id = 13, Name = "Armani", Order = 5, ParentId = 7},
                                new Section {Id = 14, Name = "Prada", Order = 6, ParentId = 7},
                                new Section {Id = 15, Name = "Dolce and Gabbana", Order = 7, ParentId = 7},
                                new Section {Id = 16, Name = "Chanel", Order = 8, ParentId = 7},
                                new Section {Id = 17, Name = "Gucci", Order = 9, ParentId = 7},
                                new Section {Id = 18, Name = "Womens", Order = 2},
                                new Section {Id = 19, Name = "Fendi", Order = 0, ParentId = 18},
                                new Section {Id = 20, Name = "Guess", Order = 1, ParentId = 18},
                                new Section {Id = 21, Name = "Valentino", Order = 2, ParentId = 18},
                                new Section {Id = 22, Name = "Dior", Order = 3, ParentId = 18},
                                new Section {Id = 23, Name = "Versace", Order = 4, ParentId = 18},
                                new Section {Id = 24, Name = "Kids", Order = 3},
                                new Section {Id = 25, Name = "Fasion", Order = 4},
                                new Section {Id = 26, Name = "Households", Order = 5},
                                new Section {Id = 27, Name = "Interiors", Order = 6},
                                new Section {Id = 28, Name = "Clothing", Order = 7},
                                new Section {Id = 29, Name = "Bags", Order = 8},
                                new Section {Id = 30, Name = "Shoes", Order = 9}
                            });
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await _db.SaveChangesAsync();
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                transaction.Commit();
            }

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _db.Employees.AddRangeAsync(new[]
                            {
                                new Employee{Id=1,FirstName="Николай",LastName="Донцов"
                                    ,HiringDate=DateTime.Parse("2016-05-01"),BirthDay=DateTime.Parse("1990-12-22")},
                                new Employee{Id=2,FirstName="Александр",LastName="Иванов"
                                    ,HiringDate=DateTime.Parse("2001-05-01"),BirthDay=DateTime.Parse("1985-10-10")},
                                new Employee{Id=3,FirstName="Сидор",LastName="Сидоров"
                                    ,HiringDate=DateTime.Parse("1999-05-01"),BirthDay=DateTime.Parse("1960-05-22")},
                                new Employee{Id=4,FirstName="Инокентий",LastName="Смактуновский"
                                    ,HiringDate=DateTime.Parse("2019-05-01"),BirthDay=DateTime.Parse("1999-02-22")},
                                new Employee{Id=5,FirstName="Афанасий",LastName="Ленин"
                                    ,HiringDate=DateTime.Parse("2010-05-01"),BirthDay=DateTime.Parse("1970-06-01")}
                            });
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Employees] ON");
                await _db.SaveChangesAsync();
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Employees] OFF");

                transaction.Commit();
            }

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _db.Products.AddRangeAsync(new[]
                            {
                                new Product { Id = 1, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product1.jpg", Order = 0, SectionId = 2, BrandId = 1 },
                                new Product { Id = 2, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product2.jpg", Order = 1, SectionId = 2, BrandId = 1 },
                                new Product { Id = 3, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product3.jpg", Order = 2, SectionId = 2, BrandId = 1 },
                                new Product { Id = 4, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product4.jpg", Order = 3, SectionId = 2, BrandId = 1 },
                                new Product { Id = 5, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product5.jpg", Order = 4, SectionId = 2, BrandId = 2 },
                                new Product { Id = 6, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product6.jpg", Order = 5, SectionId = 2, BrandId = 1 },
                                new Product { Id = 7, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product7.jpg", Order = 6, SectionId = 2, BrandId = 1 },
                                new Product { Id = 8, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product8.jpg", Order = 7, SectionId = 25, BrandId = 1 },
                                new Product { Id = 9, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product9.jpg", Order = 8, SectionId = 25, BrandId = 1 },
                                new Product { Id = 10, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product10.jpg", Order = 9, SectionId = 25, BrandId = 3 },
                                new Product { Id = 11, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product11.jpg", Order = 10, SectionId = 25, BrandId = 3 },
                                new Product { Id = 12, Name = "Easy Polo Black Edition", Price = 1025, ImageUrl = "product12.jpg", Order = 11, SectionId = 25, BrandId = 3 },
                            });
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _db.SaveChangesAsync();
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }

        }
    }
}
