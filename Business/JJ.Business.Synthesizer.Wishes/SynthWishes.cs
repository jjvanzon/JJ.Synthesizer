using System;
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
        private readonly IAudioFileFormatRepository _audioFileFormatRepository;
        private readonly ISampleDataTypeRepository  _sampleDataTypeRepository;

        public IContext Context { get; }
        public SampleManager Samples { get; }

        public SynthWishes()
            : this(PersistenceHelper.CreateContext())
        { }

        public SynthWishes(IContext context)
            : base(PersistenceHelper.CreateRepository<IOperatorRepository>(context),
                   PersistenceHelper.CreateRepository<IInletRepository>(context),
                   PersistenceHelper.CreateRepository<IOutletRepository>(context),
                   PersistenceHelper.CreateRepository<ICurveInRepository>(context),
                   PersistenceHelper.CreateRepository<IValueOperatorRepository>(context),
                   PersistenceHelper.CreateRepository<ISampleOperatorRepository>(context))
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
            
            _audioFileFormatRepository = PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(context);
            _sampleDataTypeRepository  = PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(context);

            Samples = ServiceFactory.CreateSampleManager(context);

            InitializeAudioFileOutputWishes(context);
            InitializeCurveWishes(context);
            InitializeOperatorWishes();
        }
    }
}