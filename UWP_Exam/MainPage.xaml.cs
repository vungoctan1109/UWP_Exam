using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Exam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact
            {
                Name = txtName.Text,
                PhoneNumber = txtPhone.Text
            };
            DatabaseInitialize.Save(contact);
            ContentDialog contentDialog = new ContentDialog
            {
                CloseButtonText = "Close"
            };
            contentDialog.Title = "Action success.";
            contentDialog.Content = "Contact has been saved.";
            await contentDialog.ShowAsync();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Contact searchContact = DatabaseInitialize.GetContact(txtSearchName.Text, txtSearchPhone.Text);
            if (searchContact == null)
            {
                searchText.Text = $"Your contact: Name {searchContact.Name}, Phone {searchContact.PhoneNumber}";
            }
            else
            {
                ContentDialog contentDialog = new ContentDialog
                {
                    CloseButtonText = "Close"
                };
                contentDialog.Title = "Action failed.";
                contentDialog.Content = "Contact not found.";
                await contentDialog.ShowAsync();
            }
        }
    }
}