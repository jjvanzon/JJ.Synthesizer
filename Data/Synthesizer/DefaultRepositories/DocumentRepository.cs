﻿using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;
using JJ.Framework.Exceptions.Aggregates;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class DocumentRepository : RepositoryBase<Document, int>, IDocumentRepository
    {
        // ReSharper disable once MemberCanBeProtected.Global
        public DocumentRepository(IContext context)
            : base(context) { }

        public virtual IList<Document> GetAll() => _context.Query<Document>().ToArray();

        public virtual IList<Document> OrderByName()
            => _context.Query<Document>()
                       .OrderBy(x => x.Name)
                       .ToArray();

        // By default do it with lazy loading after all.
        public virtual Document TryGetComplete(int id) => TryGet(id);

        public Document GetComplete(int id)
        {
            Document document = TryGetComplete(id);

            if (document == null)
            {
                throw new NotFoundException<Document>(id);
            }

            return Get(id);
        }

        public Document GetByName(string name)
        {
            Document entity = TryGetByName(name);

            if (entity == null)
            {
                throw new NotFoundException<Document>(new { name });
            }

            return entity;
        }

        public Document GetByNameComplete(string name)
        {
            Document entity = GetByName(name);
            entity = GetComplete(entity.ID);
            return entity;
        }

        public virtual Document TryGetByName(string name) => throw new RepositoryMethodNotImplementedException();
    }
}