using BusinessLogicLayer.Entities.Entities;
using DataAccessLayer.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Persistance.Repositories
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(HospitalRegistryContext context)
            : base(context)
        {
        }
    }
}
