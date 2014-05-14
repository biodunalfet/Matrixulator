using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Matrixulator.Resources;

namespace Matrixulator
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }


        private void addition_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/addition.xaml", UriKind.Relative));

        }

        private void subtraction_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/subtraction.xaml", UriKind.Relative));
        }

        private void multiplication_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/mult.xaml", UriKind.Relative));

        }

        private void constmult_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/constmult.xaml", UriKind.Relative));
        }

        private void determinant_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/determinant.xaml", UriKind.Relative));
        }

        private void inverse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/inverse.xaml", UriKind.Relative));

        }

        private void Cofactor_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/cofactor.xaml", UriKind.Relative));
        }

        private void transpose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/transpose.xaml", UriKind.Relative));

        }

        private void simult_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/findvar.xaml", UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}