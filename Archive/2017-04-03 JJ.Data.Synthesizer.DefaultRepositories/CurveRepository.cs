﻿using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class CurveRepository : RepositoryBase<Curve, int>, ICurveRepository
    {
        public CurveRepository(IContext context)
            : base(context)
        { }
    }
}
