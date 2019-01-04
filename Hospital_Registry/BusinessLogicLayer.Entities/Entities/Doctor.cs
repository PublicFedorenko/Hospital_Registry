using BusinessLogicLayer.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities.Entities
{
    [Serializable]
    public class Doctor : Person
    {
        #region Fields
        private int _experience;
        private DateTime _startedACareer;
        private string _education;
        private string _specialization;
        private DateTime[] _shedule;
        #endregion

        #region Properties
        public int Id { get; set; }
        public Adress Adress { get; set; }
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
        public string Education { get; set; }
        public string Specialization { get; set; }
        public List<Visit> VisitsQueue { get; set; }
        #endregion

        public Doctor()
        {
            _shedule = new DateTime[7];
        }
        public Doctor(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
           : base(firstName, lastName, dateOfBirth, gender)
        {
            _shedule = new DateTime[7];
        }

    }
}
