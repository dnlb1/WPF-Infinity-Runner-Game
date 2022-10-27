using GUI_2022_23_01_UHPYQ8.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_UHPYQ8.ViewModel
{
    public class GameScoreViewModel : ObservableRecipient
    {
        public Player py { get; set; }

        public void SetUp(Player py)
        {
            this.py = py;
        }
        public GameScoreViewModel()
        {

        }
    }
}
