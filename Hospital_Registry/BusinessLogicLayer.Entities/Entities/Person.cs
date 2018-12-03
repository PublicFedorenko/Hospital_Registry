using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities.Entities
{
    public enum Gender { Male, Female }

    [Serializable]
    public class Person : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("Age");
                OnPropertyChanged("DateOfBirth");
            }
        }
        public int Age { get => DateTime.Today.Year - DateOfBirth.Year; }
        public Gender Gender { get; set; }
        public Person() { }
        public Person(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
