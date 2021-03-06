﻿using System;
using System.IO;
using System.Windows;
using TestWPPL.Dashboard;
using Velacro.Enums;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;
using Velacro.UIElements.PasswordBox;
using Velacro.UIElements.TextBox;

namespace TestWPPL.Login
{

    public partial class LoginPage : MyPage
    {
        private BuilderButton buttonBuilder;
        private BuilderTextBox txtBoxBuilder;
        private BuilderPasswordBox pwdBoxBuilder;
        private IMyButton loginButton_btn;
        private IMyTextBox emailTxtBox;
        private IMyPasswordBox passwordTxtBox;

        public LoginPage()
        {
            InitializeComponent();
            setController(new LoginController(this));
            initUIBuilders();
            initUIElements();
        }

        private void initUIBuilders()
        {
            buttonBuilder = new BuilderButton();
            txtBoxBuilder = new BuilderTextBox();
            pwdBoxBuilder = new BuilderPasswordBox();
        }

        private void initUIElements()
        {
            loginButton_btn = buttonBuilder
                .activate(this, "loginButton")
                .addOnClick(this, "onLoginButtonClick");
            emailTxtBox = txtBoxBuilder.activate(this, "emailTextBox");
            emailTxtBox.setTextVerticalAlignment(MyVerticalAlignment.CENTER);
            passwordTxtBox = pwdBoxBuilder.activate(this, "passwordBox");
            passwordTxtBox.setPasswordVerticalAlignment(MyVerticalAlignment.CENTER);
        }

        public void onLoginButtonClick()
        {
            getController().callMethod("login", emailTextBox.Text, passwordBox.Password);
        }


        public void saveToken(String token)
        {
            this.Dispatcher.Invoke(() =>
            {
                string fullPath = @"userToken.txt";
                File.WriteAllText(fullPath, token);
                new BenjolWindow().Show();
                Window.GetWindow(this).Close();
            });
        }

        public void setStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
