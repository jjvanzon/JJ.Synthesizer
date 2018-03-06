using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Document_SideEffect_GenerateName : ISideEffect
	{
		private readonly Document _entity;
		private readonly IDocumentRepository _documentRepository;

		public Document_SideEffect_GenerateName(Document entity, IDocumentRepository documentRepository)
		{
			_entity = entity ?? throw new NullException(() => entity);
			_documentRepository = documentRepository ?? throw new NullException(() => documentRepository);
		}

		public void Execute()
		{
			IEnumerable<string> existingNames = _documentRepository.GetAll().Select(x => x.Name);

			_entity.Name = SideEffectHelper.GenerateName<Document>(existingNames);
		}
	}
}
