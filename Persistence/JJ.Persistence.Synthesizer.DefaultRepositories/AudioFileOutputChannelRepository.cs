using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    public class AudioFileOutputChannelRepository : RepositoryBase<AudioFileOutputChannel, int>, IAudioFileOutputChannelRepository
    {
        public AudioFileOutputChannelRepository(IContext context)
            : base(context)
        { }
    }
}
