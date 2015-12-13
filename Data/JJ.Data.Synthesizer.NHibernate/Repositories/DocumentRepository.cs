using JJ.Data.Synthesizer.NHibernate.Helpers;
using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;
using JJ.Framework.Reflection.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
    public class DocumentRepository : DefaultRepositories.DocumentRepository
    {
        private new NHibernateContext _context;

        public DocumentRepository(IContext context)
            : base(context)
        {
            _context = (NHibernateContext)context;
        }

        public override IList<Document> GetPageOfRootDocumentsOrderedByName(int firstIndex, int count)
        {
            SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(_context);
            IList<int> ids = sqlExecutor.Document_GetPageOfRootDocumentIDsOrderedByName(firstIndex, count).ToArray();
            IList<Document> entities = GetManyByID(ids);
            return entities;
        }

        public override int CountRootDocuments()
        {
            int count = _context.Session.QueryOver<Document>()
                                        .Where(x => x.ParentDocument == null)
                                        .RowCount();
            return count;
        }

        
        /// <summary>
        /// TODO: Not finished! Does not do that much eager loading yet.
        /// </summary>
        public override Document TryGetComplete(int id)
        {
            Curve audioFileOutput = null;
            Curve curve = null;
            Sample sample = null;
            Scale scale = null;
            Document childDocument = null;
            Patch patch = null;
            Patch curve2 = null;
            Sample sample2 = null;

            var query1 = _context.Session.QueryOver<Document>()
                                        .Where(x => x.ID == id)
                                        .JoinAlias(x => x.AudioFileOutputs, () => audioFileOutput, JoinType.LeftOuterJoin)
                                        .JoinAlias(x => x.Curves, () => curve, JoinType.LeftOuterJoin)
                                        .JoinAlias(x => x.Samples, () => sample, JoinType.LeftOuterJoin)
                                        .JoinAlias(x => x.Scales, () => scale, JoinType.LeftOuterJoin)
                                        .JoinQueryOver(x => x.ChildDocuments, () => childDocument, JoinType.LeftOuterJoin);
                                            //.JoinAlias(x => x.Patches, () => patch, JoinType.LeftOuterJoin)
                                            //.JoinAlias(x => x.Curves, () => curve2, JoinType.LeftOuterJoin)
                                            //.JoinAlias(x => x.Samples, () => sample2, JoinType.LeftOuterJoin);
    
            query1.TransformUsing(Transformers.DistinctRootEntity);
            query1.Future<Document>();

            // TODO: This makes it a TryGet.
            Document document = query1.SingleOrDefault();

            return document;
        }

        private IList<Document> GetManyByID(IList<int> ids)
        {
            if (ids == null) throw new NullException(() => ids);

            IList<Document> list = new List<Document>(ids.Count);
            foreach (int id in ids)
            {
                Document entity = Get(id);
                list.Add(entity);
            }

            return list;
        }
    }
}
