using BusinessLogicLayer.Entities.Entities;
using DataAccessLayer.Serialization;
using DataAccessLayer.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class HospitalRegistryService<T> : DataAccessService<T> where T : class
    {
        public HospitalRegistryService()
        {
            Serializer = new XmlSerializer<T>();
        }
    }
}