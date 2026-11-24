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
    public class OrderApi : IOrderApi
    {
        private readonly HttpClient _httpClient;

        public OrderApi()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://100.65.4.111:5000") };
        }
        
        public async Task<bool> AddToCartAsync(int? idUser, int? idProduct, int? quantity, decimal? totalCost)
        {
            try
            {
                AddToCartRequestDTO request = new AddToCartRequestDTO () {UserId = idUser, ProductId = idProduct, Quantity = quantity, Cost = totalCost, Status = true};
                if(request == null)
                {
                    return false;
                }
                var response = await _httpClient.PostAsJsonAsync("api/Order/addToCart", request);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CartDTO>> GetAllProdCartAsync(int? userId)
        {
            try
            {
             var response = await _httpClient.GetFromJsonAsync<List<CartDTO>>($"api/Order/getProdInCart?userId={userId}");   
             if(response == null)
                {
                    return new List<CartDTO>();
                }
                return response;
            }
            catch
            {
                return new List<CartDTO>();
            }
        }

        public async Task<bool> DeleteProdAsync(int? prodId)
        {
            try
            {
             var response = await _httpClient.PostAsJsonAsync("api/Order/deleteProd", prodId);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteAllCartAsync(List<CartDTO> cart)
        {
            try
            {
             var response = await _httpClient.PostAsJsonAsync("api/Order/deleteAllCart", cart);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> AddNewOrder(int? userId, List<CartDTO>? selectedProds, decimal? totalCost, TimeSpan? deliveryTime, string? adress, string? street, string? apart, string? home, string?  phone, string? family, string? name, string? father)
        {
            try
            {   
                AddOrderRequestDTO request = new AddOrderRequestDTO(){UserId = userId, UserCart = selectedProds, TotalCost = totalCost, DeliveryTime = deliveryTime, Adress = adress, StreetName = street, ApartName = apart, HomeName = home, Phone = phone, Family = family, Name = name, Father = father};
                var response = await _httpClient.PostAsJsonAsync("api/Order/addnewOrder",request);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
