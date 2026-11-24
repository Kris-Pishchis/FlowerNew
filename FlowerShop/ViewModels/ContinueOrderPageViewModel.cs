using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlowerShop.Services;
using FlowerShop.Services.api;
using FlowerShop.Services.interfaces;
using FlowerShop.UIModels;
using FlowerShopSharedModels.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public partial class ContinueOrderPageViewModel : ViewModelBase
    {

        private IOrderApi _orderApi;

        [ObservableProperty]
        private string? _family;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _father;

        [ObservableProperty]
        private string? _phone;

        [ObservableProperty]
        private string? _adress;

        [ObservableProperty]
        private string? _apart;

        [ObservableProperty]
        private string? _home;
        
        [ObservableProperty]
        private string? _street;

        [ObservableProperty]
        private string? _error;

        [ObservableProperty]
        private List<CartDTO>? _cartProducts;

        [ObservableProperty]
        private decimal? _totalCost;


        public ContinueOrderPageViewModel(List<CartDTO> listProd)
        {
            _orderApi = new OrderApi();
            CartProducts = listProd;
            _ = GetInfo();
        }

        public  async Task GetInfo()
        {
            Family = CurrentUser.LoginedUser.Family;
            Name = CurrentUser.LoginedUser.Name;
            Father = CurrentUser.LoginedUser.Father;
            Phone = CurrentUser.LoginedUser.Phone;
            Street = CurrentUser.LoginedUser.StreetName;
            Apart = CurrentUser.LoginedUser.Apartment;
            Home = CurrentUser.LoginedUser.Home;
            Adress = Street  + " " + Apart + " " + Home;
        }

        [RelayCommand]
        public async Task CloseWinFalse(Window _currentWin)
        {
            _currentWin.Close(false);
        }

        [RelayCommand]
        public async Task CloseWinTrue(Window _currentWin)
        {
            if(CartProducts == null)
            {
                return;
            }

            TotalCost = 0;
            foreach(var item in CartProducts)
            {
                TotalCost += item.Cost * item.Quantity;
            }

            if(string.IsNullOrWhiteSpace(Name) 
            || string.IsNullOrWhiteSpace(Family) 
            || string.IsNullOrWhiteSpace(Father) 
            || string.IsNullOrWhiteSpace(Street) 
            || string.IsNullOrWhiteSpace(Apart) 
            || string.IsNullOrWhiteSpace(Home) 
            || string.IsNullOrWhiteSpace(Phone) 
            || Phone.Length < 11)
            {
                Error = "Проверьте корректность всех полей!";
                return;
            }

            var result = await _orderApi.AddNewOrder(CurrentUser.LoginedUser.Id, CartProducts, TotalCost, TimeSpan.FromHours(15), Adress, Street, Apart, Home, Phone, Family, Name, Father);
            if (result)
            {
                _currentWin.Close(true);
            }
        }



    }
}
