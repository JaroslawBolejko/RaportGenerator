using RaportGenerator.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaportGenerator.Models
{
    public class ContractModel : INotifyPropertyChanged
    {
        public int ContractId { get; set; }

        public string Name { get; set; } = "Umowa";

        public string Number { get; set; }

        public string ContractDate { get; set; }

        public string Term { get; set; }

        public CompanyModel Client { get; set; }

        public List<ReportModel> Reports { get; set; }

        public List<DeviceModel> Devices { get; set; }

        public List<ContractDevice> ContractDevices { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _isValid;
        public bool IsValid
        {
            get => _isValid;
            set
            {
                if (_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}