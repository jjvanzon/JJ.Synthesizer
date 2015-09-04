using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;

namespace JJ.Business.Synthesizer.SideEffects
{
    public class Curve_SideEffect_GenerateName : ISideEffect
    {
        private Curve _entity;

        public Curve_SideEffect_GenerateName(Curve entity)
        {
            if (entity == null) throw new NullException(() => entity);
            _entity = entity;
        }

        public void Execute()
        {
            if (_entity.Document == null) throw new NullException(() => _entity.Document);

            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.Curve, number++);
                nameExists = _entity.Document.Curves.Where(x => String.Equals(x.Name, suggestedName)).Any();
            }
            while (nameExists);

            _entity.Name = suggestedName;
        }
    }
}
