using RaportGenerator.DataAccess;
using RaportGenerator.DataAccess.Entities;
using RaportGenerator.Models;
using System;
using System.Windows;

namespace RaportGenerator.Views
{
    /// <summary>
    /// Interaction logic for DeviceModifyView.xaml
    /// </summary>
    public partial class DeviceModifyView : Window
    {
        public DeviceModel device = new DeviceModel();
        public DeviceModifyView()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!validateIncomingData())
            {
                using (RaportGeneratorContext db = new RaportGeneratorContext())
                {
                    if (device != null && device.Id != 0)
                    {
                        Device deviceToUpdate = new Device
                        {
                            Id = device.Id,
                            DeviceName = txtDeviceName.Text,
                            SerialNumber = txtSerialNumber.Text,
                            //ColorPrintCounter = Convert.ToInt32(txtColorPrintStatus.Text),
                            //BlackWhitePrintCounter = Convert.ToInt32(txtBlackWhitePrintStatus.Text),
                        };

                        db.Devices.Update(deviceToUpdate);
                        db.SaveChanges();
                        MessageBox.Show("Edycja zakończona sukcesem!");
                        this.Close();
                    }
                    else
                    {
                        Device deviceToAdd = new Device
                        {
                            DeviceName = txtDeviceName.Text,
                            SerialNumber = txtSerialNumber.Text,
                            //ColorPrintCounter = Convert.ToInt32(txtColorPrintStatus.Text),
                            //BlackWhitePrintCounter = Convert.ToInt32(txtBlackWhitePrintStatus.Text),
                        };

                        db.Devices.Add(deviceToAdd);
                        db.SaveChanges();
                        txtDeviceName.Clear();
                        txtSerialNumber.Clear();
                        txtColorPrintStatus.Clear();
                        txtBlackWhitePrintStatus.Clear();
                        MessageBox.Show("Urządzenie zostało dodadne do bazy danych!");
                     //   if (MessageBox.Show("czy dodać kolejne urżadzenie?", "Odpowiedz", MessageBoxButton.YesNo
                     //, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                            this.Close();

                    }

                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (device != null && device.Id != 0)
            {
                txtDeviceName.Text = device.DeviceName;
                txtSerialNumber.Text = device.SerialNumber;
                txtColorPrintStatus.Text = device.ColorPrintStatus.ToString();
                txtBlackWhitePrintStatus.Text = device.BlackWhitePrintStatus.ToString();
            }
        }

        private bool validateIncomingData()
        {
            if (txtDeviceName.Text.Trim() == "")
            {
                MessageBox.Show("Podaj nazwę urządzenia!");
                return true;
            }

            if (txtSerialNumber.Text.Trim() == "")
            {
                MessageBox.Show("Podaj numer seryjny!");
                return true;
            }

            if (txtColorPrintStatus.Text.Trim() == "" || !Int32.TryParse(txtColorPrintStatus.Text, out int number))
            {
                MessageBox.Show("Podaj LICZBĘ kolorowych wydrukow!");
                return true;
            }

            if (txtBlackWhitePrintStatus.Text.Trim() == "" || !Int32.TryParse(txtBlackWhitePrintStatus.Text, out int number2))
            {
                MessageBox.Show("Podaj LICZBĘ czarno-białych wydruków"!);
                return true;
            }

            return false;
        }
    }
}