using System.Collections.Generic;
using WebStore.Domain.ViewModels.Interfaces;

namespace WebStore.Domain.ViewModels
{
    public class SectionViewModel : INamedViewModel, IOrderedViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<SectionViewModel> ChildSections { get; set; } = new List<SectionViewModel>();
        public SectionViewModel ParentSection { get; set; }
    }
}
