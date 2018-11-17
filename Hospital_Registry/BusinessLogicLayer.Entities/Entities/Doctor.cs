using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities.Entities
{
    public class Doctor : Person
    {
        #region Fields
        private int _experience;
        private DateTime _startedACareer;
        private string _education;
        private string _specialization;
        private double _salary;
        private List<DateTime> _shedule;
        #endregion

        #region Properties
        public int Experience
        {
            get => _experience;
            set
            {
                if (value > 0 || value < 100)
                {
                    _experience = value;
                }
            }
        }
        public DateTime StartedACareer
        {
            get => _startedACareer;
            set
            {
                _startedACareer = value;
                Experience = DateTime.Now.Year - _startedACareer.Year;
            }
        }
        public string Education { get => _education; set => _education = value; }
        public string Specialization { get => _specialization; set => _specialization = value; }
        public double Salary
        {
            get => _salary;
            set
            {
                if (value > 0)
                    _salary = value;
            }
        }
        public List<DateTime> Shedule { get => _shedule; set => _shedule = value; }
        #endregion

        public Doctor() { }
        public Doctor(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
           : base(firstName, lastName, dateOfBirth, gender) { }

    }
}
