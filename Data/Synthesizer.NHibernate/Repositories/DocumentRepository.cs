using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable UnusedVariable

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class DocumentRepository : DefaultRepositories.DocumentRepository
    {
        private new readonly NHibernateContext _context;

        public DocumentRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override IList<Document> OrderByName()
        {
            return _context.Session.QueryOver<Document>()
                                   .OrderBy(x => x.Name).Asc
                                   .List();
        }

        /// <summary> TODO: Eager load the enum-like types too. </summary>
        public override Document TryGetComplete(int documentID)
        {
            Document document = null;
                AudioFileOutput audioFileOutput = null;
                Curve curve = null;
                Sample sample = null;
                Scale scale = null;
                Patch patch = null;
                    Operator op = null;
                DocumentReference dependent_documentReference = null;
                DocumentReference dependentOn_documentReference = null;

            var level_1_documentQuery = _context.Session.QueryOver(() => document)
                                                        .Where(x => x.ID == documentID)
                                                        .Future<Document>();

            var level_2_audioFileOutputsQuery = _context.Session.QueryOver(() => document)
                                                                .Left.JoinAlias(() => document.AudioFileOutputs, () => audioFileOutput)
                                                                .Where(() => document.ID == documentID)
                                                                .Future<Document>();

            var level_2_curvesQuery = _context.Session.QueryOver(() => document)
                                                      .Left.JoinAlias(() => document.Curves, () => curve)
                                                      .Where(() => document.ID == documentID)
                                                      .Future<Document>();

            var level_3_nodesQuery = _context.Session.QueryOver(() => curve)
                                                     .Fetch(x => x.Nodes).Eager
                                                     .Where(x => x.Document.ID == documentID)
                                                     .Future<Curve>();

            // Not used by the application, but queried to prevent a collection retrieval query later.
            var level_2_patchesQuery = _context.Session.QueryOver(() => document)
                                                       .Left.JoinAlias(() => document.Patches, () => patch)
                                                       .Where(() => document.ID == documentID)
                                                       .Future<Document>();

            var level_2_dependentOnDocumentsQuery = _context.Session.QueryOver(() => document)
                                                                    .Left.JoinAlias(() => document.LowerDocumentReferences, () => dependentOn_documentReference)
                                                                    .Where(() => document.ID == documentID)
                                                                    .Future<Document>();

            var level_2_dependentDocumentsQuery = _context.Session.QueryOver(() => document)
                                                                  .Left.JoinAlias(() => document.HigherDocumentReferences, () => dependent_documentReference)
                                                                  .Where(() => document.ID == documentID)
                                                                  .Future<Document>();

            var level_2_samplesQuery = _context.Session.QueryOver(() => document)
                                                       .Left.JoinAlias(() => document.Samples, () => sample)
                                                       .Where(() => document.ID == documentID)
                                                       .Future<Document>();

            var level_2_scalesQuery = _context.Session.QueryOver(() => document)
                                                      .Left.JoinAlias(() => document.Scales, () => scale)
                                                      .Where(() => document.ID == documentID)
                                                      .Future<Document>();

            var level_3_tonesQuery = _context.Session.QueryOver(() => scale)
                                                     .Fetch(x => x.Tones).Eager
                                                     .Where(x => x.Document.ID == documentID)
                                                     .Future<Scale>();

            var level_3_operatorsQuery = _context.Session.QueryOver(() => patch)
                                                         .JoinAlias(() => patch.Document, () => document)
                                                         .Where(() => document.ID == documentID)
                                                         .Fetch(x => x.Operators).Eager
                                                         .Future<Patch>();

            var level_4_inletsQuery = _context.Session.QueryOver(() => op)
                                                      .JoinAlias(() => op.Patch, () => patch)
                                                      .JoinAlias(() => patch.Document, () => document)
                                                      .Where(() => document.ID == documentID)
                                                      .Fetch(x => x.Inlets).Eager
                                                      .Future<Operator>();

            var level_4_outletsQuery = _context.Session.QueryOver(() => op)
                                                       .JoinAlias(() => op.Patch, () => patch)
                                                       .JoinAlias(() => patch.Document, () => document)
                                                       .Where(() => document.ID == documentID)
                                                       .Fetch(x => x.Outlets).Eager
                                                       .Future<Operator>();

            Document outputDocument = level_1_documentQuery.FirstOrDefault();
            return outputDocument;
        }
    }
}
