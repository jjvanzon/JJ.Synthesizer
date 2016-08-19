using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using System.Collections.Generic;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class AudioFileOutput_SideEffect_GenerateName : ISideEffect
    {
        private AudioFileOutput _entity;

        public AudioFileOutput_SideEffect_GenerateName(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            _entity = entity;
        }

        public void Execute()
        {
            if (_entity.Document == null) throw new NullException(() => _entity.Document);

            HashSet<string> existingNamesLowerCase = _entity.Document.EnumerateSelfAndParentAndTheirChildren()
                                                                     .SelectMany(x => x.AudioFileOutputs)
                                                                     .Select(x => x.Name?.ToLower())
                                                                     .ToHashSet();
            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.AudioFileOutput, number++);
                nameExists = existingNamesLowerCase.Contains(suggestedName.ToLower());
            }
            while (nameExists);

            _entity.Name = suggestedName;
        }
    }
}
