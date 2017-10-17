using JJ.Framework.Data;
using System.Collections.Generic;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class SampleRepository : DefaultRepositories.SampleRepository
    {
        private readonly Dictionary<int, byte[]> _bytesDictionary = new Dictionary<int, byte[]>();
        private readonly object _bytesDictionaryLock = new object();

        public SampleRepository(IContext context)
            : base(context)
        { }

        public override byte[] TryGetBytes(int sampleID)
        {
            // Trigger exception when no entity.
            Get(sampleID);

            lock (_bytesDictionaryLock)
            {
                // Be tolerant towards existence in dictionary, because it is all about exisitence of the entity.
                byte[] bytes;
                _bytesDictionary.TryGetValue(sampleID, out bytes);
                return bytes;
            }
        }

        public override void SetBytes(int sampleID, byte[] bytes)
        {
            // Trigger exception when no entity.
            Get(sampleID);

            lock (_bytesDictionaryLock)
            {
                _bytesDictionary[sampleID] = bytes;
            }
        }
    }
}