using BusinessLogicLayer.Entities.Entities;
using BusinessLogicLayer.Entities.Misc;
using BusinessLogicLayer.Services;
using PresentationLayer.GUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.GUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly HospitalRegistryService _hospitalRegistryService = new HospitalRegistryService();

        #region Commands
        public IDelegateCommand AddPatientCommand { get; protected set; }
        public IDelegateCommand RemovePatientCommand { get; protected set; }
        public IDelegateCommand EditPatientCommand { get; protected set; }
        public IDelegateCommand CompleteEditPatientCommand { get; protected set; }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public MainWindowViewModel()
        {
            AddPatientCommand = new DelegateCommand(ExecuteAddPatient);
            RemovePatientCommand = new DelegateCommand(ExecuteRemovePatient, CanExecuteRemovePatient);
            EditPatientCommand = new DelegateCommand(ExecuteEditPatient, CanExecuteEditPatient);
            CompleteEditPatientCommand = new DelegateCommand(ExecuteComleteEditPatient, CanExecuteComleteEditPatient);
        }

        public ObservableCollection<Patient> PatientsList
        {
            get => _hospitalRegistryService.Patients;
        }

        private void ExecuteAddPatient(object param)
        {
            Patient patient = new Patient()
            {
                FirstName = "Empty",
                LastName = "Patient",
                DateOfBirth = new DateTime(2000, 1, 1),
                Gender = Gender.Male,
                Adress = new Adress()
            };

            _hospitalRegistryService.AddPatient(patient);
            OnPropertyChanged("PatientsList");
        }

        private int _patientsListSelectedIndex;
        public int PatientsListBoxSelectedIndex {
            get => _patientsListSelectedIndex;
            set
            {
                _patientsListSelectedIndex = value;
                RemovePatientCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanExecuteRemovePatient(object param)
        {
            return PatientsListBoxSelectedIndex != -1;
        }

        public void ExecuteRemovePatient(object param)
        {
            _hospitalRegistryService.RemovePatient(PatientsListBoxSelectedIndex);
            RemovePatientCommand.RaiseCanExecuteChanged();
            OnPropertyChanged("PatientsList");
        }

        private bool _isPatientReadonly = true;
        public bool IsPatientReadOnly
        {
            get => _isPatientReadonly;
            set
            {
                _isPatientReadonly = value;
                EditPatientCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanExecuteEditPatient(object param)
        {
            return PatientsListBoxSelectedIndex != -1;
        }

        public void ExecuteEditPatient(object param)
        {
            IsPatientReadOnly = false;
            OnPropertyChanged("IsPatientReadOnly");
        }



        public bool CanExecuteComleteEditPatient(object param)
        {
            return PatientsListBoxSelectedIndex != -1;
        }

        public void ExecuteComleteEditPatient(object param)
        {
            IsPatientReadOnly = true;
            OnPropertyChanged("IsPatientReadOnly");
        }
    }
}
