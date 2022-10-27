using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_UHPYQ8.Service
{
    public class Name 
    {
        public bool DialogResult { get; set; }
        public string GetName()
        {
            YourName nw = new YourName();
            nw.ShowDialog();
            DialogResult = (bool)nw.DialogResult;
            return nw.VMName.EnteredName;
        }
    }
}
