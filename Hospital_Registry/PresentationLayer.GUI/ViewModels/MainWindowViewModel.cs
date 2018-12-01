using BusinessLogicLayer.Entities.Entities;
using BusinessLogicLayer.Services;
using PresentationLayer.GUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.GUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly HospitalRegistryService _hospitalRegistryService = new HospitalRegistryService();

        public IDelegateCommand AddPatientCommand { get; protected set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel()
        {
            AddPatientCommand = new DelegateCommand(ExecuteAddPatient);
        }

        private void ExecuteAddPatient(object param)
        {
            _hospitalRegistryService.AddPatient(new Patient());
        }
    }
}
