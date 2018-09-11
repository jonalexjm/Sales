using GalaSoft.MvvmLight.Command;
using Sales.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
//using static System.Net.Mime.MediaTypeNames;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class MainViewModel
    {
        #region Properties

        public EditProductViewModel EditProduct { get; set; }

        public ProductsViewModel Products { get; set; }

        public AddProductViewModel AddProduct { get; set; } 
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.Products = new ProductsViewModel();
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
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());
        } 
        #endregion
    }
}
