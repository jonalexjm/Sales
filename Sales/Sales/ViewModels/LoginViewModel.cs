using Plugin.Media.Abstractions;
using Sales.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sales.ViewModels
{
    public class LoginViewModel
    {

        #region Attributes

        private MediaFile file;

        private ImageSource imageSource;

        private ApiService apiService;

        private bool isRunning;

        private bool isEnabled;

        #endregion


        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        public string IsRemembered { get; set; }

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

        #endregion

        #region Commands

        #endregion
    }
}
