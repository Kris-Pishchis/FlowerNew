using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlowerShop.Services;
using FlowerShop.Services.api;
using FlowerShop.Services.interfaces;
using FlowerShop.UIModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public partial class AddProductViewModel : ViewModelBase
    {

        [ObservableProperty]
        private ProductUIModel _productUIModel;

        [ObservableProperty]
        private int? _quantity;

        [ObservableProperty]
        private decimal? _totalCost;
        private IOrderApi _orderApi;
        public AddProductViewModel(ProductUIModel selectedProd)
        {
            _productUIModel = selectedProd;
            _orderApi = new OrderApi();
        }

        [RelayCommand]
        public async Task AddToCart(Window currentWindow)
        {
            if(Quantity == null)
            {
                return;
            }
            var result = await _orderApi.AddToCartAsync(CurrentUser.LoginedUser.Id, ProductUIModel.Id, Quantity, TotalCost);
            if (!result)
            {
                Debug.WriteLine("Ошибка добавления в корзину");
                return;
            }
            currentWindow.Close(true);
        }

        partial void OnQuantityChanged(int? value)
        {
            TotalCost = ProductUIModel.Cost * value;
        }

        [RelayCommand]
        public async Task CloseWin(Window  currentWindow)
        {
            currentWindow.Close();
        }
    }
}
