﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        Users user;
        public RegisterPage()
        {
            InitializeComponent();

            user = new Users();
            containerStackLayout.BindingContext = user;
        }

        private async void registerButton_Clicked(object sender, EventArgs e)
        {
            if(passwordEntry.Text == confirmPasswordEntry.Text)
            {
                Users.Register(user);
            }
            else
            {
                DisplayAlert("Error", "Passwords don't match", "OK");
            }
        }
    }
}