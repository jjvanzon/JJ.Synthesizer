﻿using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class NodeTypeRepository : DefaultRepositories.NodeTypeRepository
    {
        public NodeTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Off");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Block");
            RepositoryHelper.EnsureEnumEntity(this, 3, "Line");
            RepositoryHelper.EnsureEnumEntity(this, 4, "Curve");
       }
    }
}