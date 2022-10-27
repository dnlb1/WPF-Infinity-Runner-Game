﻿using System;
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
            IntroMedia.Open(new Uri(System.IO.Path.Combine("Videos", "Intro.mp4"), UriKind.RelativeOrAbsolute));
            IntroMedia.MediaEnded += IntroMedia_MediaEnded;
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
        public MediaPlayer IntroMedia { get; set; } = new MediaPlayer();
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

    }
}
