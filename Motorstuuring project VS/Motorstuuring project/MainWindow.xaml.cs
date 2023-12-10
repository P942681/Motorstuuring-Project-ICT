using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Motorstuuring_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Motor Motor;

        SerialPort _serialPort;

        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;

            Motor = new Motor();

            cbxPortName.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxPortName.Items.Add(s);

            _serialPort = new SerialPort();
            _serialPort.BaudRate = 9600;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Add)
            {
                sldrSnelheid.Value += 1;
            }
            else if (e.Key == Key.Subtract)
            {
                sldrSnelheid.Value -= 1;
            }
        }

        private void cbxPortName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_serialPort != null)
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                if (cbxPortName.SelectedItem.ToString() != "None")
                {
                    _serialPort.PortName = cbxPortName.SelectedItem.ToString();
                    _serialPort.Open();
                    sldrSnelheid.IsEnabled = true;
                    txtbx.IsEnabled = true;
                    btnAchteruit.IsEnabled = true;
                    btnVooruit.IsEnabled = true;
                    btnLinks.IsEnabled = true;
                    btnRechts.IsEnabled = true;
                    sldrSnelheid.Value = 0;
                    txtbx.Text = "0";
                }
                else
                {
                    sldrSnelheid.IsEnabled = false;
                    txtbx.IsEnabled = false;
                    btnAchteruit.IsEnabled = false;
                    btnVooruit.IsEnabled = false;
                    btnLinks.IsEnabled = false;
                    btnRechts.IsEnabled = false;
                }
            }
        }

        private void sldrSnelheid_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            txtbx.Text = sldrSnelheid.Value.ToString();
            SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
            Motor.Stop();
            btnVooruit.Focus();

        }

        private void txtbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (double.TryParse(txtbx.Text, out double value))
                {
                    sldrSnelheid.Value = value;
                }
                else
                {
                    MessageBox.Show("Geef een juiste waarde in", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                btnVooruit.Focus();
            }
        }

        private void btnLinks_Click(object sender, RoutedEventArgs e)
        {
            Motor.links();
            SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
            Motor.Stop();
        }

        private void btnVooruit_Click(object sender, RoutedEventArgs e)
        {
            Motor.vooruit();
            SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
            Motor.Stop();

        }

        private void btnAchteruit_Click(object sender, RoutedEventArgs e)
        {
            Motor.achteruit();
            SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
            Motor.Stop();
        }

        private void btnRechts_Click(object sender, RoutedEventArgs e)
        {
            Motor.rechts();
            SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
            Motor.Stop();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                Motor.vooruit();
                SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
                Motor.Stop();
            }
            if (e.Key == Key.Down)
            {
                Motor.achteruit();
                SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
                Motor.Stop();
            }
            if (e.Key == Key.Left)
            {
                Motor.links();
                SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
                Motor.Stop();
            }
            if (e.Key == Key.Right)
            {
                Motor.rechts();
                SendValue((int)sldrSnelheid.Value, Motor.Richting.ToString());
                Motor.Stop();
            }
        }

        public void SendValue(int snelheid, string richting)
        {
            _serialPort.WriteLine(snelheid.ToString() + "\r");
            Thread.Sleep(50);
            _serialPort.WriteLine(richting + "\r");
            Thread.Sleep(50);
        }
    }
}
