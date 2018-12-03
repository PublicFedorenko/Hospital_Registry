using BusinessLogicLayer.Entities.Entities;
using DataAccessLayer.Persistance;
using DataAccessLayer.Persistance.Repositories;
using DataAccessLayer.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class HospitalRegistryService : INotifyPropertyChanged
    {
        private ObservableCollection<Patient> _patients;
        public ObservableCollection<Patient> Patients
        {
            get
            {
                //ObservableCollection<Patient> patients;
                using (UnitOfWork unitOfWork = new UnitOfWork(new HospitalRegistryContext()))
                {
                    _patients = new ObservableCollection<Patient>(unitOfWork.Patients.GetAll().ToList());
                }
                return _patients;
            }
            //set
            //{
            //    _patients = value;
            //    using (UnitOfWork unitOfWork = new UnitOfWork(new HospitalRegistryContext()))
            //    {
            //        unitOfWork.Patients.AddRange(_patients);
            //        unitOfWork.Complete();
            //    }
            //}
        }
        public ObservableCollection<Doctor> Doctors;

        public HospitalRegistryService()
        {
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void AddPatient(Patient patient)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new HospitalRegistryContext()))
            {
                unitOfWork.Patients.Add(patient);
                unitOfWork.Complete();
                OnPropertyChanged("Patients");
            }
        }

        public void RemovePatient(Patient patient)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new HospitalRegistryContext()))
            {
                unitOfWork.Patients.Remove(patient);
                unitOfWork.Complete();
                OnPropertyChanged("Patients");
            }
        }

        public void RemovePatient(int index)
        {
            Patient patient = Patients[index];
            using (UnitOfWork unitOfWork = new UnitOfWork(new HospitalRegistryContext()))
            {
                unitOfWork.Patients.Remove(patient);
                unitOfWork.Complete();
                OnPropertyChanged("Patients");
            }
        }

        public void AddPatientRange(IEnumerable<Patient> patients)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new HospitalRegistryContext()))
            {
                unitOfWork.Patients.AddRange(patients);
                unitOfWork.Complete();
                OnPropertyChanged("Patients");
            }
        }

        public void ClearPatients()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new HospitalRegistryContext()))
            {
                unitOfWork.Patients.RemoveRange(unitOfWork.Patients.GetAll());
                unitOfWork.Complete();
                OnPropertyChanged("Patients");
            }
        }

        public void FindPatients(string value)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new HospitalRegistryContext()))
            {
                if (value.Length != 0)
                {
                    List<Patient> allPatients = Patients.ToList();
                    Patients.Clear();
                    foreach (var p in allPatients)
                    {
                        if (p.FirstName.Contains(value))
                            Patients.Add(p);
                        if (p.LastName.Contains(value))
                            Patients.Add(p);
                    }
                }

                unitOfWork.Complete();
                OnPropertyChanged("Patients");
            }
        }
    }
}
