using System;
using WebStore.Domain.Entity.Base.Interfaces;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO.Products
{
    public class BrandDTO : INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
