using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Warnings
{
    public class DocumentWarningValidator_Recursive : FluentValidator<Document>
    {
        private ISampleRepository _sampleRepository;
        private HashSet<object> _alreadyDone;

        public DocumentWarningValidator_Recursive(Document document, ISampleRepository sampleRepository, HashSet<object> alreadyDone)
            : base(document, postponeExecute: true)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            _sampleRepository = sampleRepository;
            _alreadyDone = alreadyDone;

            Execute();
        }

        protected override void Execute()
        {
            Document document = Object;

            if (_alreadyDone.Contains(document))
            {
                return;
            }
            _alreadyDone.Add(document);

            foreach (Patch patch in document.Patches)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.Patch, patch.Name);
                Execute(new PatchWarningValidator_Recursive(patch, _sampleRepository, _alreadyDone), messagePrefix);
            }

            foreach (Sample sample in document.Samples)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.Sample, sample.Name);
                Execute(new SampleWarningValidator(sample, _alreadyDone), messagePrefix);
            }

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.AudioFileOutput, audioFileOutput.Name);
                Execute(new AudioFileOutputWarningValidator(audioFileOutput), messagePrefix);
            }

            foreach (Document instrument in document.Instruments)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.Instrument, instrument.Name);
                Execute(new DocumentWarningValidator_Recursive(instrument, _sampleRepository, _alreadyDone));
            }

            foreach (Document effect in document.Effects)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(PropertyDisplayNames.Instrument, effect.Name);
                Execute(new DocumentWarningValidator_Recursive(effect, _sampleRepository, _alreadyDone));
            }

            // TODO:

            // DocumentWarningValidator_Baseic?
            // Curves?
            // AsInstrumentInDocument
            // AsEffectInDocument
            // DependentOnDocuments
            // DependentDocuments
            // MainPatch

            // TODO: Compare to validation in Circle code base.
        }
    }
}