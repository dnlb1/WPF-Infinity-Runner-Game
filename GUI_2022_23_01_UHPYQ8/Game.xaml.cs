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

namespace GUI_2022_23_01_UHPYQ8
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        MainWindow window;
        SoundPlayer clicksound = new SoundPlayer(System.IO.Path.Combine("Music", "ClickSound.wav"));
        public Game(MainWindow w)
        {
            InitializeComponent();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
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
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
        }
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void Button_Click(object sender, RoutedEventArgs e) //Vigyen vissza Main-ba
        {
            window.GoBackToStartPage();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) //Resume 
        {
            menuGrid.Visibility = Visibility.Hidden;
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
           
        }
    }
}
