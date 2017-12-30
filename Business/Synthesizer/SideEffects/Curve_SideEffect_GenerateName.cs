using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Curve_SideEffect_GenerateName : ISideEffect
	{
		private readonly Curve _curve;
		private readonly Document _document;

		public Curve_SideEffect_GenerateName(Curve curve, Document document)
		{
			_curve = curve ?? throw new ArgumentNullException(nameof(curve));
			_document = document ?? throw new ArgumentNullException(nameof(document));
		}

		public void Execute()
		{
			IEnumerable<string> existingNames = _document.Patches
			                                             .SelectMany(x => x.Operators)
			                                             .Where(x => x.Curve != null)
			                                             .Select(x => x.Curve.Name);

			_curve.Name = SideEffectHelper.GenerateName<Curve>(existingNames);
		}
	}
}