using SportStore.Services.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Web.ViewComponents.ViewModels
{
    public class CategoryViewModel
    {
        public CategoryViewModel(CategoryesDTO serviceModel)
        {
            this.Categoryes = serviceModel.Categoryes.Select(c => c);
        }

        public IEnumerable<string> Categoryes { get; set; }

        public string SelectedCategory { get; set; }
    }
}