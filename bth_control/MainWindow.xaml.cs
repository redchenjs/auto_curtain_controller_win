using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.IO.Ports;

namespace bth_control
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SerialPort serialPort1 = new SerialPort();

        public MainWindow()
        {
            DispatcherTimer timer1 = new DispatcherTimer();
            
            InitializeComponent();

            timer1.Tick += new EventHandler(timeCycle);
            timer1.Interval = new TimeSpan(0,0,0,1);
            timer1.Start();

            serialPort1.ReceivedBytesThreshold = 1;
            serialPort1.BaudRate = 9600;
            serialPort1.PortName = "COM1";
            serialPort1.Parity = Parity.Even;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataReceived += serialPort1_DataReceived;

            this.sysTimeLbl.Content = DateTime.Now.ToString("HH:mm:ss");
            this.sysDateLbl.Content = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public void timeCycle(object sender, EventArgs e)
        {
            this.sysTimeLbl.Content = DateTime.Now.ToString("HH:mm:ss");
            this.sysDateLbl.Content = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void comBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void linkBtn_Click(object sender, RoutedEventArgs e)
        {
            Window mainwin = Application.Current.MainWindow;

            if (linkBtn.Content == "连接设备") {
                try {
                    serialPort1.Open();
                    linkBtn.Content = "关闭串口";

                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                serialPort1.Close();
                linkBtn.Content = "连接设备";
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string serialReadString = serialPort1.ReadExisting() + "  ";
           // this.recvBox.(new MethodInvoker(delegate { this.recvBox.AppendText(serialReadString); }));

        }

        private void sendClrBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            string serialStringTemp = this.sendBox.Text;
            serialPort1.WriteLine(serialStringTemp);
        }
    }
}
