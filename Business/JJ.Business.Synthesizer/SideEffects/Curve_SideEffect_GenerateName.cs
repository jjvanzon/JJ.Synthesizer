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
    /// Fills in a name in the Instrument Document, that is like: 'Instrument 1'.
    /// If 'Instrument 1' already exists, it tries 'Instrument 2' etcetera.
    /// </summary>
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

            Document document = _entity.Document;

            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.Curve, number++);
                nameExists = document.Curves.Where(x => String.Equals(x.Name, suggestedName)).Any();
            }
            while (nameExists);

            _entity.Name = suggestedName;
        }
    }
}
