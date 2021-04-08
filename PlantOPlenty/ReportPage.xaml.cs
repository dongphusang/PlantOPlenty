using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlantOPlenty
{
    public partial class ReportPage : ContentPage
    {
        public ReportPage()
        {
            InitializeComponent();
            var reportPage = new ReportPageData();
        }

        // refresh page
        public void MeasureLighting_Clicked(object sender, EventArgs e)
        {
            InitializeComponent();
        }

    }
}
