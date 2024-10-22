using System;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

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
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;

            InitializeAudioFileWishes(context);
            InitializeCurveWishes(context);
            InitializeOperatorWishes(context);
        }
    }
}