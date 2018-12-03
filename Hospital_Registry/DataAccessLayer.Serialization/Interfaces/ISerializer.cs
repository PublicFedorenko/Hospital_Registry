using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Serialization.Interfaces
{
    public interface ISerializer<T> where T : class
    {
        void Serialize(T item, string filePath, FileMode fileMode);
        T Deserialize(string filePath, FileMode fileMode);
    }
}
