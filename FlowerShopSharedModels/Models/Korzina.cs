using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShopSharedModels.Models
{
    public class Korzina
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Cost { get; set; }
        public bool Status { get; set; }
    }
}
