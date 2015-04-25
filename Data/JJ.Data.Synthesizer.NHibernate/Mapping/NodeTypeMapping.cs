using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class NodeTypeMapping : ClassMap<NodeType>
    {
        public NodeTypeMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
        }
    }
}
