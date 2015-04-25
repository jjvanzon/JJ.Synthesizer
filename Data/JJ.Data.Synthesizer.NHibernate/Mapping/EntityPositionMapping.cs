using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class EntityPositionMapping : ClassMap<EntityPosition>
    {
        public EntityPositionMapping()
        {
            Id(x => x.ID);
            Map(x => x.EntityTypeName);
            Map(x => x.EntityID);
            Map(x => x.X);
            Map(x => x.Y);
        }
    }
}
