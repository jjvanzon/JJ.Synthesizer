using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Names;
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
                                        .Where(x => String.Equals(x.OperatorTypeName, PropertyNames.PatchOutlet))
                                        .FirstOrDefault();

            Operator sampleOperator = patch.Operators
                                           .Where(x => String.Equals(x.OperatorTypeName, PropertyNames.SampleOperator))
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
                    PropertyKey = PropertyNames.SampleOperator,
                    Text = "Please add a SampleOperator to your Patch in order to play a sound."
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

                    var sampleOperatorWrapper = new SampleOperatorWrapper(sampleOperator, sampleRepository);
                    sampleOperatorWrapper.Sample = sample;

                    AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
                    audioFileOutput.FilePath = _outputFilePath;
                    audioFileOutput.Duration = DEFAULT_DURATION;

                    var patchOutletWrapper = new PatchOutletWrapper(patchOutlet);
                    Outlet outlet = patchOutletWrapper.Result;
                    audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

                    IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(curveRepository, sampleRepository, audioFileOutput);
                    audioFileOutputCalculator.Execute();

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
            ISampleRepository sampleRepository = PersistenceHelper.CreateMemoryRepository<ISampleRepository>(context);
            ISampleDataTypeRepository sampleDataTypeRepository = PersistenceHelper.CreateMemoryRepository<ISampleDataTypeRepository>(context);
            ISpeakerSetupRepository speakerSetupRepository = PersistenceHelper.CreateMemoryRepository<ISpeakerSetupRepository>(context);
            IAudioFileFormatRepository audioFileFormatRepository = PersistenceHelper.CreateMemoryRepository<IAudioFileFormatRepository>(context);
            IInterpolationTypeRepository interpolationTypeRepository = PersistenceHelper.CreateMemoryRepository<IInterpolationTypeRepository>(context);

            var manager = new SampleManager(sampleRepository, sampleDataTypeRepository, speakerSetupRepository, audioFileFormatRepository, interpolationTypeRepository);
            return manager;
        }

        private static AudioFileOutputManager CreateAudioFileOutputManager(IContext context)
        {
            IAudioFileOutputRepository audioFileOutputRepository = PersistenceHelper.CreateMemoryRepository<IAudioFileOutputRepository>(context);
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository = PersistenceHelper.CreateMemoryRepository<IAudioFileOutputChannelRepository>(context);
            ISampleDataTypeRepository sampleDataTypeRepository = PersistenceHelper.CreateMemoryRepository<ISampleDataTypeRepository>(context);
            ISpeakerSetupRepository speakerSetupRepository = PersistenceHelper.CreateMemoryRepository<ISpeakerSetupRepository>(context);
            IAudioFileFormatRepository audioFileFormatRepository = PersistenceHelper.CreateMemoryRepository<IAudioFileFormatRepository>(context);

            var manager = new AudioFileOutputManager(audioFileOutputRepository, audioFileOutputChannelRepository, sampleDataTypeRepository, speakerSetupRepository, audioFileFormatRepository);
            return manager;
        }
    }
}
