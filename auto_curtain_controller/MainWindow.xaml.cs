using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.IO.Ports;

namespace auto_curtain_controller
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

            timer1.Tick += new EventHandler(TimeCycle);
            timer1.Interval = new TimeSpan(0,0,0,1);
            timer1.Start();

            serialPort1.ReceivedBytesThreshold = 1;
            serialPort1.BaudRate = 9600;
            serialPort1.PortName = "COM1";
            serialPort1.Parity = Parity.Even;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataReceived += SerialPort1_DataReceived;

            sysTimeLbl.Content = DateTime.Now.ToString("HH:mm:ss");
            sysDateLbl.Content = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public void TimeCycle(object sender, EventArgs e)
        {
            sysTimeLbl.Content = DateTime.Now.ToString("HH:mm:ss");
            sysDateLbl.Content = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void ComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LinkBtn_Click(object sender, RoutedEventArgs e)
        {
            Window mainwin = Application.Current.MainWindow;

            if (linkBtn.Content == "连接设备")
            {
                NewMethod();
            }
            else {
                serialPort1.Close();
                linkBtn.Content = "连接设备";
            }
        }

        private void NewMethod()
        {
            try
            {
                serialPort1.Open();
                linkBtn.Content = "关闭串口";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string serialReadString = serialPort1.ReadExisting() + "  ";
           // this.recvBox.(new MethodInvoker(delegate { this.recvBox.AppendText(serialReadString); }));

        }

        private void SendClrBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            string serialStringTemp = sendBox.Text;
            serialPort1.WriteLine(serialStringTemp);
        }

        private void sendClrBtn_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
