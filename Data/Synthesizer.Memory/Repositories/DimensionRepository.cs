using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class DimensionRepository : DefaultRepositories.DimensionRepository
    {
        public DimensionRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Signal");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Frequency");
            RepositoryHelper.EnsureEnumEntity(this, 3, "Volume");
            RepositoryHelper.EnsureEnumEntity(this, 4, "Frequencies");
            RepositoryHelper.EnsureEnumEntity(this, 5, "Volumes");
            RepositoryHelper.EnsureEnumEntity(this, 6, "VibratoSpeed");
            RepositoryHelper.EnsureEnumEntity(this, 7, "VibratoDepth");
            RepositoryHelper.EnsureEnumEntity(this, 8, "TremoloSpeed");
            RepositoryHelper.EnsureEnumEntity(this, 9, "TremoloDepth");
            RepositoryHelper.EnsureEnumEntity(this, 10, "NoteDuration");
            RepositoryHelper.EnsureEnumEntity(this, 11, "AttackDuration");
            RepositoryHelper.EnsureEnumEntity(this, 12, "ReleaseDuration");
            RepositoryHelper.EnsureEnumEntity(this, 13, "NoteStart");
            RepositoryHelper.EnsureEnumEntity(this, 14, "Brightness");
            RepositoryHelper.EnsureEnumEntity(this, 15, "Intensity");
            RepositoryHelper.EnsureEnumEntity(this, 16, "BrightnessModulationSpeed");
            RepositoryHelper.EnsureEnumEntity(this, 17, "BrightnessModulationDepth");
            RepositoryHelper.EnsureEnumEntity(this, 18, "IntensityModulationSpeed");
            RepositoryHelper.EnsureEnumEntity(this, 19, "IntensityModulationDepth");
            RepositoryHelper.EnsureEnumEntity(this, 20, "DecayDuration");
            RepositoryHelper.EnsureEnumEntity(this, 21, "SustainVolume");
            RepositoryHelper.EnsureEnumEntity(this, 22, "Time");
            RepositoryHelper.EnsureEnumEntity(this, 23, "Channel");
            RepositoryHelper.EnsureEnumEntity(this, 24, "Harmonic");
            RepositoryHelper.EnsureEnumEntity(this, 25, "Balance");
            RepositoryHelper.EnsureEnumEntity(this, 26, "Sound");
            RepositoryHelper.EnsureEnumEntity(this, 27, "Phase");
            RepositoryHelper.EnsureEnumEntity(this, 28, "PreviousPosition");
            RepositoryHelper.EnsureEnumEntity(this, 29, "Origin");
            RepositoryHelper.EnsureEnumEntity(this, 30, "Input");
            RepositoryHelper.EnsureEnumEntity(this, 31, "Result");
            RepositoryHelper.EnsureEnumEntity(this, 32, "A");
            RepositoryHelper.EnsureEnumEntity(this, 33, "B");
            RepositoryHelper.EnsureEnumEntity(this, 36, "Base");
            RepositoryHelper.EnsureEnumEntity(this, 37, "Exponent");
            RepositoryHelper.EnsureEnumEntity(this, 38, "Difference");
            RepositoryHelper.EnsureEnumEntity(this, 39, "Factor");
            RepositoryHelper.EnsureEnumEntity(this, 40, "SamplingRate");
            RepositoryHelper.EnsureEnumEntity(this, 41, "Start");
            RepositoryHelper.EnsureEnumEntity(this, 42, "End");
            RepositoryHelper.EnsureEnumEntity(this, 43, "Low");
            RepositoryHelper.EnsureEnumEntity(this, 44, "High");
            RepositoryHelper.EnsureEnumEntity(this, 45, "Ratio");
            RepositoryHelper.EnsureEnumEntity(this, 46, "PitchBend");
            RepositoryHelper.EnsureEnumEntity(this, 47, "MainVolume");
            RepositoryHelper.EnsureEnumEntity(this, 48, "Number");
            RepositoryHelper.EnsureEnumEntity(this, 49, "SliceLength");
            RepositoryHelper.EnsureEnumEntity(this, 50, "SampleCount");
            RepositoryHelper.EnsureEnumEntity(this, 51, "From");
            RepositoryHelper.EnsureEnumEntity(this, 52, "Till");
            RepositoryHelper.EnsureEnumEntity(this, 53, "Step");
            RepositoryHelper.EnsureEnumEntity(this, 54, "Item");
            RepositoryHelper.EnsureEnumEntity(this, 55, "Width");
            RepositoryHelper.EnsureEnumEntity(this, 56, "BlobVolume");
            RepositoryHelper.EnsureEnumEntity(this, 58, "PassThrough");
            RepositoryHelper.EnsureEnumEntity(this, 59, "Reset");
            RepositoryHelper.EnsureEnumEntity(this, 60, "Collection");
            RepositoryHelper.EnsureEnumEntity(this, 61, "Slope");
            RepositoryHelper.EnsureEnumEntity(this, 62, "Decibel");
            RepositoryHelper.EnsureEnumEntity(this, 63, "Condition");
            RepositoryHelper.EnsureEnumEntity(this, 64, "Then");
            RepositoryHelper.EnsureEnumEntity(this, 65, "Else");
            RepositoryHelper.EnsureEnumEntity(this, 66, "Rate");
            RepositoryHelper.EnsureEnumEntity(this, 67, "Offset");
            RepositoryHelper.EnsureEnumEntity(this, 68, "FrequencyCount");
            RepositoryHelper.EnsureEnumEntity(this, 69, "Skip");
            RepositoryHelper.EnsureEnumEntity(this, 70, "LoopStartMarker");
            RepositoryHelper.EnsureEnumEntity(this, 71, "LoopEndMarker");
            RepositoryHelper.EnsureEnumEntity(this, 72, "ReleaseEndMarker");
        }
    }
}