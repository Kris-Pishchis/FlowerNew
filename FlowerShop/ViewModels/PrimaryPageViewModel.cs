using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlowerShop.Services;
using FlowerShop.Services.api;
using FlowerShop.Services.HelpHashMethods;
using FlowerShop.Services.interfaces;
using FlowerShop.UIModels;
using FlowerShop.Views;
using FlowerShopSharedModels.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public partial class PrimaryPageViewModel : ViewModelBase
    {
        private readonly IProductApi _productApi;
        private readonly MainWindowViewModel _mainViewModel;
        public ObservableCollection<ProductUIModel> Products { get; } = new ObservableCollection<ProductUIModel>();
        private ProductUIModel? _selectedProduct;
        private Window _currentWindow;


        [ObservableProperty]
        private string? _error;

        public PrimaryPageViewModel(MainWindowViewModel mainViewModel, Window window) 
        {
            _productApi = new ProductApi();
            _mainViewModel = mainViewModel;
            Title = "Главная";
            _currentWindow = window;
            _ = GetProductsAsync();
        }

        public async Task GetProductsAsync()
        {
            var result = await _productApi.GetProductsAsync();
            if(result == null)
            {
                return;
            }
            var uri = new Uri("avares://FlowerShop/Assets/avalonia-logo.ico");
            foreach (var item in result)
            {
                ProductUIModel newProd = new ProductUIModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Cost = item.Cost,
                    DehashedImage = item.PhotoHashed != null ? RehashImage.ConvertToImage(item.PhotoHashed) : new Bitmap(AssetLoader.Open(uri))
                };
                Products.Add(newProd);
            }
        }

        [RelayCommand]
        public async Task AddToKorzinaProduct(ProductUIModel prod)
        {
            if(_selectedProduct == null)
            {
                _selectedProduct = prod;
                return;
            }
            if(prod == _selectedProduct)
            {
                AddProductPageWindow addprod = new AddProductPageWindow()
                {
                    DataContext = new AddProductViewModel(prod)
                };
                bool dialogresult = await addprod.ShowDialog<bool>(_currentWindow);
                if (dialogresult == true) 
                {
                    _selectedProduct = null;
                }
                return;
            }
            else
            {
                _selectedProduct = prod;
                return;
            }
        }

        [RelayCommand]
        public async Task GoToCart()
        {
            await _mainViewModel.GoToCart(_currentWindow);
        }

        [RelayCommand]
        public async Task GoToOrder()
        {
            await _mainViewModel.GotoOrder(_currentWindow);
        }

        [RelayCommand]
        public async Task GoToPrimary()
        {
            await _mainViewModel.GoToSystem(_currentWindow);
        }
        [RelayCommand]
        private async Task BackToStart()
        {
            CurrentUser.LoginedUser = null;
            _mainViewModel.CurrentGlobalScreen = new StartPageViewModel(_mainViewModel);
        }

    }
}
