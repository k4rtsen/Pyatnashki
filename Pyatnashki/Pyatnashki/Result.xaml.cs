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
    public partial class Result : ContentPage
    {
        public Result()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            btn_done.Clicked += Btn_done_Clicked;
            btn_reset_result.Clicked += Btn_reset_result_Clicked;
        }

        private void Btn_reset_result_Clicked(object sender, EventArgs e)
        {
            // Сбросить результаты
        }

        private async void Btn_done_Clicked(object sender, EventArgs e)
        {
            // Вернуться назад, в меню
            await Navigation.PopToRootAsync(true);
        }
    }
}