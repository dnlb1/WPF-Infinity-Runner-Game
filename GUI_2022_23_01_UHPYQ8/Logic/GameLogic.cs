using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GUI_2022_23_01_UHPYQ8.Logic
{
    public class GameLogic : IGameLogic
    {
        public GameLogic()
        {

            Intro = false;
            EscON = false;

            hp = 5;


            WaterSound.MediaEnded += WaterSound_MediaEnded;
            HurtMadara.MediaEnded += HurtMadara_MediaEnded;
            Susano.MediaEnded += Susano_MediaEnded;

            Susano.Open(new Uri(System.IO.Path.Combine("Music", "Susano.mp3"), UriKind.RelativeOrAbsolute));
            MainMusic.Open(new Uri(System.IO.Path.Combine("Music", "MainGameTheme.wav"), UriKind.RelativeOrAbsolute));
            HurtMadara.Open(new Uri(System.IO.Path.Combine("Music", "hurtmadara.wav"), UriKind.RelativeOrAbsolute));
            HurtMadara.Volume = 0.9;
            IntroMedia.Open(new Uri(System.IO.Path.Combine("Videos", "Intro.mp4"), UriKind.RelativeOrAbsolute));
            IntroMedia.MediaEnded += IntroMedia_MediaEnded;
        }

        private void HurtMadara_MediaEnded(object sender, EventArgs e)
        {
            HurtMadara.Stop();
            HurtMadara.Position = TimeSpan.Zero;
        }
        private void WaterSound_MediaEnded(object sender, EventArgs e)
        {
            WaterSound.Stop();
            WaterSound.Position = TimeSpan.Zero;
        }
        private void Susano_MediaEnded(object sender, EventArgs e)
        {
            Susano.Stop();
            Susano.Position = TimeSpan.Zero;
        }
        private void IntroMedia_MediaEnded(object sender, EventArgs e)
        {
            Intro = true;
        }
        public void Resize(Size size)
        {
            this.size = size;
        }
        Size size;
        public string Name { get; set; }
        public bool Intro { get; set; }
        public bool gameOver { get; set; } //vége van-e a játéknak
        public bool EscON { get; set; }

        public int score { get; set; }
        public int hp { get; set; }
        public MediaPlayer IntroMedia { get; set; } = new MediaPlayer();
        public MediaPlayer MainMusic { get; set; } = new MediaPlayer();
        public MediaPlayer clicksound { get; set; } = new MediaPlayer();
        public MediaPlayer WaterSound { get; set; } = new MediaPlayer();
        public MediaPlayer KatonFireStyle { get; set; } = new MediaPlayer();
        public MediaPlayer Susano { get; set; } = new MediaPlayer();
        public MediaPlayer HurtMadara { get; set; } = new MediaPlayer();

        public event EventHandler Changed; //változott a background. akkor ÚJRA RAJZOLUNK
        public enum ControlKey
        {
            space,
            skillone,
            skilltwo,
            enter, //később new game
            esc //back to main menu
        }
        public void Control(ControlKey key)
        {
            switch (key)
            {
                case ControlKey.space:
                   
                    break;
                case ControlKey.skillone:
                    
                    break;
                case ControlKey.enter:
                    
                    break;
                case ControlKey.esc:
                   
                    break;
                case ControlKey.skilltwo:
                    
                    break;

                default:
                    break;
            }
        }

        public void GameEngine()
        {
            if (hp >1)
            {
                if (!EscON)
                {

                }
            }
        }


    }
}
