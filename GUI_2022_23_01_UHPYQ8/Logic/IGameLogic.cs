using System.Windows;

namespace GUI_2022_23_01_UHPYQ8.Logic
{
    public interface IGameLogic
    {
        void Resize(Size size);
        string Name { get; set; }
    }
}