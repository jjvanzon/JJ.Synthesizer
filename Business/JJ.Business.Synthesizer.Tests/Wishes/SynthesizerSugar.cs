using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugar
    {
        public SampleManager Samples { get; }

        public SynthesizerSugar()
            : this(PersistenceHelper.CreateContext())
        { }

        public SynthesizerSugar(IContext context)
            : base(PersistenceHelper.CreateRepository<IOperatorRepository>(context),
                   PersistenceHelper.CreateRepository<IInletRepository>(context),
                   PersistenceHelper.CreateRepository<IOutletRepository>(context),
                   PersistenceHelper.CreateRepository<ICurveInRepository>(context),
                   PersistenceHelper.CreateRepository<IValueOperatorRepository>(context),
                   PersistenceHelper.CreateRepository<ISampleOperatorRepository>(context))
        {
            InitializeAudioFileOutputWishes(context);
            InitializeCurveWishes(context);
            InitializeOperatorWishes();
            Samples = TestHelper.CreateSampleManager(context);
        }
    }
}