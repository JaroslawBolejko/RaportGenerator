﻿using RaportGenerator.DataAccess.Entities;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaportGenerator.Models
{
    public class ReportModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string ReportName { get; set; }
        public string ReportNumber { get; set; }
        public string ReportDocumentDate { get; set; }       
        public string ReportForMonth { get; set; }
        //{
        //    get => _reportDate;
        //    set
        //    {
        //        _reportDate = value;
        //        OnPropertyChanged(ReportForMonth);
        //    }
        //}

        public string DeviceName { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string CompanyName { get; set; }
        public string ContractNumber { get; set; }
        public int PrevPrintsStateBlack { get; set; }
        public int PrevPrintsStateColor { get; set; }
        public int CurrentPrintsStateBlack { get; set; }
        public int CurrentPrintsStateColor { get; set; }
        public int BlackPrintsAmount { get; set; }
        public int ColorPrintsAmount { get; set; }
        public Company Client { get; set; }
        public Device Device { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private string _reportDate = DateTime.Now.ToString("dd.MM.yyyy");
    }
}
