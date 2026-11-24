using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class OrderPageViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainViewModel;
        public ObservableCollection<ProductUIModel> Products { get; } = new ObservableCollection<ProductUIModel>();
        private Window _currentWindow;


        [ObservableProperty]
        private string? _error;

        public OrderPageViewModel(MainWindowViewModel mainViewModel, Window window) 
        {
            _mainViewModel = mainViewModel;
            Title = "Заказы";
            _currentWindow = window;
        }


        [RelayCommand]
        public async Task GoToPrimary()
        {
            await _mainViewModel.GoToSystem(_currentWindow);
        }

    }
}
