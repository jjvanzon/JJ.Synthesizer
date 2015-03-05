using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    public class SampleFormatRepository : RepositoryBase<SampleFormat, int>, ISampleFormatRepository
    {
        public SampleFormatRepository(IContext context)
            : base(context)
        { }
    }
}
