using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
	public interface ISpeakerSetupChannelRepository : IRepository<SpeakerSetupChannel, int>
	{
		// ReSharper disable once UnusedMember.Global
		IList<SpeakerSetupChannel> GetAll();
	}
}
