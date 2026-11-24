using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopSharedModels.Models
{
    public class CartDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Cost { get; set; }
        public string? ProductName { get; set; }
        public Boolean? Status { get; set; }
        public string? UserName { get; set; }
        
    }
}
