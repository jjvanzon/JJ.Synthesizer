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
        }
    }
}