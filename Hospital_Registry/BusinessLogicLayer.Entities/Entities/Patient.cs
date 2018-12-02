using BusinessLogicLayer.Entities.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities.Entities
{
    [Serializable]
    public class Patient : Person
    {
        #region Fields
        private string _mobileNumber;
        private string _homeNumber;
        private Adress _adress;
        private string _email;
        #endregion

        #region Properties
        public int Id { get; set; }
        public List<Visit> SheduledVisits { get; set; }
        public List<Visit> VisitsHistory { get; set; }
        public Adress Adress { get; set; }

        public string MobileNumber
        {
            get => _mobileNumber;
            set
            {
                //Regex mobileNumberPattern = new Regex(@"(?x)(\+38\d{10} | 38\d{10} | \d{10})");
                //if (mobileNumberPattern.IsMatch(value))
                //    _mobileNumber = value;
                //else
                //    throw new ArgumentException("Invalid mobile number.");

                _mobileNumber = value;
            }
        }
        public string HomeNumber
        {
            get => _homeNumber;
            set
            {
                //Regex homeNumberPattern = new Regex(@"(?x)(044\d{7} | \d{7} | +38044\d{7} | +38(044)\d{7})");
                //if (homeNumberPattern.IsMatch(value))
                //    _homeNumber = value;
                //else
                //    throw new ArgumentException("Invalid home number.");

                _homeNumber = value;
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                //Regex emailPattern = new Regex(@".+@\w+\.\w+");
                //if (emailPattern.IsMatch(value))
                //    _email = value;
                //else
                //    throw new ArgumentException("Invalid email.");

                _email = value;
            }
        }
        #endregion

        public Patient() { }
        public Patient(string firstName, string lastName, DateTime dateTime, Gender gender)
           : base(firstName, lastName, dateTime, gender) { }
        public void AddAdress(string city, string street, string building, int apartment)
        {
            Adress = new Adress(city, street, building, apartment);
        }
    }
}
