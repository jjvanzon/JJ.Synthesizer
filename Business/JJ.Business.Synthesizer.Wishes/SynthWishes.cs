using System;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public IContext Context { get; }

        public SynthWishes()
            : this(PersistenceHelper.CreateContext())
        { }

        public SynthWishes(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            InitializeSampleWishes(context);
            InitializeCurveWishes(context);
            InitializeOperatorWishes(context);
            InitializeAudioFileWishes();
        }
    }
}