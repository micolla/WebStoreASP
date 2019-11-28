using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Model.Interfaces;
using WebStore.ViewModels;
using WebStore.Infrastructure.Mappings;

namespace WebStore.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductDataProvider _ProductData;
        public SectionsViewComponent(IProductDataProvider productData)
        {
            _ProductData = productData;
        }
        public IViewComponentResult Invoke() => View(GetSections());

        private IEnumerable<SectionViewModel> GetSections()
        {
            var sections = _ProductData.GetSections();
            var parentSections = sections.Where(s => s.ParentId == null);
            IEnumerable<SectionViewModel> childSectionsView;
            var parentSectionsView = parentSections
                .Select(s=>new SectionViewModel
                            {
                                Id=s.Id,
                                Name=s.Name,
                                Order=s.Order
                            })
                .OrderBy(s=>s.Order).ToArray();
            foreach (var parentSectionView in parentSectionsView)
            {
                childSectionsView = sections
                                .Where(s => s.ParentId == parentSectionView.Id)
                                .Select((ch) =>
                                    new SectionViewModel
                                    {
                                        Id = ch.Id,
                                        Name = ch.Name,
                                        Order = ch.Order,
                                        ParentSection = parentSectionView
                                    }).OrderBy(ch=>ch.Order);
                parentSectionView.ChildSections = childSectionsView.ToList();
            }
            return parentSectionsView;
        }
            
    }
}
