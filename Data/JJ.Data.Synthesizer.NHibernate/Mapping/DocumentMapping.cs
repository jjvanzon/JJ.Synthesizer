using FluentNHibernate.Mapping;
using JJ.Data.Synthesizer.NHibernate.Names;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.NHibernate.Mapping
{
    public class DocumentMapping : ClassMap<Document>
    {
        public DocumentMapping()
        {
            Id(x => x.ID);

            Map(x => x.Name);

            References(x => x.MainPatch, ColumnNames.MainPatchID);
            References(x => x.AsInstrumentInDocument, ColumnNames.AsInstrumentInDocumentID);
            References(x => x.AsEffectInDocument, ColumnNames.AsEffectInDocumentID);

            HasMany(x => x.Curves).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.Patches).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.Samples).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.AudioFileOutputs).KeyColumn(ColumnNames.DocumentID).Inverse();
            HasMany(x => x.Instruments).KeyColumn(ColumnNames.AsInstrumentInDocumentID).Inverse();
            HasMany(x => x.Effects).KeyColumn(ColumnNames.AsEffectInDocumentID).Inverse();
            HasMany(x => x.DocumentReferences).KeyColumn(ColumnNames.ReferringDocumentID).Inverse();
        }
    }
}
