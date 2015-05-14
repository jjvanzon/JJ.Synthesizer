using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class AudioFileOutputRepository : RepositoryBase<AudioFileOutput, int>, IAudioFileOutputRepository
    {
        public AudioFileOutputRepository(IContext context)
            : base(context)
        { }

        public virtual IList<AudioFileOutput> GetManyByDocumentID(int documentID)
        {
            return _context.Query<AudioFileOutput>()
                           .Where(x => x.Document.ID == documentID)
                           .ToArray();
        }
    }
}
