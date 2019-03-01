using System.Collections.Generic;

namespace SportStore.Services.DTOs
{
    public class ProductsListDTO
    {
        public IEnumerable<ProductDTO> Products { get; set; }

        public PagingInfoDTO PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}