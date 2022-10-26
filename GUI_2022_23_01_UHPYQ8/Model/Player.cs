using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_UHPYQ8.Model
{
    public class Player
    {
        public int Rank { get; set; } = 1;
        public string Name { get; set; }
        public DateTime WhenStarted { get; set; } = DateTime.Now;
        public int Score { get; set; }
    }
}
