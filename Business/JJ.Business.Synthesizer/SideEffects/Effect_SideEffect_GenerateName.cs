using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.SideEffects
{
    /// <summary>
    /// Fills in a name in the Effect Document, that is like: 'Effect 1'.
    /// If 'Effect 1' already exists, it tries 'Effect 2' etcetera.
    /// </summary>
    public class Effect_SideEffect_GenerateName : ISideEffect
    {
        private Document _effect;

        public Effect_SideEffect_GenerateName(Document effect)
        {
            if (effect == null) throw new NullException(() => effect);
            _effect = effect;
        }

        public void Execute()
        {
            if (_effect.AsEffectInDocument == null) throw new NullException(() => _effect.AsEffectInDocument);

            Document parentDocument = _effect.AsEffectInDocument;

            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.Effect, number++);
                nameExists = parentDocument.Effects.Where(x => String.Equals(x.Name, suggestedName)).Any();
            }
            while (nameExists);

            _effect.Name = suggestedName;
        }
    }
}
