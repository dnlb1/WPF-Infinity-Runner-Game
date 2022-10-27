using System;
using System.Windows;
using System.Windows.Media;
using static GUI_2022_23_01_UHPYQ8.Logic.GameLogic;

namespace GUI_2022_23_01_UHPYQ8.Logic
{
    public interface IGameLogic
    {
        public void GameEngine();
        void Control(ControlKey key);
        void Resize(Size size);
        string Name { get; set; }
        bool Intro { get; set; }
        bool gameOver { get; set; }
        bool EscON { get; set; }
        int score { get; set; }
        MediaPlayer IntroMedia { get; set; }
        MediaPlayer MainMusic { get; set; }
        MediaPlayer HurtMadara { get; set; }
        MediaPlayer WaterSound { get; set; }
        MediaPlayer KatonFireStyle { get; set; }
        event EventHandler Changed;
    }
}