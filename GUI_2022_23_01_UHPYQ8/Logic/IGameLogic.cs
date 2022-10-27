using System;
using System.Windows;
using System.Windows.Media;

namespace GUI_2022_23_01_UHPYQ8.Logic
{
    public interface IGameLogic
    {
        void Resize(Size size);
        string Name { get; set; }
        bool Intro { get; set; }
        MediaPlayer IntroMedia { get; set; }
        event EventHandler Changed;
    }
}