using System;
using System.Collections.Generic;
using System.Reflection;
using JJ.Framework.Data.JsonBlobZip;

namespace JJ.Data.Synthesizer.JsonBlobZip
{
    public class SynthesizerJsonBlobZipContext : JsonBlobZipContext
    {
        public SynthesizerJsonBlobZipContext(string location, Assembly modelAssembly, Assembly mappingAssembly, string dialect)
            : base(location, modelAssembly, mappingAssembly, dialect)
        {

        }

        public override TEntity TryGet<TEntity>(object id) => throw new NotImplementedException();

        public override TEntity Create<TEntity>() => throw new NotImplementedException();

        public override void Insert(object entity) => throw new NotImplementedException();

        public override void Update(object entity) => throw new NotImplementedException();

        public override void Delete(object entity) => throw new NotImplementedException();

        public override IEnumerable<TEntity> Query<TEntity>() => throw new NotImplementedException();

        public override void Commit() => throw new NotImplementedException();

        public override void Flush() => throw new NotImplementedException();

        public override void Dispose() => throw new NotImplementedException();

        public override void Rollback() => throw new NotImplementedException();


    }
}