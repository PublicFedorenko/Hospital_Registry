using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BusinessLogicLayer.Entities.Entities;

namespace DataAccessLayer.Persistance
{
    public class HospitalRegistryContext : DbContext
    {
        public DbSet<Patient> Patients { get; }
        public DbSet<Doctor> Doctors { get; }

        public HospitalRegistryContext()
            :base("name=HospitalRegistryContext")
        {
        }
    }
}
