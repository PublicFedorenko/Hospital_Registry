using BusinessLogicLayer.Entities.Entities;
using DataAccessLayer.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Persistance.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(HospitalRegistryContext context)
            : base(context)
        {
        }
    }
}