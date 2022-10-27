using GUI_2022_23_01_UHPYQ8.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI_2022_23_01_UHPYQ8.ViewModel
{
    public class HighscoreViewModel
    {
        public ObservableCollection<Player> Runners { get; set; }
        public static bool IsInDesign
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public HighscoreViewModel()
        {
            if (!IsInDesign)
            {
                Runners = new ObservableCollection<Player>();
                //valahonnan majd betöltjük a ranglistát
                //Azokból létrehozunk 1 runner tömböt
                if (File.Exists(Path.Combine("RankList", "Rank.txt")))
                {
                    string[] datas = File.ReadAllLines(Path.Combine("RankList", "Rank.txt"));

                    for (int i = 0; i < datas.Length; i++)
                    {
                        string[] split = datas[i].Split("#");
                        Player r = new Player();
                        r.Rank = int.Parse(split[0]);
                        r.WhenStarted = DateTime.Parse(split[1]);
                        r.Name = split[2];
                        r.Score = int.Parse(split[3]);
                        Runners.Add(r);
                    }
                    Runners = new ObservableCollection<Player>(Runners.OrderByDescending(t => t.Score));
                    for (int i = 0; i < Runners.Count; i++)
                    {
                        Runners[i].Rank = i + 1;
                    }
                }
            }
        }
    }
}
