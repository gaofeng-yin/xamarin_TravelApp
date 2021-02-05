using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class RegisterVM : INotifyPropertyChanged
    {
        private string email;

        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                User = new Users()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ComfirmPassword = this.ComfirmPassword
                };
                OnPropertyChanged("Email");
            }
        }
        private string  password;

        public string  Password
        {
            get { return password; }
            set
            { 
                password = value;
                User = new Users()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ComfirmPassword = this.ComfirmPassword
                };
                OnPropertyChanged("Password");
            }
        }

        private string comfirmPassword;

        public string ComfirmPassword
        {
            get { return comfirmPassword; }
            set 
            { 
                comfirmPassword = value;
                User = new Users()
                {
                    Email = this.Email,
                    Password = this.Password,
                    ComfirmPassword = this.ComfirmPassword
                };
                OnPropertyChanged("ComfirmPassword");
            }
        }

        private Users user;

        public Users User
        {
            get { return user; }
            set 
            { 
                user = value;
                OnPropertyChanged("User");
            }
        }

        public RegisterVM()
        {
            RegisterCommand = new RegisterCommand(this);
        }

        public RegisterCommand RegisterCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Register(Users user)
        {
            Users.Register(user);
        }
    }
}
