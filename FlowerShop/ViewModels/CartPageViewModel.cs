using Avalonia;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlowerShop.Services;
using FlowerShop.Services.api;
using FlowerShop.Services.interfaces;
using FlowerShop.UIModels;
using FlowerShop.Views;
using FlowerShopSharedModels.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace FlowerShop.ViewModels
{
    public partial class CartPageViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        public ObservableCollection<CartDTO> GettedCart {get;} = new ObservableCollection<CartDTO>();
        private Window _currentWindow;
        [ObservableProperty]
        private string? _error;

        private IOrderApi _orderApi;
        public CartPageViewModel(MainWindowViewModel mainWindowViewModel, Window currentWin)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _currentWindow = currentWin;
            _orderApi = new OrderApi();
            _ = GetCartAsync();
        }

        private async Task GetCartAsync()
        {
            var list = await _orderApi.GetAllProdCartAsync(CurrentUser.LoginedUser.Id);
            if(list == null)
            {
                return; 
            }
            GettedCart.Clear();
            foreach(var item in list)
            {
                GettedCart.Add(item);
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await _mainWindowViewModel.GoToSystem(_currentWindow);
        }

        [RelayCommand]
        public async Task DeleteFromCartAsync(object selectedProd)
        {
            var selected = selectedProd as CartDTO;
            var result = await _orderApi.DeleteProdAsync(selected.ProductId);
            await GetCartAsync();
        }

        [RelayCommand]
        public async Task OpenOrderWindowAsync()
        {
            ContinueOrderPageWindow continueOrder = new ContinueOrderPageWindow()
            {
                DataContext = new ContinueOrderPageViewModel(GettedCart.ToList())
            };
            bool dialogResult = await continueOrder.ShowDialog<bool>(_currentWindow);
            if (dialogResult)
            {
                var result = await _orderApi.DeleteAllCartAsync(GettedCart.ToList());
                if (result)
                {
                    await GetCartAsync();
                }
            }
        }
    }
}
