using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Validation
{
    public class AudioFileOutputValidator_InDocument : FluentValidator<AudioFileOutput>
    {
        public AudioFileOutputValidator_InDocument(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Execute<AudioFileOutputValidator>();

            Execute(new NameValidator(Object.Name));
            
            // TODO: Consider if more additional constraints need to be enforced in a document e.g. reference constraints. Name unicity constraint.
        }
    }
}