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
        public static string PlantName 
        { 
            get
            {
                return PlantName;
            }
            set { PlantName = value; }
        } 


        public Menu()
        {
            InitializeComponent();
        }
        
        async void Section_Clicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InfoBoard());
            //PlantName = ((Button)sender).Text;    // the cause of crashes. Solution: find another way to get variable name

        }

  
    }
}
