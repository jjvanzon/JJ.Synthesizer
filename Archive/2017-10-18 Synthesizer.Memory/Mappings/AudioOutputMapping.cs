﻿using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Memory.Helpers;
using JJ.Framework.Data.Memory;

namespace JJ.Data.Synthesizer.Memory.Mappings
{
    public class AudioOutputMapping : MemoryMapping<AudioOutput>
    {
        public AudioOutputMapping()
        {
            IdentityType = IdentityType.Assigned;
            IdentityPropertyName = nameof(AudioOutput.ID);
        }
    }
}
