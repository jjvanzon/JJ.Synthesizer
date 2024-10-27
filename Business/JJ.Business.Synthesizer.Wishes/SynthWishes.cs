using System;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;

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
    }
}