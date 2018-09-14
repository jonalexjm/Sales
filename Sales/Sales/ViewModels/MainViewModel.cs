using GalaSoft.MvvmLight.Command;
using Sales.Helpers;
using Sales.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
//using static System.Net.Mime.MediaTypeNames;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class MainViewModel
    {
        #region Properties

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        public LoginViewModel Login { get; set; }

        public EditProductViewModel EditProduct { get; set; }

        public ProductsViewModel Products { get; set; }

        public AddProductViewModel AddProduct { get; set; } 
        #endregion

        #region Constructor
        public MainViewModel()
        {
            //this.Products = new ProductsViewModel();
            //this.Login = new LoginViewModel();
            instance = this;
            this.LoadMenu();
            
        }

        
        #endregion

        #region Singleton // coger instancia que esta en memoria

        public static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        #endregion


        #region Methods
        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = Languages.About,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_phonelink_setup",
                PageName = "SetupPage",
                Title = Languages.Setup,
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = Languages.Exit,
            });

        }
        #endregion

        #region Commands

        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);
            }

        }

        private async void GoToAddProduct()
        {
            this.AddProduct = new AddProductViewModel();// se instancia aqui para que le de tiempo de entrar
            await App.Navigator.PushAsync(new AddProductPage());
        } 
        #endregion
    }
}
