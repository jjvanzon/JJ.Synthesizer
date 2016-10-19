using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator_UniqueName : FluentValidator<AudioFileOutput>
    {
        /// <summary>
        /// NOTE:
        /// Do not always execute this validator everywhere,
        /// because then validating a document becomes inefficient.
        /// Extensive document validation will include validating that the AudioFileOutput names are unique already
        /// and it will do so in a more efficient way.
        /// </summary>
        public AudioFileOutputValidator_UniqueName(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object.Document == null)
            {
                return;
            }

            bool isUnique = ValidationHelper.AudioFileOutputNameIsUnique(Object);
            if (!isUnique)
            {
                string message = MessageFormatter.NotUnique_WithPropertyName_AndValue(CommonTitles.Name, Object.Name);
                ValidationMessages.Add(PropertyNames.Name, message);
            }
        }
    }
}
