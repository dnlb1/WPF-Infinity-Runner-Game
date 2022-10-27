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
                    if (model.Katon)
                    {
                        model.KatonFireStyle.Open(new Uri(System.IO.Path.Combine("Music", "Katon.wav"), UriKind.RelativeOrAbsolute));
                        model.KatonFireStyle.Play();
                        model.Katon = false;
                    }

                    //Első background
                    drawingContext.DrawRectangle(
                        model.Background,
                        null,
                        new Rect(0 - model.BackgroundMoveFirst, 0, 3450, size.Height));

                    //Második background
                    drawingContext.DrawRectangle(
                      model.Background,
                      null,
                     new Rect(3450 - model.BackgroundMoveSecond, 0, 3450, size.Height));

                    //ManaBar
                    drawingContext.DrawRectangle(
                       model.ManaBar,
                       null,
                       new Rect(0, 0, size.Width / 4, size.Height / 8));

                    //HPBar
                    drawingContext.DrawRectangle(
                       model.HPBar,
                       null,
                       new Rect(0, 0, size.Width / 4, size.Height / 8));


                    //Press Start
                    if (!model.IsStanding)
                    {
                        drawingContext.DrawRectangle(
                        model.PressStart,
                        null,
                        new Rect(size.Width / 4, size.Height / 4, size.Width / 1.92, size.Height / 2));
                    }

                    if (model.hp != 1)
                    {
                        //Madara
                        if (!model.drawform)
                        {
                            drawingContext.DrawRectangle(
                         model.playerSprite,
                         null,
                        new Rect(model.X, model.Y, size.Height / 4, size.Height / 4)); //x és y-nak maradnia kell
                        }
                        else // Form - Susano
                        {
                            drawingContext.DrawRectangle(
                         model.playerSprite,
                         null,
                        new Rect(0, size.Height / 13, size.Height / 1.8, size.Height / 1.375)); //x és y-nak maradnia kell
                        }


                    }
                    else
                    {
                        drawingContext.DrawRectangle(
                        model.MadaraDead,
                        null,
                        new Rect(size.Width / 8.9, size.Height / 1.45, size.Height / 3.5, size.Height / 5)); //x és y-nak maradnia kell
                    }

                    //Pecsét Jutsu
                    if (model.hp == 1)
                    {
                        drawingContext.DrawRectangle(
                       model.SkillDead,
                      null,
                       new Rect(0, size.Height / 2.5, size.Width / 2.335, size.Height / 1.725));
                    }

                    //Láthatatlan föld!
                    drawingContext.DrawRectangle(
                     Brushes.Transparent,
                    null,
                    new Rect(0, model.groundheight, size.Width, 1));

                    //Shuriken - Enemy
                    if (model.hp != 1)
                    {
                        if (!model.DisapearShuriken) //és csak akkor spawnolódhat le, hogyha nincs enemy 
                        {
                            drawingContext.DrawRectangle(
                        model.obstacleSprite,
                        null,
                        new Rect(model.ObstacleX, model.ObstacleY, size.Width / 10, size.Height / 10)); //x-y változik
                        }

                        //Mana
                        if (model.ManaChanche && !model.DisapearMana && !model.IsInForm)
                        {
                            drawingContext.DrawRectangle(
                        model.Mana,
                        null,
                        new Rect(model.ManaX, model.ManaY, size.Width / 10, size.Height / 10)); //x-y változik
                        }

                        //Enemy -- legyen 1 olyan boolja, hogy meghalt-e
                        if (model.youcanspawnenemy)
                        {
                            drawingContext.DrawRectangle(
                        model.EnemySprite,
                        null,
                        new Rect(model.EnemyX, model.EnemyY, size.Width / 3, size.Width / 3));
                        }
                        //SkillOne
                        if (model.SkillShoot)
                        {
                            drawingContext.DrawRectangle(
                          model.SkillOne,
                           null,
                          new Rect(model.SkillShootX, model.SkillShootY, size.Width / 3 + 20, size.Height / 3 + 20));
                        }
                    }

                    //Esc Gomb
                    if (model.EscON)
                    {
                        drawingContext.DrawRectangle(
                    model.Esc,
                     null,
                    new Rect(0, 0, size.Width, size.Height));
                    }

                    if (model.gameOver && model.EscON)
                    {
                        drawingContext.DrawRectangle(
                    model.Esc,
                    null,
                    new Rect(0, 0, size.Width, size.Height));
                    }
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
