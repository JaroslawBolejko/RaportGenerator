using RaportGenerator.Models;
using RaportGenerator.ViewModels;
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

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for DeviceDetailsView.xaml
    /// </summary>
    public partial class DeviceDetailsView : Window
    {
        public DeviceDetailsView()
        {
            InitializeComponent();
        }

        public DeviceModel device;

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (device != null && device.Id != 0)
            {
                txtDeviceName.Text = device.DeviceName;
                txtSerialNumber.Text = device.SerialNumber;
                txtBlackWhitePrintStatus.Text = device.ColorPrintStatus.ToString();
                txtColorPrintStatus.Text = device.BlackWhitePrintStatus.ToString();
                /// id umowy jak wolna to napis wolna
            }
        }
    }
}
