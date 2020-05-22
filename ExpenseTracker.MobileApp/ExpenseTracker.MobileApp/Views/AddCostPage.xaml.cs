using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCostPage : ContentPage
    {
        public AddCostPage()
        {
            InitializeComponent();
        }

        private void AddCostButton_Clicked(object sender, EventArgs e)
        {
            //(sender as Button).Text = "I was just clicked!";
        }
    }
}