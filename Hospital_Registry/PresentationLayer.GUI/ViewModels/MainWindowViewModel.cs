﻿using BusinessLogicLayer.Entities.Entities;
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
        private ObservableCollection<Doctor> _doctors;
        private ObservableCollection<Visit> _patientAppointments;
        private readonly string _defaultPatientsFilePath;
        private readonly string _defaultDoctorsFilePath;

        #region Commands
        public IDelegateCommand AddPatientCommand { get; protected set; }
        public IDelegateCommand RemovePatientCommand { get; protected set; }
        public IDelegateCommand EditPatientCommand { get; protected set; }
        public IDelegateCommand CompleteEditPatientCommand { get; protected set; }
        public IDelegateCommand RefreshPatient { get; protected set; }
        public IDelegateCommand FindPatients { get; protected set; }
        public IDelegateCommand AddAppointment { get; protected set; }
        public IDelegateCommand RemoveAppointment { get; protected set; }

        public IDelegateCommand AddDoctorCommand { get; protected set; }
        public IDelegateCommand RemoveDoctorCommand { get; protected set; }
        public IDelegateCommand EditDoctorCommand { get; protected set; }
        public IDelegateCommand CompleteEditDoctorCommand { get; protected set; }
        public IDelegateCommand RefreshDoctor { get; protected set; }
        public IDelegateCommand FindDoctors { get; protected set; }
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
            _defaultDoctorsFilePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Doctors.xml"));
            _patients = Load<Patient>(_defaultPatientsFilePath);
            _doctors = Load<Doctor>(_defaultDoctorsFilePath);

            #region InitializeCommands
            AddPatientCommand = new DelegateCommand(ExecuteAddPatient);
            RemovePatientCommand = new DelegateCommand(ExecuteRemovePatient, CanExecuteRemovePatient);
            EditPatientCommand = new DelegateCommand(ExecuteEditPatient, CanExecuteEditPatient);
            CompleteEditPatientCommand = new DelegateCommand(ExecuteComleteEditPatient, CanExecuteComleteEditPatient);
            RefreshPatient = new DelegateCommand(ExecuteRefreshPatient, CanExecuteRefreshPatient);
            FindPatients = new DelegateCommand(ExecuteFindPatients, CanExecuteFindPatients);
            AddAppointment = new DelegateCommand(ExecuteAddAppointment);
            RemoveAppointment = new DelegateCommand(ExecuteRemoveAppointment, CanExecuteRemoveAppointment);

            AddDoctorCommand = new DelegateCommand(ExecuteAddDoctor);
            RemoveDoctorCommand = new DelegateCommand(ExecuteRemoveDoctor, CanExecuteRemoveDoctor);
            EditDoctorCommand = new DelegateCommand(ExecuteEditDoctor, CanExecuteEditDoctor);
            CompleteEditDoctorCommand = new DelegateCommand(ExecuteComleteEditDoctor, CanExecuteComleteEditDoctor);
            RefreshDoctor = new DelegateCommand(ExecuteRefreshDoctor, CanExecuteRefreshDoctor);
            FindDoctors = new DelegateCommand(ExecuteFindDoctors, CanExecuteFindDoctors);
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

        public ObservableCollection<Visit> PatientAppointments
        {
            get
            {
                try
                {
                    _patientAppointments = new ObservableCollection<Visit>(_patients[PatientsListBoxSelectedIndex].SheduledVisits);

                }
                catch (Exception ex) { }
                return _patientAppointments;
            }
        }

        #region SaveEntities
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
        #endregion

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
                OnPropertyChanged("PatientAppointments");
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

        
        private int _patientsAppointmentsListBoxSelectedIndex;
        public int PatientsAppointmentsListBoxSelectedIndex
        {
            get => _patientsAppointmentsListBoxSelectedIndex;
            set
            {
                _patientsAppointmentsListBoxSelectedIndex = value;
                RemoveAppointment.RaiseCanExecuteChanged();
                OnPropertyChanged("PatientsAppointmentsListBoxSelectedIndex");
            }
        }
        #endregion


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
            OnPropertyChanged("PatientsList");
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

        #region AddAppointment
        public void ExecuteAddAppointment(object param)
        {
            Visit visit = new Visit()
            {
                Date = new DateTime(2000, 1, 1),
                Doctor = new Doctor()
                {
                    Specialization = "Specialization",
                    FirstName = "FirstName",
                    LastName = "LastName"
                }
            };
            _patients[PatientsListBoxSelectedIndex].SheduledVisits.Add(visit);
            Save(_patients, _defaultPatientsFilePath);
            OnPropertyChanged("PatientsList");
            OnPropertyChanged("PatientAppointments");
        }
        #endregion

        #region RemoveAppointment
        public bool CanExecuteRemoveAppointment(object param)
        {
            return PatientsAppointmentsListBoxSelectedIndex != -1;
        }

        public void ExecuteRemoveAppointment(object param)
        {
            Visit selectedVisit = _patientAppointments[_patientsAppointmentsListBoxSelectedIndex];
            _patients[PatientsListBoxSelectedIndex]
                .SheduledVisits
                .Remove(selectedVisit);
            Save(_patients, _defaultPatientsFilePath);
            RemoveAppointment.RaiseCanExecuteChanged();
            OnPropertyChanged("PatientsAppointmentsListBoxSelectedIndex");
            OnPropertyChanged("PatientAppointments");
            OnPropertyChanged("PatientsList");
        }
        #endregion



        public ObservableCollection<Doctor> DoctorsList
        {
            get
            {
                return _doctors;
            }
            set
            {
                _doctors = value;
            }
        }

        #region UtilityProperties
        private int _doctorsListSelectedIndex;
        public int DoctorsListBoxSelectedIndex
        {
            get => _doctorsListSelectedIndex;
            set
            {
                _doctorsListSelectedIndex = value;
                RemoveDoctorCommand.RaiseCanExecuteChanged();
                OnPropertyChanged("IsDoctorSelected");
            }
        }

        private bool _isDoctorReadonly = true;
        public bool IsDoctorReadOnly
        {
            get => _isDoctorReadonly;
            set
            {
                _isDoctorReadonly = value;
                EditDoctorCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsDoctorSelected
        {
            get => DoctorsListBoxSelectedIndex != -1;
        }
        #endregion

        #region AddDoctor
        private void ExecuteAddDoctor(object param)
        {
            Doctor doctor = new Doctor()
            {
                FirstName = "Empty",
                LastName = "Doctor",
                DateOfBirth = new DateTime(2000, 1, 1),
                Gender = Gender.Male,
                Adress = new Adress()
            };

            _doctors.Add(doctor);
            Save(_doctors, _defaultDoctorsFilePath);
            OnPropertyChanged("DoctorsList");
        }
        #endregion

        #region RemoveDoctor
        public bool CanExecuteRemoveDoctor(object param)
        {
            return DoctorsListBoxSelectedIndex != -1;
        }

        public void ExecuteRemoveDoctor(object param)
        {
            Doctor selectedDoctor = _doctors[DoctorsListBoxSelectedIndex];
            _doctors.Remove(selectedDoctor);
            Save(_doctors, _defaultDoctorsFilePath);
            RemoveDoctorCommand.RaiseCanExecuteChanged();
            OnPropertyChanged("DoctorsList");
        }
        #endregion

        #region EditDoctor
        public bool CanExecuteEditDoctor(object param)
        {
            return DoctorsListBoxSelectedIndex != -1;
        }

        public void ExecuteEditDoctor(object param)
        {
            IsDoctorReadOnly = false;
            OnPropertyChanged("IsDoctorReadOnly");
        }
        #endregion

        #region CompleteEditDoctor
        public bool CanExecuteComleteEditDoctor(object param)
        {
            return DoctorsListBoxSelectedIndex != -1;
        }

        public void ExecuteComleteEditDoctor(object param)
        {
            IsDoctorReadOnly = true;
            Save(_doctors, _defaultDoctorsFilePath);
            OnPropertyChanged("IsDoctorReadOnly");
        }
        #endregion

        #region RefreshDoctor
        public bool CanExecuteRefreshDoctor(object param)
        {
            return DoctorsListBoxSelectedIndex != -1;
        }

        public void ExecuteRefreshDoctor(object param)
        {
            _doctors = Load<Doctor>(_defaultDoctorsFilePath);
            DoctorsListBoxSelectedIndex = 0;
            OnPropertyChanged("DoctorsList");
        }
        #endregion

        #region FindDoctors
        public bool CanExecuteFindDoctors(object param)
        {
            return DoctorsList.Count > 0;
        }

        public void ExecuteFindDoctors(object param)
        {
            _doctors = new ObservableCollection<Doctor>(Load<Doctor>(_defaultDoctorsFilePath).Where(
                p =>
                {
                    return p.FirstName.ToLower().Contains(((string)param).ToLower()) ||
                           p.LastName.ToLower().Contains(((string)param).ToLower());
                }));
            OnPropertyChanged("DoctorsList");
        }
        #endregion
    }
}
