using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopSharedModels.requests
{
    public class AddToCartRequestDTO
    {
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Cost { get; set; }
        public bool Status { get; set; }
    }
}
