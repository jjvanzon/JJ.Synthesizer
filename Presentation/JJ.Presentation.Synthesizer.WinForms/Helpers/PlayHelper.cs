using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Data.Memory;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class PlayHelper
    {
        private const double DEFAULT_DURATION = 10;

        private static string _sampleFilePath;
        private static string _outputFilePath;

        static PlayHelper()
        {
            ConfigurationSection config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            _sampleFilePath = config.FilePaths.SampleFilePath;
            _outputFilePath = config.FilePaths.OutputFilePath;
        }

        public static VoidResult Play(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            var result = new VoidResult
            {
                Messages = new List<Message>()
            };

            Operator patchOutlet = patch.Operators
                                        .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.PatchOutlet)
                                        .FirstOrDefault();

            Operator sampleOperator = patch.Operators
                                           .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.Sample)
                                           .FirstOrDefault();

            if (patchOutlet == null)
            {
                result.Successful = false;
                result.Messages.Add(new Message
                {
                    // TODO: Use string resources.
                    PropertyKey = PropertyNames.PatchOutlet,
                    Text = "Please add a PatchOutlet to your Patch in order to play a sound."
                });
                return result;
            }

            if (sampleOperator == null)
            {
                result.Successful = false;
                result.Messages.Add(new Message
                {
                    // TODO: Use string resources.
                    PropertyKey = PropertyNames.Sample,
                    Text = "Please add a Sample operator to your Patch in order to play a sound."
                });
                return result;
            }

            if (!File.Exists(_sampleFilePath))
            {
                result.Successful = false;
                result.Messages.Add(new Message
                {
                    // TODO: Use string resources.
                    PropertyKey = PropertyNames.Patch,
                    Text =  String.Format("Input sample does not exist. Please put a file in the following location:{0}{1}", Environment.NewLine, Path.GetFullPath(_sampleFilePath))
                });
                return result;
            }

            using (IContext context = PersistenceHelper.CreateMemoryContext())
            {
                ISampleRepository sampleRepository = PersistenceHelper.CreateMemoryRepository<ISampleRepository>(context);
                ICurveRepository curveRepository = PersistenceHelper.CreateMemoryRepository<ICurveRepository>(context);

                SampleManager sampleManager = CreateSampleManager(context);
                AudioFileOutputManager audioFileOutputManager = CreateAudioFileOutputManager(context);

                using (Stream sampleStream = new FileStream(_sampleFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Sample sample = sampleManager.CreateSample(sampleStream);

                    var sampleOperatorWrapper = new Sample_OperatorWrapper(sampleOperator, sampleRepository);
                    sampleOperatorWrapper.Sample = sample;

                    AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                    audioFileOutput.FilePath = _outputFilePath;
                    audioFileOutput.Duration = DEFAULT_DURATION;

                    var patchOutletWrapper = new PatchOutlet_OperatorWrapper(patchOutlet);
                    Outlet outlet = patchOutletWrapper.Result;
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

                    audioFileOutputManager.Execute(audioFileOutput);

                    SoundPlayer soundPlayer = new SoundPlayer(_outputFilePath);
                    soundPlayer.Play();

                    sampleOperatorWrapper.Sample = null;

                    return new VoidResult
                    {
                        Successful = true,
                        Messages = new Message[0]
                    };
                }
            }
        }

        private static SampleManager CreateSampleManager(IContext context)
        {
            var sampleRepositories = new SampleRepositories(
                PersistenceHelper.CreateMemoryRepository<IDocumentRepository>(context),
                PersistenceHelper.CreateMemoryRepository<ISampleRepository>(context),
                PersistenceHelper.CreateMemoryRepository<IAudioFileFormatRepository>(context),
                PersistenceHelper.CreateMemoryRepository<ISampleDataTypeRepository>(context),
                PersistenceHelper.CreateMemoryRepository<ISpeakerSetupRepository>(context),
                PersistenceHelper.CreateMemoryRepository<IInterpolationTypeRepository>(context));

            var manager = new SampleManager(sampleRepositories);
            return manager;
        }

        private static AudioFileOutputManager CreateAudioFileOutputManager(IContext context)
        {
            IAudioFileOutputRepository audioFileOutputRepository = PersistenceHelper.CreateMemoryRepository<IAudioFileOutputRepository>(context);
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository = PersistenceHelper.CreateMemoryRepository<IAudioFileOutputChannelRepository>(context);
            ISampleDataTypeRepository sampleDataTypeRepository = PersistenceHelper.CreateMemoryRepository<ISampleDataTypeRepository>(context);
            ISpeakerSetupRepository speakerSetupRepository = PersistenceHelper.CreateMemoryRepository<ISpeakerSetupRepository>(context);
            IAudioFileFormatRepository audioFileFormatRepository = PersistenceHelper.CreateMemoryRepository<IAudioFileFormatRepository>(context);
            ICurveRepository curveRepository = PersistenceHelper.CreateMemoryRepository<ICurveRepository>(context);
            ISampleRepository sampleRepository = PersistenceHelper.CreateMemoryRepository<ISampleRepository>(context);

            var manager = new AudioFileOutputManager(
                audioFileOutputRepository, 
                audioFileOutputChannelRepository, 
                sampleDataTypeRepository, 
                speakerSetupRepository, 
                audioFileFormatRepository,
                curveRepository,
                sampleRepository);

            return manager;
        }
    }
}
