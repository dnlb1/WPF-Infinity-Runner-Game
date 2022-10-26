using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GUI_2022_23_01_UHPYQ8.Render
{
    public class Display : FrameworkElement
    {
        Size size;
        public MediaPlayer player = new MediaPlayer();
        public void Resize(Size size)
        {
            this.size = size;
            InvalidateVisual();
        }
        public void VolumeChanger(double Volume)
        {
            player.Volume = Volume;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            //Ez a MainMenu - videó rajza
            if (size.Width > 0 && size.Height > 0)
            {
                if (!(player.Position > TimeSpan.Zero))
                {
                   MediaTimeline timeline = new MediaTimeline(new Uri(Path.Combine("Videos", "MadaraIntro.mp4"), UriKind.RelativeOrAbsolute));
                    timeline.RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever;
                    MediaClock clock = timeline.CreateClock();
                    player.Clock = clock;
                }
                drawingContext.DrawVideo(player, new Rect(0, 0, size.Width, size.Height));
            }
        }
    }
}
