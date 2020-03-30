using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pyatnashki
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Finish : ContentPage
    {
        public Finish()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            btn_done.Clicked += Btn_done_Clicked;
        }

        private async void Btn_done_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(true);
        }
    }
}