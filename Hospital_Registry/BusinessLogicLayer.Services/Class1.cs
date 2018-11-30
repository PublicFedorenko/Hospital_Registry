using DataAccessLayer.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class HospitalRegistryService
    {
        public HospitalRegistryContext Context { get; }

        public HospitalRegistryService()
        {
            Context = new HospitalRegistryContext();
        }
    }
}
