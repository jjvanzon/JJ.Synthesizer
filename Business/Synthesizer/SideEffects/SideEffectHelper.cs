using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Mathematics;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal static class SideEffectHelper
	{
		private const int MIN_RANDOM_X = 50;
		private const int MAX_RANDOM_X = 300;
		private const int MIN_RANDOM_Y = 50;
		private const int MAX_RANDOM_Y = 400;

		public static string GenerateName<TEntity>(IEnumerable<string> existingNames)
		{
			if (existingNames == null) throw new NullException(() => existingNames);

			HashSet<string> canonicalExistingNamesHashSet = existingNames.Select(NameHelper.ToCanonical).ToHashSet();

			int number = 1;
			string suggestedName;
			bool nameExists;

			string entityDisplayName = ResourceFormatter.GetDisplayName(typeof(TEntity).Name);

			do
			{
				suggestedName = $"{entityDisplayName} {number++}";
				nameExists = canonicalExistingNamesHashSet.Contains(NameHelper.ToCanonical(suggestedName));
			}
			while (nameExists);

			return suggestedName;
		}

		public static EntityPosition CreateEntityPosition(IEntityPositionRepository entityPositionRepository, IIDRepository idRepository)
		{
			if (entityPositionRepository == null) throw new ArgumentNullException(nameof(entityPositionRepository));
			if (idRepository == null) throw new ArgumentNullException(nameof(idRepository));

			var entityPosition = new EntityPosition
			{
				ID = idRepository.GetID(),
				X = Randomizer.GetInt32(MIN_RANDOM_X, MAX_RANDOM_X),
				Y = Randomizer.GetInt32(MIN_RANDOM_Y, MAX_RANDOM_Y)
			};

			entityPositionRepository.Insert(entityPosition);
			return entityPosition;
		}
	}
}
