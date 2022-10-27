using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GUI_2022_23_01_UHPYQ8.ViewModel
{
    public class NameWindowViewModel : ObservableRecipient
    {
        public bool IsPressed;
        private string enteredName;

        public string EnteredName
        {
            get { return enteredName; }
            set 
            { 
                enteredName = value; 
                (Ok as RelayCommand).NotifyCanExecuteChanged(); 
            }
        }

        public ICommand Ok { get; set; }

        public static bool IsInDesign
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public NameWindowViewModel(Window window)
        {

        }
    }
}
