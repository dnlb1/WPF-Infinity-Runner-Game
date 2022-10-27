﻿using GUI_2022_23_01_UHPYQ8.Model;
using GUI_2022_23_01_UHPYQ8.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace GUI_2022_23_01_UHPYQ8
{
    /// <summary>
    /// Interaction logic for GameScore.xaml
    /// </summary>
    public partial class GameScore : Window
    {
        Player py;
        public GameScore(Player py)
        {
            InitializeComponent();
            var vm = new GameScoreViewModel();
            this.py = py;
            vm.SetUp(py);
            this.DataContext = vm;
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //Mentse ki TXT-be
            string Save = py.Rank + "#" + py.WhenStarted + "#" + py.Name + "#" + py.Score + "\r";
            File.AppendAllText(System.IO.Path.Combine("RankList", "Rank.txt"), Save);
            DialogResult = true;
        }
    }
}
