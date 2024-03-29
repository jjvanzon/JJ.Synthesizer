﻿using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class SampleRepository : RepositoryBase<Sample, int>, ISampleRepository
    {
        public SampleRepository(IContext context)
            : base(context)
        { }

        /// <summary>
        /// does nothing
        /// </summary>
        public virtual void SetBytes(int id, byte[] bytes)
        {
            throw new NotSupportedException("Bytes can only be accessed using a specialized repository.");
        }

        /// <summary>
        /// does nothing
        /// </summary>
        public virtual byte[] TryGetBytes(int id)
        {
            throw new NotSupportedException("Bytes can only be accessed using a specialized repository.");
        }
    }
}
