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
        DbSet<Patient> Patients { get; set; }
        DbSet<Doctor> Doctors { get; set; }
    }
}
