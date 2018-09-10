﻿

namespace Sales.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Helpers;
    using Sales.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class AddProductViewModel : BaseViewModel
    {
        #region Attributes

        private ImageSource imageSource;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;

        #endregion

        #region Properties

        public string Description { get; set; }

        public string Price { get; set; }

        public string Remarks { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }


        #endregion

        #region Constructors

        public AddProductViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;

        }


        #endregion

        #region Commands

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Description))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    Languages.DescriptionError, 
                    Languages.Accept);
                return;
            }


            if (string.IsNullOrEmpty(this.Price))
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PriceError,
                    Languages.Accept);
                return;
            }

            var price = decimal.Parse(this.Price);
            if (price < 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.PriceError,
                    Languages.Accept);
                return;
            }


            this.IsRunning = true;
            this.isEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    connection.Message, 
                    Languages.Accept);
                return;

            }

            var product = new Product
            {
                Description = this.Description,
                Price = price,
                Remarks = this.Remarks,
                

            };

            var response = await this.apiService.Post(
                "https://salesapijonalexjm.azurewebsites.net", 
                "/api", 
                "/Products",
                product);

            if (!response.IsSuccess)// si funciono sigue pasando
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, 
                    response.Message, 
                    Languages.Accept);
                return;
            }

            var newProduct = (Product)response.Result;
            var viewModel = ProductsViewModel.GetInstance();
            viewModel.Products.Add(newProduct);
            

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.Navigation.PopAsync();// navega a la pagina pricipal


        }

        #endregion
    }
}
