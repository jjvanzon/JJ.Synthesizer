using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using System.Linq;
using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Curve_SideEffect_GenerateName : ISideEffect
    {
        private readonly Curve _entity;

        public Curve_SideEffect_GenerateName(Curve entity)
        {
            _entity = entity ?? throw new NullException(() => entity);
        }

        public void Execute()
        {
            bool mustExecute = _entity.Document != null;
            // ReSharper disable once InvertIf
            if (mustExecute)
            {
                IEnumerable<string> existingNames = _entity.Document.Curves.Select(x => x.Name);

                _entity.Name = SideEffectHelper.GenerateName<Curve>(existingNames);
            }
        }
    }
}