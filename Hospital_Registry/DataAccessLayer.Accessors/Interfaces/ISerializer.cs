using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Accessors.Interfaces
{
    interface ISerializer<TEntity>
    {
        void Serialize(TEntity item, string filePath, System.IO.FileMode fileMode);
        TEntity Deserialize(string filePath, System.IO.FileMode fileMode);
    }
}
