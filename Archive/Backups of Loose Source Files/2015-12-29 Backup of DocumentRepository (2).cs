//using JJ.Data.Synthesizer.NHibernate.Helpers;
//using JJ.Data.Synthesizer.SqlClient;
//using JJ.Framework.Data;
//using JJ.Framework.Data.NHibernate;
//using JJ.Framework.Reflection.Exceptions;
//using System.Collections.Generic;
//using System.Linq;
//using System;
//using NHibernate.SqlCommand;
//using NHibernate.Transform;

//namespace JJ.Data.Synthesizer.NHibernate.Repositories
//{
//    public class DocumentRepository : DefaultRepositories.DocumentRepository
//    {
//        private new NHibernateContext _context;

//        public DocumentRepository(IContext context)
//            : base(context)
//        {
//            _context = (NHibernateContext)context;
//        }

//        public override IList<Document> GetPageOfRootDocumentsOrderedByName(int firstIndex, int count)
//        {
//            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
//            IList<int> ids = sqlExecutor.Document_GetPageOfRootDocumentIDsOrderedByName(firstIndex, count).ToArray();
//            IList<Document> entities = GetManyByID(ids);
//            return entities;
//        }

//        public override int CountRootDocuments()
//        {
//            int count = _context.Session.QueryOver<Document>()
//                                        .Where(x => x.ParentDocument == null)
//                                        .RowCount();
//            return count;
//        }

//        /// <summary> TODO: Eager load the enum-like types too. </summary>
//        public override Document TryGetComplete(int documentID)
//        {
//            Document parentDocument = null;
//                AudioFileOutput audioFileOutput = null;
//                Curve curve = null;
//                Sample sample = null;
//                Scale scale = null;
//                Document childDocument = null;
//                    Patch patch = null;
//                        Operator op = null;
//                            Outlet outlet = null;
//                DocumentReference dependent_documentReference = null;
//                DocumentReference dependentOn_documentReference = null;

//            var level_1_documentQuery = _context.Session.QueryOver(() => parentDocument)
//                                                        .Where(x => x.ID == documentID)
//                                                        .Future<Document>();

//            var level_2_audioFileOutputsQuery = _context.Session.QueryOver(() => parentDocument)
//                                                                .Left.JoinAlias(() => parentDocument.AudioFileOutputs, () => audioFileOutput)
//                                                                .Where(() => parentDocument.ID == documentID)
//                                                                .Future<Document>();

//            var level_3_audioFileOutputChannelsQuery = _context.Session.QueryOver(() => audioFileOutput)
//                                                                       .Fetch(x => x.AudioFileOutputChannels).Eager
//                                                                       .Where(x => x.Document.ID == documentID)
//                                                                       .Future<AudioFileOutput>();

//            var level_2_curvesQuery = _context.Session.QueryOver(() => parentDocument)
//                                                      .Left.JoinAlias(() => parentDocument.Curves, () => curve)
//                                                      .Where(() => parentDocument.ID == documentID)
//                                                      .Future<Document>();

//            var level_3_nodesQuery = _context.Session.QueryOver(() => curve)
//                                                     .Fetch(x => x.Nodes).Eager
//                                                     .Where(x => x.Document.ID == documentID)
//                                                     .Future<Curve>();

//            // Not used by the application, but queried to prevent a collection retrieval query later.
//            var level_2_patchesQuery = _context.Session.QueryOver(() => parentDocument)
//                                                       .Left.JoinAlias(() => parentDocument.Patches, () => patch)
//                                                       .Where(() => parentDocument.ID == documentID)
//                                                       .Future<Document>();

//            var level_2_dependentOnDocumentsQuery = _context.Session.QueryOver(() => parentDocument)
//                                                                    .Left.JoinAlias(() => parentDocument.DependentOnDocuments, () => dependentOn_documentReference)
//                                                                    .Where(() => parentDocument.ID == documentID)
//                                                                    .Future<Document>();

//            var level_2_dependentDocumentsQuery = _context.Session.QueryOver(() => parentDocument)
//                                                                  .Left.JoinAlias(() => parentDocument.DependentDocuments, () => dependent_documentReference)
//                                                                  .Where(() => parentDocument.ID == documentID)
//                                                                  .Future<Document>();

//            var level_2_samplesQuery = _context.Session.QueryOver(() => parentDocument)
//                                                       .Left.JoinAlias(() => parentDocument.Samples, () => sample)
//                                                       .Where(() => parentDocument.ID == documentID)
//                                                       .Future<Document>();

//            var level_2_scalesQuery = _context.Session.QueryOver(() => parentDocument)
//                                                      .Left.JoinAlias(() => parentDocument.Scales, () => scale)
//                                                      .Where(() => parentDocument.ID == documentID)
//                                                      .Future<Document>();

//            var level_3_tonesQuery = _context.Session.QueryOver(() => scale)
//                                                     .Fetch(x => x.Tones).Eager
//                                                     .Where(x => x.Document.ID == documentID)
//                                                     .Future<Scale>();

//            var level_2_childDocumentsQuery = _context.Session.QueryOver(() => parentDocument)
//                                                              .Left.JoinAlias(() => parentDocument.ChildDocuments, () => childDocument)
//                                                              .Where(() => parentDocument.ID == documentID)
//                                                              .Future<Document>();

//            // Not used by the application, but queried to prevent a collection retrieval query later.
//            var level_3_childDocumentsQuery2 = _context.Session.QueryOver(() => childDocument)
//                                                               .Fetch(x => x.ChildDocuments).Eager
//                                                               .Where(x => x.ParentDocument.ID == documentID)
//                                                               .Future<Document>();

//            // Not used by the application, but queried to prevent a collection retrieval query later.
//            var level_3_audioFileOutputsQuery2 = _context.Session.QueryOver(() => childDocument)
//                                                                 .Fetch(x => x.AudioFileOutputs).Eager
//                                                                 .Where(x => x.ParentDocument.ID == documentID)
//                                                                 .Future<Document>();

//            var level_3_curvesQuery2 = _context.Session.QueryOver(() => childDocument)
//                                                       .Fetch(x => x.Curves).Eager
//                                                       .Where(x => x.ParentDocument.ID == documentID)
//                                                       .Future<Document>();

//            var level_4_nodesQuery2 = _context.Session.QueryOver(() => curve)
//                                                      .JoinAlias(() => curve.Document, () => childDocument)
//                                                      .JoinAlias(() => childDocument.ParentDocument, () => parentDocument)
//                                                      .Where(() => parentDocument.ID == documentID)
//                                                      .Fetch(x => x.Nodes).Eager
//                                                      .Future<Curve>();

//            // Not used by the application, but queried to prevent a collection retrieval query later.
//            var level_3_dependentOnDocumentsQuery2 = _context.Session.QueryOver(() => childDocument)
//                                                                     .Fetch(x => x.DependentOnDocuments).Eager
//                                                                     .Where(x => x.ParentDocument.ID == documentID)
//                                                                     .Future<Document>();

//            // Not used by the application, but queried to prevent a collection retrieval query later.
//            var level_3_dependentDocumentsQuery2 = _context.Session.QueryOver(() => childDocument)
//                                                                   .Fetch(x => x.DependentDocuments).Eager
//                                                                   .Where(x => x.ParentDocument.ID == documentID)
//                                                                   .Future<Document>();

//            var level_3_patchesQuery2 = _context.Session.QueryOver(() => childDocument)
//                                                        .Fetch(x => x.Patches).Eager
//                                                        .Where(x => x.ParentDocument.ID == documentID)
//                                                        .Future<Document>();

//            var level_4_operatorsQuery = _context.Session.QueryOver(() => patch)
//                                                         .JoinAlias(() => patch.Document, () => childDocument)
//                                                         .JoinAlias(() => childDocument.ParentDocument, () => parentDocument)
//                                                         .Where(() => parentDocument.ID == documentID)
//                                                         .Fetch(x => x.Operators).Eager
//                                                         .Future<Patch>();

//            var level_5_outletsQuery = _context.Session.QueryOver(() => op)
//                                                       .JoinAlias(() => op.Patch, () => patch)
//                                                       .JoinAlias(() => patch.Document, () => childDocument)
//                                                       .JoinAlias(() => childDocument.ParentDocument, () => parentDocument)
//                                                       .Where(() => parentDocument.ID == documentID)
//                                                       .Fetch(x => x.Outlets).Eager
//                                                       .Future<Operator>();

//            var level_5_inletsQuery = _context.Session.QueryOver(() => op)
//                                                      .JoinAlias(() => op.Patch, () => patch)
//                                                      .JoinAlias(() => patch.Document, () => childDocument)
//                                                      .JoinAlias(() => childDocument.ParentDocument, () => parentDocument)
//                                                      .Where(() => parentDocument.ID == documentID)
//                                                      .Fetch(x => x.Inlets).Eager
//                                                      .Future<Operator>();

//            var level_6_connectedInletsQuery = _context.Session.QueryOver(() => outlet)
//                                                               .JoinAlias(() => outlet.Operator, () => op)
//                                                               .JoinAlias(() => op.Patch, () => patch)
//                                                               .JoinAlias(() => patch.Document, () => childDocument)
//                                                               .JoinAlias(() => childDocument.ParentDocument, () => parentDocument)
//                                                               .Where(() => parentDocument.ID == documentID)
//                                                               .Fetch(x => x.ConnectedInlets).Eager
//                                                               .Future<Outlet>();

//            var level_3_samplesQuery2 = _context.Session.QueryOver(() => childDocument)
//                                                        .Fetch(x => x.Samples).Eager
//                                                        .Where(x => x.ParentDocument.ID == documentID)
//                                                        .Future<Document>();

//            // Not used by the application, but queried to prevent a collection retrieval query later.
//            var level_3_scalesQuery2 = _context.Session.QueryOver(() => childDocument)
//                                                       .Fetch(x => x.Scales).Eager
//                                                       .Where(x => x.ParentDocument.ID == documentID)
//                                                       .Future<Document>();

//            Document document = level_1_documentQuery.FirstOrDefault();
//            return document;
//        }

//        private IList<Document> GetManyByID(IList<int> ids)
//        {
//            if (ids == null) throw new NullException(() => ids);

//            IList<Document> list = new List<Document>(ids.Count);
//            foreach (int id in ids)
//            {
//                Document entity = Get(id);
//                list.Add(entity);
//            }

//            return list;
//        }
//    }
//}
