using GUI_2022_23_01_UHPYQ8.ViewModel;
using System;
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
using System.Windows.Shapes;

namespace GUI_2022_23_01_UHPYQ8
{
    /// <summary>
    /// Interaction logic for YourName.xaml
    /// </summary>
    public partial class YourName : Window
    {
        public NameWindowViewModel VMName { get; set; }
        public YourName()
        {
            InitializeComponent();
            VMName = new NameWindowViewModel(this);
            this.DataContext = VMName;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((VMName.EnteredName == "" || VMName.EnteredName == null) || VMName.IsPressed == false)
            {
                DialogResult = false;
            }
            else
            {
                DialogResult = true;
            }
        }
    }
}
