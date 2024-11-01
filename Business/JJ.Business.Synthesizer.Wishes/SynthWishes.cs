using System;
using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public IContext Context { get; }

        private readonly SaveAudioWishes _saveAudioWishes;

        public SynthWishes()
            : this(PersistenceHelper.CreateContext())
        { }

        public SynthWishes(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            InitializeSampleWishes(context);
            InitializeCurveWishes(context);
            InitializeOperatorWishes(context);
            _saveAudioWishes = new SaveAudioWishes(this);
            InitializeParallelWishes();
            InitializePlayWishes();
        }


        private string FormatAudioFileName(string name, AudioFileFormatEnum audioFileFormatEnum)
        {
            string fileName = Path.GetFileNameWithoutExtension(name);
            string fileExtension = audioFileFormatEnum.GetFileExtension();
            fileName += fileExtension;
            return fileName;
        }
    }
}