using FlowerShopSharedModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services.interfaces
{
    public interface IOrderApi
    {
        Task<bool> AddToCartAsync(int? idUser, int? idProduct, int? quantity, decimal? totalCost);
        Task<List<CartDTO>> GetAllProdCartAsync(int? userId);
        Task<bool> DeleteProdAsync(int? prodId);
        Task<bool> AddNewOrder(int? userId, List<CartDTO>? selectedProds, decimal? totalCost, TimeSpan? deliveryTime, string? adress, string? street, string? apart, string? home, string?  phone, string? family, string? name, string? father);
        Task<bool> DeleteAllCartAsync(List<CartDTO> cart);
    }
}
