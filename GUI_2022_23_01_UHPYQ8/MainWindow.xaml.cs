﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_2022_23_01_UHPYQ8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game Gm;
        private object content;
        public double volume;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myDisplay.Resize(new Size(myGrid.ActualWidth, myGrid.ActualHeight));
            //if (Gm != null)
            //{
            //    Gm.Window_SizeChanged(sender, e);
            //}
        }
        public void ChangeToGame()
        {
            Gm = new Game(this);
            this.Content = Gm;
        }
        public void ChangeContentToHighscore()
        {
            Highscore Hw = new Highscore(this);
            this.Content = Hw;
        }


    }
}
