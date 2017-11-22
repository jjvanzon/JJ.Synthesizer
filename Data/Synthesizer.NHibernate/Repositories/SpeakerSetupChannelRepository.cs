using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Data.NHibernate;

namespace JJ.Data.Synthesizer.NHibernate.Repositories
{
	public class SpeakerSetupChannelRepository : DefaultRepositories.SpeakerSetupChannelRepository
	{
		private new readonly NHibernateContext _context;

		public SpeakerSetupChannelRepository(IContext context) : base(context)
		{
			_context = (NHibernateContext)context;
		}

		public override IList<SpeakerSetupChannel> GetAll() => _context.Session.QueryOver<SpeakerSetupChannel>().List();
	}
}
