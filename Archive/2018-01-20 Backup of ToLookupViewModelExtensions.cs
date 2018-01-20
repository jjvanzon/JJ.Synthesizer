//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Data.Canonical;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Presentation.Synthesizer.ToViewModel
//{
//	internal static class ToLookupViewModelExtensions
//	{
//		public static IList<IDAndName> ToLookupViewModels(this IList<Scale> entities)
//		{
//			if (entities == null) throw new ArgumentNullException(nameof(entities));

//			List<IDAndName> viewModels = entities.Select(x => x.ToIDAndName()).ToList();

//			return viewModels;
//		}
//	}
//}
