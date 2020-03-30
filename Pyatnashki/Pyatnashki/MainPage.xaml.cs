using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pyatnashki
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            btn_start.Clicked += Btn_start_Clicked;
            btn_result.Clicked += Btn_result_Clicked;
            btn_exit.Clicked += Btn_exit_Clicked; 
        }

        private void Btn_exit_Clicked(object sender, EventArgs e)
        {
            // Закрыть приложение.
        }

        private async void Btn_result_Clicked(object sender, EventArgs e)
        {
            // Открыть Result
            await Navigation.PushAsync(new Result());
        }

        private async void Btn_start_Clicked(object sender, EventArgs e)
        {
            // Открыть PrepareToGame
            await Navigation.PushAsync(new PrepareToGame());
        }
    }
}
