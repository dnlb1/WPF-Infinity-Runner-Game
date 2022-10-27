using GUI_2022_23_01_UHPYQ8.Logic;
using GUI_2022_23_01_UHPYQ8.Model;
using GUI_2022_23_01_UHPYQ8.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GUI_2022_23_01_UHPYQ8
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        MainWindow window;
        SoundPlayer clicksound = new SoundPlayer(System.IO.Path.Combine("Music", "ClickSound.wav"));
        IName name;
        IGameLogic logic;
        IScore ScoreService;
        DispatcherTimer dt;
        public Game(MainWindow w)
        {
            InitializeComponent();
            this.window = w;
            this.logic = new GameLogic();
            this.ScoreService = new Score();
            this.name = new Name();
            menuGrid.Visibility = Visibility.Hidden;
        }

        private void Engine(object sender, EventArgs e)
        {
            if (!logic.gameOver)
            {
                logic.GameEngine();
            }
            else
            {
                dt.Stop();
                //GameOver
                if (logic.gameOver)
                {
                    Player ActualPlayer = new Player()
                    {
                        Name = logic.Name,
                        Score = logic.score ///////
                    };
                    logic.MainMusic.Stop();
                    window.GoBackToStartPage();
                }
            }
        }

        public void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (window.WindowState == WindowState.Maximized)
            {
                ResumeButton.Margin = new Thickness(400, 10, 400, 10);
                ExitButton.Margin = new Thickness(400, 10, 400, 10);
                WindowModeButton.Margin = new Thickness(100, 10, 100, 10);
            }
            if (window.WindowState != WindowState.Maximized)
            {
                ExitButton.Margin = new Thickness(150, 10, 150, 10);
                ResumeButton.Margin = new Thickness(150, 10, 150, 10);
                WindowModeButton.Margin = new Thickness(40, 10, 40, 10);
            }
            myDisplay.Resize(new Size(myGrid.ActualWidth, myGrid.ActualHeight));
            if (logic != null)
            {
                logic.Resize(new Size(myGrid.ActualWidth, myGrid.ActualHeight));
            }
        }
        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(30);
            dt.Tick += Engine;
            
            window.Hide();
            logic.Name = name.GetName();
            if (name.DialogResult)
            {
                logic.IntroMedia.Play();
                myDisplay.Resize(new Size(myGrid.ActualWidth, myGrid.ActualHeight));
                logic.Resize(new Size(myGrid.ActualWidth, myGrid.ActualHeight));
                myDisplay.SetUp(logic);
                dt.Start();
            }
            else
            {
                window.GoBackToStartPage();
            }
            window.Show();
        }
        public void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logic.Control(GameLogic.ControlKey.enter);
            }
            else if (e.Key == Key.D1)
            {
                logic.Control(GameLogic.ControlKey.skillone);
            }
            else if (e.Key == Key.Escape)
            {
                logic.Control(GameLogic.ControlKey.esc);
                if (menuGrid.Visibility == Visibility.Hidden)
                {
                    menuGrid.Visibility = Visibility.Visible;
                }
                else
                {
                    menuGrid.Visibility = Visibility.Hidden;
                }
            }
            else if (e.Key == Key.D2)
            {
                logic.Control(GameLogic.ControlKey.skilltwo);
            }
        }
        public void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                logic.Control(GameLogic.ControlKey.space);
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (logic != null)
            {
                Volume.Value = 0;
                logic.MainMusic.Volume = 0;
                logic.WaterSound.Volume = 0;
                logic.KatonFireStyle.Volume = 0;
                logic.IntroMedia.Volume = 0;
                logic.HurtMadara.Volume = 0;
            }
        }
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (logic != null)
            {
                Volume.Value = 100;
                logic.MainMusic.Volume = 0.5;
                logic.WaterSound.Volume = 1;
                logic.KatonFireStyle.Volume = 1;
                logic.IntroMedia.Volume = 1;
                logic.HurtMadara.Volume = 1;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) //Vigyen vissza Main-ba
        {
            clicksound.Play();
            logic.MainMusic.Stop(); 
            logic.IntroMedia.Stop(); 
            logic.WaterSound.Stop(); 
            logic.KatonFireStyle.Stop(); 
            dt.Stop(); 
            window.GoBackToStartPage();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //Resume 
        {
            menuGrid.Visibility = Visibility.Hidden;
            logic.Control(GameLogic.ControlKey.esc);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }
        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (logic != null)
            {
                logic.MainMusic.Volume = (Volume.Value / 100) / 2;
                logic.WaterSound.Volume = Volume.Value / 100;
                logic.KatonFireStyle.Volume = Volume.Value / 100;
                logic.IntroMedia.Volume = Volume.Value / 100;
                logic.HurtMadara.Volume = Volume.Value / 100;
            }
        }
    }
}
