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

        public void SetUp(IGameLogic model)
        {
            this.model = model;
            model.Changed += Model_Changed;
        }
        private void Model_Changed(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (model != null && size.Width > 0 && size.Height > 0)
            {
                if (model.Intro)
                {

                }
                else
                {
                    drawingContext.DrawVideo(
                       model.IntroMedia,
                       new Rect(0, 0, size.Width, size.Height));
                }
            }
        }
    }
}
