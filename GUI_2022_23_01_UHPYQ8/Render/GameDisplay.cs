using GUI_2022_23_01_UHPYQ8.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GUI_2022_23_01_UHPYQ8.Render
{
    public class GameDisplay : FrameworkElement
    {
        Size size;
        IGameLogic model;
        public void Resize(Size size)
        {
            this.size = size;
            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }
}
