using System;
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

        public SynthWishes WithName(string name)
        {
            PickedName = name;
            return this;
        }
 
        private string UseName()
        {
            var name = PickedName;
            PickedName = default;
            return name;
        }
    }
}