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
    public partial class Game : ContentPage
    {
        List<Image> imageDrawable;
        Image[,] imageList;
        Image[] images;
        int lvl;
        bool isStartGame = false;

        Grid grid = new Grid()
        {
            ColumnSpacing = 2,
            RowSpacing = 2,
            Margin = new Thickness(15, 0, 15, 0),
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        public Game(int lvl, bool mode)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.lvl = lvl;
            images = new Image[lvl * lvl];
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) => {
                // handle the tap
                tapGestureRecognizer_Tapped(s, e);
            };
            for (int i = 1; i <= lvl * lvl-1; i++)
            {
                images[i-1] = new Image { 
                    Source = "i" + i + ".png"
                };
                images[i - 1].GestureRecognizers.Add(tapGestureRecognizer);
            }
            images[lvl*lvl-1] = new Image
            {
                Source = "izero.png"
            };
            images[lvl*lvl-1].GestureRecognizers.Add(tapGestureRecognizer);
            

            fillTheArrayImages();
            imageDrawable = new List<Image>();
            setUpGame();
            isStartGame = true;

            for (int i = 0; i < lvl; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star)
                });
            }
            for (int i = 0; i < lvl; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
            }
            if (!mode) classicMode(lvl);
            else
            {
                //photoMode(lvl);
            }

            Label label1 = new Label()
            {
                Text = "Ходов: 0 Время: 00:30",
                FontSize = 24,
                Margin = new Thickness(15, 20, 15, 20),
                Padding = new Thickness(0,5,0,5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.FromHex("cc9a37"),
                BackgroundColor = Color.White
            };

            StackLayout stackLayout = new StackLayout()
            {
                Margin = new Thickness(0, 0, 0, 0),
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { label1, grid }
            };

            Button button = new Button
            {
                Text = "Финиш",
                FontSize = 22,
                CornerRadius = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("cc9a37"),
                BackgroundColor = Color.White,
                Margin = new Thickness(15,50,15,30),
                VerticalOptions = LayoutOptions.EndAndExpand
            };

            button.Clicked += backToMainPage;

            StackLayout mainStackLayout = new StackLayout()
            {
                BackgroundColor = Color.FromHex("cc9a37"),
                Margin = new Thickness(0, 0, 0, 0),
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { stackLayout, button }
            };

            this.Content = mainStackLayout;
        }

        private async void backToMainPage(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync(true);
        }

        private void tapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (isStartGame == false) return;
            Image image = (Image)sender;
            var selectedImage = image.Source as FileImageSource;

            if (selectedImage.File != "izero.png")
            {
                for (int x = 0; x < lvl; x++)
                {
                    for (int y = 0; y < lvl; y++)
                    {
                        if (imageList[x, y] == image)
                        {
                            checkNeighbours(x, y);
                        }
                    }
                }
            }
            checkWin();
        }

        private async void checkWin()
        {
            int countSum = 0;
            for (int i = 0; i < lvl*lvl-1; i++)
            {
                if (images[i].Source.ToString() == imageDrawable[i].Source.ToString())
                {
                    countSum++;
                }
            }
            if (countSum == lvl*lvl-1)
            {
                isStartGame = false;
                await Navigation.PushAsync(new Finish());
            }
        }

        private void checkNeighbours(int xB, int yB)
        {
            for (int x = xB - 1; x <= xB + 1; x++)
            {
                for (int y = yB - 1; y <= yB + 1; y++)
                {
                    if (x >= 0 && x < lvl && y >= 0 && y < lvl && (xB == x || yB == y))
                    {
                        var fileImage = imageList[x, y].Source as FileImageSource;
                        if (fileImage.File == "izero.png")
                        {
                            imageList[x, y].Source = imageList[xB, yB].Source;
                            imageList[xB, yB].Source = "izero.png";
                        }
                    }
                }
            }
        }

        private void setUpGame()
        {
            for (int i = 1; i <= lvl * lvl-1; i++)
            {
                //Image im = new Image { Source = "i" + i + ".png" };
                //imageDrawable.Add(im);
                imageDrawable.Add(new Image { Source = "i" + i + ".png" });
            }
            imageDrawable.Add(new Image { Source = "izero.png" });
            Random rnd = new Random();
            for (int x = 0; x < lvl; x++)
            {
                for (int y = 0; y < lvl; y++)
                {
                    int randomImageIndex = rnd.Next(0, imageDrawable.Count);
                    imageList[x, y].Source = (imageDrawable[randomImageIndex]).Source;
                    imageDrawable.RemoveAt(randomImageIndex);
                }
            }
            for (int i = 1; i <= lvl * lvl-1; i++)
            {
                //imageDrawable.Add(images[i-1]);
                imageDrawable.Add(new Image { Source = "i" + i + ".png" });
            }
            imageDrawable.Add(new Image { Source = "izero.png" });            
        }

        private void fillTheArrayImages()
        {
            imageList = new Image[lvl, lvl];
            int ij = 0;
            for (int i = 0; i < lvl; i++)
            {
                for (int j = 0; j < lvl; j++)
                {
                    imageList[j, i] = images[ij];
                    ij++;
                }
            }
        }

        public void classicMode(int Sise)
        {
            int N = Sise * Sise;
            int ij = 0;
            for (int i = 0; i < Sise; i++)
            {
                for (int j = 0; j < Sise; j++)
                {
                    grid.Children.Add(images[ij], j, i);
                    ij++;
                }
            }
        }
    }
}