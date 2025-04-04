﻿using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class AudioFileFormatRepository : JJ.Persistence.Synthesizer.DefaultRepositories.AudioFileFormatRepository
    {
        private static readonly object _lock = new object();

        public AudioFileFormatRepository(IContext context)
            : base(context)
        {
            AudioFileFormat entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs
            
            lock (_lock)
            {
                entity = TryGet(1);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Raw";
                }

                entity = TryGet(2);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Wav";
                }
            }
        }
    }
}