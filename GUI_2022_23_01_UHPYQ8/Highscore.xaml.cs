using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Highscore.xaml
    /// </summary>
    public partial class Highscore : Page
    {
        MainWindow window;
        public Highscore(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            if (myDisplay.player != null)
            {
                myDisplay.player.Volume = window.volume;
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myDisplay.Resize(new Size(myGrid.ActualWidth, myGrid.ActualHeight));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myDisplay.player.Clock = null;
            myDisplay.player.Stop();
            window.GoBackToStartPage();
        }
    }
}
