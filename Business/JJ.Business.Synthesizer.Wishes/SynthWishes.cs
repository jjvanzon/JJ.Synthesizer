using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
// ReSharper disable RedundantIfElseBlock

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
            Context = context ?? throw new ArgumentNullException(nameof(context));

            InitializeAudioFileWishes(context);
            InitializeCurveWishes(context);
            InitializeOperatorWishes(context);
        }

        private string PickedName { get; set; }

        public SynthWishes WithName([CallerMemberName] string uglyName = null)
        {
            PickedName = NameHelper.GetPrettyName(uglyName);
            return this;
        }

        private string UseName()
        {
            var name = PickedName;
            PickedName = null;
            return name;
        }

        // Duration

        private FluentOutlet _duration;
        public FluentOutlet Duration
        {
            get => _duration ?? _[1];
            set => _duration = value ?? _[1];
        }

        public SynthWishes WithDuration(Outlet duration) => WithDuration(_[duration]);
        public SynthWishes WithDuration(double duration) => WithDuration(_[duration]);
        public SynthWishes WithDuration(FluentOutlet duration)
        {
            Duration = duration ?? _[1];
            return this;
        }
        
        public SynthWishes AddDuration(Outlet duration) => AddDuration(_[duration]);
        public SynthWishes AddDuration(double duration) => AddDuration(_[duration]);
        public SynthWishes AddDuration(FluentOutlet additionalDuration)
        {
            return WithDuration(Duration + additionalDuration);
        }
    }
}