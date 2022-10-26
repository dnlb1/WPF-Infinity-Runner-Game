using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_2022_23_01_UHPYQ8.Render
{
    public class Display : FrameworkElement
    {
        Size size;
        public void Resize(Size size)
        {
            this.size = size;
            InvalidateVisual();
        }
    }
}
