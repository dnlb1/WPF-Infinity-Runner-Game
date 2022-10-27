using GUI_2022_23_01_UHPYQ8.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_UHPYQ8.Service
{
    public class Score : IScore
    {
        public void GameOver(Player Py)
        {
            new GameScore(Py).ShowDialog();
        }
    }
}
