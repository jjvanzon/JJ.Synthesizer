using JJ.Data.Synthesizer.NHibernate.Helpers;
using JJ.Data.Synthesizer.SqlClient;
using JJ.Framework.Data.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JJ.Data.Synthesizer.NHibernate
{
    /// <summary>
    /// Specialized IContext that derives from NHibernateContext,
    /// but adds to it the management of byte arrays.
    /// It is a bad plan to map byte arrays using NHibernate,
    /// because then retrieving an entity has a large overhead.
    /// Byte arrays are better managed with separate repository methods,
    /// because then you only retrieve the bytes when you need them.
    /// But the consequence of SQL staments for the byte arrays,
    /// is that only flushed byte arrays can be retreived.
    /// Intermediate flushes are dangerous, because in memory we often work with data that is not yet valid.
    /// Intermediate flushes can also come with a performance penalty if we rollback the transaction afterwards.
    /// And e.g. SampleOperatorWrapper.SampleBytes is so flexibly usable you can count on the bytes being retrieved before committing.
    /// That is why we want to cache the byte arrays in memory.
    /// This also improves performance preventing needless SQL statements.
    /// However, because Synthesizer runs in a stateful environment, the context is long lived, and the repositories are long lived.
    /// We need to purge the cached byte arrays upon rollback, or upon deletion of an entity.
    /// We cannot manage such transactionality in the repository.
    /// We can only manage that in the context.
    /// (In a stateless environment it might work reasonably well if we just leave the byte arrays in cache until the IContext is disposed.)
    /// 
    /// So to be as flexible as to be able to retrieve byte arrays that are not yet flushed,
    /// and not to send byte arrays to SQL server unless we commit,
    /// we need to manage that in an alternative IContext implementation
    /// </summary>
    public class SynthesizerContext : NHibernateContext
    {
        public SynthesizerContext(string connectionString, Assembly modelAssembly, Assembly mappingAssembly, string dialect)
            : base(connectionString, modelAssembly, mappingAssembly, dialect)
        { }

        private object _sampleBytesLock = new object();
        private Dictionary<int, byte[]> _sampleBytesReadDictionary = new Dictionary<int, byte[]>();
        private Dictionary<int, byte[]> _sampleBytesToSaveDictionary = new Dictionary<int, byte[]>();

        public byte[] TryGetSampleBytes(int sampleID)
        {
            lock (_sampleBytesLock)
            {
                byte[] sampleBytes;

                if (_sampleBytesToSaveDictionary.TryGetValue(sampleID, out sampleBytes))
                {
                    return sampleBytes;
                }

                if (_sampleBytesReadDictionary.TryGetValue(sampleID, out sampleBytes))
                {
                    return sampleBytes;
                }

                SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(this);
                sampleBytes = sqlExecutor.Sample_TryGetBytes(sampleID);
                _sampleBytesReadDictionary[sampleID] = sampleBytes;
                return sampleBytes;
            }
        }

        public void SetSampleBytes(int sampleID, byte[] sampleBytes)
        {
            lock (_sampleBytesLock)
            {
                // Do not save if bytes not changed.
                byte[] sampleBytesRead;
                if (_sampleBytesReadDictionary.TryGetValue(sampleID, out sampleBytesRead))
                {
                    bool sampleBytesNotChanged = Object.ReferenceEquals(sampleBytes, sampleBytesRead);
                    if (sampleBytesNotChanged)
                    {
                        return;
                    }
                }

                _sampleBytesToSaveDictionary[sampleID] = sampleBytes;
            }
        }

        public override void Delete(object entity)
        {
            base.Delete(entity);

            Sample sample = entity as Sample;
            if (sample != null)
            {
                lock (_sampleBytesLock)
                {
                    if (_sampleBytesReadDictionary.ContainsKey(sample.ID))
                    {
                        _sampleBytesReadDictionary.Remove(sample.ID);
                    }

                    if (_sampleBytesToSaveDictionary.ContainsKey(sample.ID))
                    {
                        _sampleBytesToSaveDictionary.Remove(sample.ID);
                    }
                }
            }
        }

        public override void Flush()
        {
            base.Flush();

            lock (_sampleBytesLock)
            {
                foreach (var entry in _sampleBytesToSaveDictionary)
                {
                    SynthesizerSqlExecutor sqlExecutor = SqlExecutorHelper.CreateSynthesizerSqlExecutor(this);
                    sqlExecutor.Sample_TrySetBytes(entry.Key, entry.Value);
                }

                _sampleBytesToSaveDictionary.Clear();
            }
        }

        public override void Rollback()
        {
            base.Rollback();

            lock (_sampleBytesLock)
            {
                _sampleBytesToSaveDictionary.Clear();
                _sampleBytesReadDictionary.Clear();
            }
        }

        public override void Commit()
        {
            try
            {
                base.Commit();
            }
            catch
            {
                // When Commit fails in any way (which includes Flush), you want to start with a clean slate.
                lock (_sampleBytesLock)
                {
                    _sampleBytesToSaveDictionary.Clear();
                    _sampleBytesReadDictionary.Clear();
                }
            }
        }
    }
}
