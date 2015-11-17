using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Sample_SideEffect_GenerateName : ISideEffect
    {
        private Sample _entity;

        public Sample_SideEffect_GenerateName(Sample entity)
        {
            if (entity == null) throw new NullException(() => entity);
            _entity = entity;
        }

        public void Execute()
        {
            if (_entity.Document == null) throw new NullException(() => _entity.Document);

            HashSet<string> existingNames = _entity.Document.EnumerateSelfAndParentAndTheirChildren()
                                                            .SelectMany(x => x.Samples)
                                                            .Select(x => x.Name)
                                                            .ToHashSet();
            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.Sample, number++);
                nameExists = existingNames.Contains(suggestedName);
            }
            while (nameExists);

            _entity.Name = suggestedName;
        }
    }
}
