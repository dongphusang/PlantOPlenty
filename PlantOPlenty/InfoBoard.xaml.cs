using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlantOPlenty
{
    public partial class InfoBoard : ContentPage
    {
        public InfoBoard()
        {
            var plantData = new InfoBoardData(); // initialize InforBoardData
            InitializeComponent();
        }

        async void MeasureLighting_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReportPage());
        }




    }
}
