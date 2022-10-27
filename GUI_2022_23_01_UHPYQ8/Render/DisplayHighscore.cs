using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_2022_23_01_UHPYQ8.Render
{
    public class DisplayHighscore : FrameworkElement
    {
        Size size { get; set; }
        public MediaPlayer player = new MediaPlayer();

        ImageBrush brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "scroll.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
    }
}
