using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlowerShop.Services.api;
using FlowerShop.Services.interfaces;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private object _currentGlobalScreen;

        public MainWindowViewModel()
        {
            _currentGlobalScreen = new StartPageViewModel(this);
        }

        public async Task GoToRegistration()
        {
            CurrentGlobalScreen = new RegisterPageViewModel(this);
        }

        public async Task GoToSystem(Window selectedWindow)
        {
            CurrentGlobalScreen = new PrimaryPageViewModel(this, selectedWindow);
        }
        public async Task GoToCart(Window selectedWindow)
        {
            CurrentGlobalScreen = new CartPageViewModel(this, selectedWindow);
        }
        public async Task GotoOrder(Window selectedWindow)
        {
            CurrentGlobalScreen = new OrderPageViewModel(this, selectedWindow);
        }
    }
}
