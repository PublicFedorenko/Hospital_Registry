using BusinessLogicLayer.Entities.Entities;
using BusinessLogicLayer.Entities.Misc;
using BusinessLogicLayer.Services;
using PresentationLayer.GUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.GUI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Patient> _patients;
        private readonly string _defaultPatientsFilePath;

        #region Commands
        public IDelegateCommand AddPatientCommand { get; protected set; }
        public IDelegateCommand RemovePatientCommand { get; protected set; }
        public IDelegateCommand EditPatientCommand { get; protected set; }
        public IDelegateCommand CompleteEditPatientCommand { get; protected set; }
        public IDelegateCommand RefreshPatient { get; protected set; }
        public IDelegateCommand FindPatients { get; protected set; }
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
            _defaultPatientsFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Patients.xml"));
            _patients = Load<Patient>(_defaultPatientsFilePath);

            #region InitializeCommands
            AddPatientCommand = new DelegateCommand(ExecuteAddPatient);
            RemovePatientCommand = new DelegateCommand(ExecuteRemovePatient, CanExecuteRemovePatient);
            EditPatientCommand = new DelegateCommand(ExecuteEditPatient, CanExecuteEditPatient);
            CompleteEditPatientCommand = new DelegateCommand(ExecuteComleteEditPatient, CanExecuteComleteEditPatient);
            RefreshPatient = new DelegateCommand(ExecuteRefreshPatient, CanExecuteRefreshPatient);
            FindPatients = new DelegateCommand(ExecuteFindPatients, CanExecuteFindPatients);
            #endregion
        }

        public ObservableCollection<Patient> PatientsList
        {
            get
            {
                return _patients;
            }
            set
            {
                _patients = value;
            }
        }

        #region UtilityProperties
        private int _patientsListSelectedIndex;
        public int PatientsListBoxSelectedIndex
        {
            get => _patientsListSelectedIndex;
            set
            {
                _patientsListSelectedIndex = value;
                RemovePatientCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("IsPatientSelected");
            }
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

        public bool IsPatientSelected
        {
            get => PatientsListBoxSelectedIndex != -1;
        }
        #endregion

        private void Save<TEntity>(ObservableCollection<TEntity> entities, string filePath)
        {
            HospitalRegistryService<ObservableCollection<TEntity>> hospitalRegistryService =
                new HospitalRegistryService<ObservableCollection<TEntity>>();

            hospitalRegistryService.Write(entities, filePath, FileMode.Truncate);
        }

        private ObservableCollection<TEntity> Load<TEntity>(string filePath)
        {
            HospitalRegistryService<ObservableCollection<TEntity>> hospitalRegistryService =
                new HospitalRegistryService<ObservableCollection<TEntity>>();

            return hospitalRegistryService.Read(filePath, FileMode.Open);
        }

        #region AddPatient
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

            
            _patients.Add(patient);
            Save(_patients, _defaultPatientsFilePath);
            OnPropertyChanged("PatientsList");
        }
        #endregion

        #region RemovePatient
        public bool CanExecuteRemovePatient(object param)
        {
            return PatientsListBoxSelectedIndex != -1;
        }

        public void ExecuteRemovePatient(object param)
        {
            HospitalRegistryService<ObservableCollection<Patient>> hospitalRegistryService =
                new HospitalRegistryService<ObservableCollection<Patient>>();

            Patient selectedPatient = _patients[PatientsListBoxSelectedIndex];
            _patients.Remove(selectedPatient);
            Save(_patients, _defaultPatientsFilePath);
            RemovePatientCommand.RaiseCanExecuteChanged();
            OnPropertyChanged("PatientsList");
        }
        #endregion

        #region EditPatient
        public bool CanExecuteEditPatient(object param)
        {
            return PatientsListBoxSelectedIndex != -1;
        }

        public void ExecuteEditPatient(object param)
        {
            IsPatientReadOnly = false;
            OnPropertyChanged("IsPatientReadOnly");
        }
        #endregion

        #region CompleteEditPatient
        public bool CanExecuteComleteEditPatient(object param)
        {
            return PatientsListBoxSelectedIndex != -1;
        }

        public void ExecuteComleteEditPatient(object param)
        {
            IsPatientReadOnly = true;
            Save(_patients, _defaultPatientsFilePath);
            OnPropertyChanged("IsPatientReadOnly");
        }
        #endregion

        #region RefreshPatient
        public bool CanExecuteRefreshPatient(object param)
        {
            return PatientsListBoxSelectedIndex != -1;
        }

        public void ExecuteRefreshPatient(object param)
        {
            _patients = Load<Patient>(_defaultPatientsFilePath);
            PatientsListBoxSelectedIndex = 0;
            OnPropertyChanged("PatientsList");
        }
        #endregion

        #region FindPatients
        public bool CanExecuteFindPatients(object param)
        {
            return PatientsList.Count > 0;
        }

        public void ExecuteFindPatients(object param)
        {
            HospitalRegistryService<ObservableCollection<Patient>> hospitalRegistryService =
                new HospitalRegistryService<ObservableCollection<Patient>>();

            
            _patients = new ObservableCollection<Patient>(Load<Patient>(_defaultPatientsFilePath).Where(
                p =>
                {
                    return p.FirstName.ToLower().Contains(((string)param).ToLower()) ||
                           p.LastName.ToLower().Contains(((string)param).ToLower());
                }));
            OnPropertyChanged("PatientsList");
        }
        #endregion
    }
}
