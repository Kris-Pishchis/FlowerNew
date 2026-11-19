using FlowerShopSharedModels.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services.interfaces
{
    public interface IProductApi
    {
        Task<List<Product>> GetProductsAsync();
    }
}
