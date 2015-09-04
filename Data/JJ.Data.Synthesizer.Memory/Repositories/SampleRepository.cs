using JJ.Framework.Data;
using System.Collections.Generic;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class SampleRepository : DefaultRepositories.SampleRepository
    {
        private Dictionary<int, byte[]> _bytesDictionary = new Dictionary<int, byte[]>();
        private object _bytesDictionaryLock = new object();

        public SampleRepository(IContext context)
            : base(context)
        { }

        public override byte[] TryGetBytes(int id)
        {
            // Trigger exception when no entity.
            Get(id);

            lock (_bytesDictionaryLock)
            {
                // Be tollerant towards existence in dictionary, because it is all about exisitence of the entity.
                byte[] bytes;
                _bytesDictionary.TryGetValue(id, out bytes);
                return bytes;
            }
        }

        public override void SetBytes(int id, byte[] bytes)
        {
            // Trigger exception when no entity.
            Get(id);

            lock (_bytesDictionaryLock)
            {
                _bytesDictionary[id] = bytes;
            }
        }
    }
}