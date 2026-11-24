using FlowerShop.Services.interfaces;
using FlowerShopSharedModels.Models;
using FlowerShopSharedModels.requests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Services.api
{
    public class ProductApi : IProductApi
    {
        private readonly HttpClient _httpClient;

        public ProductApi()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://100.65.4.111:5000") };
        }
        
        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<Product>>("api/Product/getProducts");
                if(response == null)
                {
                    return new List<Product>();
                }
                return response;
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }
    }
}
