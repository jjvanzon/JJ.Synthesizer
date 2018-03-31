using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	[UsedImplicitly]
	public class AudioOutputRepository : RepositoryBase<AudioOutput, int>, IAudioOutputRepository
	{
		public AudioOutputRepository(IContext context)
			: base(context)
		{ }
	}
}
