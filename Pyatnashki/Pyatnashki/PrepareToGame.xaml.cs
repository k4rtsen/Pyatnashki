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
    public partial class PrepareToGame : ContentPage
    {

        int lvl;
        bool mode;

        public PrepareToGame()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            btn_start.Clicked += Btn_start_Clicked;
            btn_back.Clicked += Btn_back_Clicked;

            //mode = false;
        }

        private async void Btn_back_Clicked(object sender, EventArgs e)
        {
            // Вернуться назад
            await Navigation.PopToRootAsync(true);
        }

        private async void Btn_start_Clicked(object sender, EventArgs e)
        {
            // Открыть Game
            if (picker_size.SelectedIndex == -1)
                await DisplayAlert("Нажмите ОК", "Пожалуйста выберите размер игрового поля", "ОК");
            else await Navigation.PushAsync(new Game(this.lvl, this.mode));
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (picker_size.SelectedIndex)
            {
                case 0:
                    lvl = 3;
                    break;
                case 1:
                    lvl = 4;
                    break;
                case 2:
                    lvl = 5;
                    break;
            }
        }
    }
}