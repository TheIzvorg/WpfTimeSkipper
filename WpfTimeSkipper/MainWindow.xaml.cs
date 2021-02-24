using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTimeSkipper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public long ticker = 0;
        public DateTime Datetime;
        public int Resin;
        bool Flag = true;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0,0,1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            long SecondForResinCap = (160 - Resin) * 8 * 60 - ticker % 60 - 1;
            if (Resin != 160)
            {
                Result_tb.Text = $"{SecondForResinCap/3600:d2}:{SecondForResinCap/60%60:d2}:{SecondForResinCap%60:d2}";
                tbResinTimer.Text = $"{7-(ticker/60)%8:d2}:{59-ticker%60:d2}";
                tbResinCounter.Text = $"Смолы: {Resin}/160";
                if (7 - (ticker/60%8) == 0 && 59 - ticker%60 == 0 && Resin < 160)
                {
                    Resin++;
                }
                ticker++;
            }
            else
            {
                Result_tb.Text = $"Достигнуто максимальное";
                tbResinTimer.Text = $"Количество смолы";
                tbResinCounter.Text = $"Смолы: {Resin}/160";
                ticker = 0;
            }
            if (Resin >= 159 && Flag)
            {
                Flag = false;
                Alert alert = new Alert(Flag);
                alert.Show();
                Flag = alert.getFlag();
            }
        }

        private void Start_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamReader sr = new StreamReader("timesave"))
                {
                    Datetime = Convert.ToDateTime(sr.ReadLine());
                    ticker = Convert.ToInt64(sr.ReadLine());
                    //$"{7 - (ticker / 60) % 8:d2}:{59 - ticker % 60:d2}"
                    Resin = Convert.ToInt32(sr.ReadLine());
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            ticker += (long)(DateTime.UtcNow - Datetime).TotalSeconds;
            for (int i = 0; i < (ticker)/60/8; i++)
            {
                if ((ticker / 60 / 8) > 0 && ticker / 60 > 0 && Resin < 160)
                {
                    Resin++;
                }
            }
            btnMinusResin.IsEnabled = true;
            timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("timesave"))
                {
                    sw.WriteLine(DateTime.UtcNow);
                    sw.WriteLine(ticker);
                    sw.WriteLine(Resin);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMinusResin_Click(object sender, RoutedEventArgs e)
        {
            if(Resin >= 20)
            {
                Resin -= 20;
            }
            else if (Resin < 0)
            {
                Resin = 0;
            }
        }
    }
}
