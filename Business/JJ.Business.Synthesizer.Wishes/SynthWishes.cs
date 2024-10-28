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

        private FluentOutlet _audioLength;
        public FluentOutlet AudioLength
        {
            get => _audioLength ?? _[1];
            set => _audioLength = value ?? _[1];
        }

        public SynthWishes WithAudioLength(Outlet audioLength) => WithAudioLength(_[audioLength]);
        public SynthWishes WithAudioLength(double audioLength) => WithAudioLength(_[audioLength]);
        public SynthWishes WithAudioLength(FluentOutlet audioLength)
        {
            AudioLength = audioLength ?? _[1];
            return this;
        }
        
        public SynthWishes AddAudioLength(Outlet audioLength) => AddAudioLength(_[audioLength]);
        public SynthWishes AddAudioLength(double audioLength) => AddAudioLength(_[audioLength]);
        public SynthWishes AddAudioLength(FluentOutlet addedLength)
        {
            return WithAudioLength(AudioLength + addedLength);
        }
    }
}