using DataAccessLayer.Persistance.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalRegistryContext _context;

        public IPatientRepository Patients { get; private set; }
        public IDoctorRepository Doctors { get; private set; }

        public UnitOfWork(HospitalRegistryContext context)
        {
            _context = context;
            Patients = new PatientRepository(_context);
            Doctors = new DoctorRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}