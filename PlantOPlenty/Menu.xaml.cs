using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlantOPlenty
{
    public partial class Menu : ContentPage
    {
        

        public Menu()
        {
            InitializeComponent();
        }
        
        async void Section_Clicked (object sender, EventArgs e)
        {
            InfoBoardData.PlantName = ((Button)sender).Text; // getting info of which plant the user is interested, register the plant in InfoBoardData class
            await Navigation.PushAsync(new InfoBoard());
        }

  
    }
}
