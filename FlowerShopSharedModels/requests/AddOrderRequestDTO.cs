using System;
using System.Collections.Generic;
using System.Text;
using FlowerShopSharedModels.Models;

namespace FlowerShopSharedModels.requests
{
    public class AddOrderRequestDTO
    {
        public int? UserId { get; set; }
        public decimal? TotalCost { get; set; }
        public TimeSpan? DeliveryTime { get; set; }
        public string? Adress { get; set; }
        public string? StreetName { get; set; }
        public string? ApartName { get; set; }
        public string? HomeName { get; set; }
        public string? Phone { get; set; }
        public string? Family { get; set; }
        public string? Name { get; set; }
        public string? Father { get; set; }
        public List<CartDTO>? UserCart {get; set;}
        
    }
}
