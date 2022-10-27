using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public void Resize(Size size)
        {
            this.size = size;
            InvalidateVisual();
        }
        public static bool IsInDesign
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (!IsInDesign)
            {
                if (!(player.Position > TimeSpan.Zero))
                {
                    MediaTimeline timeline = new MediaTimeline(new Uri(Path.Combine("Music", "ListMusic.wav"), UriKind.RelativeOrAbsolute));
                    timeline.RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever;
                    MediaClock clock = timeline.CreateClock();
                    player.Clock = clock;
                }                
            }
        }
    }
}
