using System.ComponentModel;
using System.Windows.Documents;
using System.Xml.XPath;

namespace RaportGenerator.Models
{
    public class DeviceModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public int ColorPrintStatus { get; set; }
        public int BlackWhitePrintStatus { get; set; }
        public string ContractedCompany { get; set; }
        public string DeviceDisplayName
        {
            get { return $"{DeviceName.Trim()}-{SerialNumber.Trim()}"; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}