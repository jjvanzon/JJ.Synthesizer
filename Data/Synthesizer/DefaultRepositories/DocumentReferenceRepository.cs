using JetBrains.Annotations;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
	[UsedImplicitly]
	public class DocumentReferenceRepository : RepositoryBase<DocumentReference, int>, IDocumentReferenceRepository
	{
		public DocumentReferenceRepository(IContext context)
			: base(context)
		{ }
	}
}
