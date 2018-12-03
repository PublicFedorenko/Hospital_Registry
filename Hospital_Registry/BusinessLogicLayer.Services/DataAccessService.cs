using DataAccessLayer.Serialization.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class DataAccessService<T> where T : class
    {
        public ISerializer<T> Serializer { get; set; }

        public void Write(T item, string filePath, FileMode fileMode)
        {
            Serializer.Serialize(item, filePath, fileMode);
        }

        public T Read(string filePath, FileMode fileMode)
        {
            return Serializer.Deserialize(filePath, fileMode);
        }
    }
}
