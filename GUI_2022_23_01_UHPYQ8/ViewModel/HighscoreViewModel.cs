using GUI_2022_23_01_UHPYQ8.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    }
}
