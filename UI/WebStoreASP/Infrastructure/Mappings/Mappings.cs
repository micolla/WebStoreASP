using System;
using WebStore.Domain.ViewModels;
using WebStore.Domain.Entity;
using WebStore.Domain.DTO.Products;

namespace WebStore.Infrastructure.Mappings
{
    public static class Mappings
    {
        public static EmployeeView MapEmployeeView(this Employee employee)
        {
            return new EmployeeView { 
                Id=employee.Id
                ,FirstName=employee.FirstName
                ,LastName=employee.LastName
                ,HiringDate=employee.HiringDate
                ,BirthDay=employee.BirthDay
            };
        }

        public static Employee MapEmployee(this EmployeeView employeeView)
        {
            return new Employee
            {
                Id = employeeView.Id
                ,
                FirstName = employeeView.FirstName
                ,
                LastName = employeeView.LastName
                ,
                HiringDate = employeeView.HiringDate
                ,
                BirthDay = employeeView.BirthDay
            };
        }

        public static Brand MapBrandViewToBrand(this BrandViewModel brandViewModel)
        {
            return new Brand {
                Id=brandViewModel.Id,
                Name=brandViewModel.Name,
                Order=brandViewModel.Order
            };
        }

        public static BrandViewModel MapBrandDTOToBrandView(this BrandDTO brand)
        {
            return new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }

        public static BrandViewModel MapBrandToBrandView(this Brand brand)
        {
            return new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order
            };
        }

        public static Section MapSectionViewToSection(this SectionViewModel sectionViewModel)
        {
            return new Section
            {
                Id = sectionViewModel.Id,
                Name = sectionViewModel.Name,
                Order = sectionViewModel.Order,
                ParentId = sectionViewModel.ParentSection.Id,
            };
        }

    }
}
