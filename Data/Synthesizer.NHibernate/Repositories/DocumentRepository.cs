using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;

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

        public override IList<Document> GetAll() => _context.Session.QueryOver<Document>().List();

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
                DocumentReference higherDocumentReference = null;
                DocumentReference lowerDocumentReference = null;

            var level_1_documentQuery = _context.Session.QueryOver(() => document)
                                                        .Where(x => x.ID == documentID)
                                                        .Future<Document>();

            var level_2_audioFileOutputsQuery = _context.Session.QueryOver(() => document)
                                                                .Left.JoinAlias(() => document.AudioFileOutputs, () => audioFileOutput)
                                                                .Where(() => document.ID == documentID)
                                                                .Future<Document>();

            var level_2_patchesQuery = _context.Session.QueryOver(() => document)
                                                       .Left.JoinAlias(() => document.Patches, () => patch)
                                                       .Where(() => document.ID == documentID)
                                                       .Future<Document>();

            var level_2_lowerDocumentReferencesQuery = _context.Session.QueryOver(() => document)
                                                               .Left.JoinAlias(() => document.LowerDocumentReferences, () => lowerDocumentReference)
                                                               .Where(() => document.ID == documentID)
                                                               .Future<Document>();

            var level_2_higherDocumentReferencesQuery = _context.Session.QueryOver(() => document)
                                                                .Left.JoinAlias(() => document.HigherDocumentReferences, () => higherDocumentReference)
                                                                .Where(() => document.ID == documentID)
                                                                .Future<Document>();

            var level_3_higherDocuments = _context.Session.QueryOver(() => higherDocumentReference)
                                                  .JoinAlias(() => higherDocumentReference.LowerDocument, () => document)
                                                  .Where(() => document.ID == documentID)
                                                  .Fetch(x => x.HigherDocument).Eager
                                                  .Future<DocumentReference>();

            // This partial query would cause duplicate patches to be loaded.
            //var level_4_higherDocumentPatchesQuery = _context.Session.QueryOver(() => higherDocument)
            //                                                 .JoinAlias(() => higherDocument.HigherDocumentReferences, () => higherDocumentReference)
            //                                                 .JoinAlias(() => higherDocumentReference.LowerDocument, () => document)
            //                                                 .Where(() => document.ID == documentID)
            //                                                 .Fetch(x => x.Patches).Eager
            //                                                 .Future<Document>();

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

            var level_4_standardDimensionQuery = _context.Session.QueryOver(() => op)
                                                         .JoinAlias(() => op.Patch, () => patch)
                                                         .JoinAlias(() => patch.Document, () => document)
                                                         .Where(() => document.ID == documentID)
                                                         .Fetch(x => x.StandardDimension).Eager
                                                         .Future<Operator>();

            var level_4_underlyingPatchQuery = _context.Session.QueryOver(() => op)
                                                       .JoinAlias(() => op.Patch, () => patch)
                                                       .JoinAlias(() => patch.Document, () => document)
                                                       .Where(() => document.ID == documentID)
                                                       .Fetch(x => x.UnderlyingPatch).Eager
                                                       .Future<Operator>();

            var level_4_curveQuery = _context.Session.QueryOver(() => op)
                                             .JoinAlias(() => op.Patch, () => patch)
                                             .JoinAlias(() => patch.Document, () => document)
                                             .Where(() => document.ID == documentID)
                                             .Fetch(x => x.Curve)
                                             .Eager
                                             .Future<Operator>();

            // Too bad: There is no curve.Operator to link upward to.
            //var level_5_nodesQuery = _context.Session.QueryOver(() => curve)
            //                                 .JoinAlias(() => curve.Operator, () => op)
            //                                 .JoinAlias(() => op.Patch, () => patch)
            //                                 .JoinAlias(() => patch.Document, () => document)
            //                                 .Where(() => document.ID == documentID)
            //                                 .Fetch(x => x.Nodes).Eager
            //                                 .Future<Curve>();

            var level_4_sampleQuery = _context.Session.QueryOver(() => op)
                                             .JoinAlias(() => op.Patch, () => patch)
                                             .JoinAlias(() => patch.Document, () => document)
                                             .Where(() => document.ID == documentID)
                                             .Fetch(x => x.Sample)
                                             .Eager
                                             .Future<Operator>();

            Document outputDocument = level_1_documentQuery.FirstOrDefault();
            return outputDocument;
        }

        public override Document TryGetByName(string name)
        {
            Document entity = _context.Session
                                      .QueryOver<Document>()
                                      .Where(x => x.Name == name)
                                      .SingleOrDefault();
            return entity;
        }
    }
}
