using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;
// ReSharper disable UnusedMember.Global

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	public class ChannelRepository : RepositoryBase<Channel, int>, IChannelRepository
	{
		public ChannelRepository(IContext context)
			: base(context)
		{ }

		/// <summary>
		/// Does not get the related entities immediately unless you override it in a specialized repository.
		/// </summary>
		public virtual Channel GetWithRelatedEntities(int id) => Get(id);
	}
}
