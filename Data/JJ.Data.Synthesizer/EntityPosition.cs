using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer
{
    public class EntityPosition
    {
        public virtual int ID { get; set; }
        public virtual string EntityTypeName { get; set; }
        public virtual int EntityID { get; set; }
        public virtual float X { get; set; }
        public virtual float Y { get; set; }
    }
}
