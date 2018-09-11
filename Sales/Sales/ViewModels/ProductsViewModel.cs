
namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Helpers;
    using Sales.Services;
    using Xamarin.Forms;
    

    public class ProductsViewModel : BaseViewModel
    {

        #region Attributes
        private ApiService apiService;

        private bool isRefreshing;

        private ObservableCollection<ProductItemViewModel> products;

        public List<Product> MyProducts { get; set; }


        #endregion

        #region Properties


        public ObservableCollection<ProductItemViewModel> Products
        {
            get { return this.products; }

            set { this.SetValue(ref this.products, value); }


        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }

            set { this.SetValue(ref this.isRefreshing, value); }


        }
        #endregion




        

        #region Contructors
        public ProductsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadProducts();
        }
        #endregion

        #region Singleton // coger instancia que esta en memoria

        public static ProductsViewModel instance;

        public static ProductsViewModel GetInstance()
        {
            if(instance == null)
            {
                return new ProductsViewModel();
            }
            return instance;
        }

        #endregion

        #region Methods
        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;

            }

            var response = await this.apiService.GetList<Product>(
                "https://salesapijonalexjm.azurewebsites.net", 
                "/api", 
                "/Products");

            this.IsRefreshing = false;

            if (!response.IsSuccess)// si funciono sigue pasando
            {
               
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }



            this.MyProducts = (List<Product>)response.Result;
            this.RefreshList();

            var myListProductItemViewModel = MyProducts.Select(p => new ProductItemViewModel
            {
                Description = p.Description,
                ImageArray = p.ImageArray,
                ImagePath = p.ImagePath,
                IsAvailable = p.IsAvailable,
                Price = p.Price,
                ProductId = p.ProductId,
                PublishOn = p.PublishOn,
                Remarks = p.Remarks,
            });

            this.Products = new ObservableCollection<ProductItemViewModel>(myListProductItemViewModel.OrderBy(p => p.Description));
            this.IsRefreshing = false;


        }

        public void RefreshList()
        {
            var myListProductItemViewModel = MyProducts.Select(p => new ProductItemViewModel
            {
                Description = p.Description,
                ImageArray = p.ImageArray,
                ImagePath = p.ImagePath,
                IsAvailable = p.IsAvailable,
                Price = p.Price,
                ProductId = p.ProductId,
                PublishOn = p.PublishOn,
                Remarks = p.Remarks,
            });

            this.Products = new ObservableCollection<ProductItemViewModel>(myListProductItemViewModel.OrderBy(p => p.Description));

        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {

            get
            {
                return new RelayCommand(LoadProducts);
            }

        } 
        #endregion
    }
}
