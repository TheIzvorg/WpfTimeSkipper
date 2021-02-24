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

namespace WpfTimeSkipper
{
    /// <summary>
    /// Логика взаимодействия для Alert.xaml
    /// </summary>
    /// 
    public partial class Alert : Window
    {
        bool Flag;
        public Alert(bool Flag)
        {
            InitializeComponent();
            this.Flag = !Flag;
        }

        public bool getFlag()
        {
            return Flag;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
