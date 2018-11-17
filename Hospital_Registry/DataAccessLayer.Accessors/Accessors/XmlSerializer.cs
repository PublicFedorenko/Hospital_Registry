using DataAccessLayer.Accessors.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataAccessLayer.Accessors.Accessors
{
    public class XmlSerializer<TEntity> : ISerializer<TEntity>
    {
        private XmlSerializer _xmlSerializer;

        public XmlSerializer()
        {
            _xmlSerializer = new XmlSerializer(typeof(TEntity));
        }

        public void Serialize(TEntity item, string filePath, FileMode fileMode)
        {
            using (FileStream fs = new FileStream(filePath, fileMode))
            {
                _xmlSerializer.Serialize(fs, item);
            }
        }

        public TEntity Deserialize(string filePath, FileMode fileMode)
        {
            using (FileStream fs = new FileStream(filePath, fileMode))
            {
                return (TEntity) _xmlSerializer.Deserialize(fs);
            }
        }
    }
}
