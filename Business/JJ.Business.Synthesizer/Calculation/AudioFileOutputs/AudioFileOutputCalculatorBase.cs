using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal abstract class AudioFileOutputCalculatorBase : IAudioFileOutputCalculator
    {
        protected string _filePath;

        /// <summary>
        /// The base class will verify the data.
        /// </summary>
        protected AudioFileOutput _audioFileOutput;

        public AudioFileOutputCalculatorBase(AudioFileOutput audioFileOutput, string filePath)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            if (String.IsNullOrEmpty(filePath)) 
            {
                filePath = audioFileOutput.FilePath;
            }

            if (String.IsNullOrEmpty(filePath)) throw new Exception("Either filePath must be passed explicitly or audioFileOutput.FilePath must be filled in.");

            // TODO: I have never done this before. I wonder if it is a good idea.
            IValidator validator = new AudioFileOutputValidator(audioFileOutput);
            validator.Verify();

            _audioFileOutput = audioFileOutput;
            _filePath = filePath;
        }

        public abstract void Execute();
    }
}
