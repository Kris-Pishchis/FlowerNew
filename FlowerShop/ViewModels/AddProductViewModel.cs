using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlowerShop.Services.api;
using FlowerShop.Services.interfaces;
using FlowerShop.UIModels;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public partial class AddProductViewModel : ViewModelBase
    {

        [ObservableProperty]
        private ProductUIModel _productUIModel;
        public AddProductViewModel(ProductUIModel selectedProd)
        {
            _productUIModel = selectedProd;
        }
        [RelayCommand]
        private async Task GoBack(Window currentWindow)
        {
            currentWindow.Close(true);
        }
    }
}
