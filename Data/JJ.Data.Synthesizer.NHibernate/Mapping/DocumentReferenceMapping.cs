using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class DocumentReferenceMapping : ClassMap<DocumentReference>
    {
        public DocumentReferenceMapping()
        {
            Id(x => x.ID).GeneratedBy.Assigned();
            Map(x => x.Alias);

            References(x => x.DependentDocument, ColumnNames.DependentDocumentID);
            References(x => x.DependentOnDocument, ColumnNames.DependentOnDocumentID);
        }
    }
}
