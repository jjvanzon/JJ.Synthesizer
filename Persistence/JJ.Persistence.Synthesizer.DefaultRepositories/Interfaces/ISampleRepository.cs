using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces
{
    public interface ISampleRepository : IRepository<Sample, int>
    {
        void SetBinary(int id, byte[] bytes);
        byte[] GetBinary(int id);
    }
}
