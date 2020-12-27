﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TestWPPL.Model;
using Velacro.UIElements.Basic;
using Velacro.UIElements.Button;

namespace TestWPPL.Pickup
{
    public partial class ListPickupPage : MyPage
    {

        private BuilderButton builderButton;
        private IMyButton refreshButton;
        public ListPickupPage()
        {
            InitializeComponent(); 
            this.KeepAlive = true;
            setController(new PickupController(this));
            initUIBuilders();
            initUIElements();
            getPickup();
        }

        private void initUIBuilders()
        {
            builderButton = new BuilderButton();
        }

        private void initUIElements()
        {
            refreshButton = builderButton
                .activate(this, "refreshBtn")
                .addOnClick(this, "onRefreshButtonClick");
        }

        public void onRefreshButtonClick()
        {
            getPickup();
        }

        public void setPickup(List<ModelPickup> pickups)
        {
            int id = 1;
            foreach (ModelPickup pickup in pickups)
            {
                if (pickup.status.Equals("picking up"))
                    pickup.buttonAction = "Process Service";
                else if (pickup.status.Equals("processing"))
                    pickup.buttonAction = "Deliver Back";
                else if (pickup.status.Equals("delivering"))
                    pickup.buttonAction = "Done !";
                pickup.num = id;
                id++;
            }

            this.Dispatcher.Invoke((Action)(() => {
                this.pickupList.ItemsSource = pickups;
            }));
        }

        private void getPickup()
        {
            String token = File.ReadAllText(@"userToken.txt");
            getController().callMethod("requestPickup", token);
        }

        public void setFailStatus(String _status)
        {
            MessageBoxResult result = MessageBox.Show(_status, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void PickUp_Click(object sender, RoutedEventArgs e)
        {
            String status = null;
            Button button = sender as Button;
            if (button.Content.Equals("Process Service"))
                status = "processing";
            else if (button.Content.Equals("Deliver Back"))
                status = "delivering";
            else if (button.Content.Equals("Done !"))
                status = null;

            ModelPickup dataObject = button.DataContext as ModelPickup; 
            String token = File.ReadAllText(@"userToken.txt");
            MessageBoxResult result;
            if (status != null)
                getController().callMethod("pickup", status, dataObject.booking_id, token);
            else
                result = MessageBox.Show("Vehicle has been delivered !", "Finished Pickup", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void setStatus(String _status)
        {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show(_status, "Set Pickup Status", MessageBoxButton.OK, MessageBoxImage.Information);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        this.NavigationService.Navigate(new ListPickupPage());
                        break;
                }
            });
        }
    }
}
