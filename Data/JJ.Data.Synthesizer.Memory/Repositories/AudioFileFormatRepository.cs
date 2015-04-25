using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class AudioFileFormatRepository : JJ.Data.Synthesizer.DefaultRepositories.AudioFileFormatRepository
    {
        public AudioFileFormatRepository(IContext context)
            : base(context)
        {
            AudioFileFormat entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            entity = Create();
            entity.Name = "Raw";

            entity = Create();
            entity.Name = "Wav";
        }
    }
}