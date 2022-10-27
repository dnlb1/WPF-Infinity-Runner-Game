using System;
using System.Windows;
using System.Windows.Media;
using static GUI_2022_23_01_UHPYQ8.Logic.GameLogic;

namespace GUI_2022_23_01_UHPYQ8.Logic
{
    public interface IGameLogic
    {
        void Control(ControlKey key);
        void Resize(Size size);
        string Name { get; set; }
        bool Intro { get; set; }
        MediaPlayer IntroMedia { get; set; }
        event EventHandler Changed;
    }
}