using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_2022_23_01_UHPYQ8.ViewModel
{
    public class NameWindowViewModel : ObservableRecipient
    {
        public bool IsPressed;
        private string enteredName;

        public string EnteredName
        {
            get { return enteredName; }
            set { enteredName = value;  }
        }
    }
}
