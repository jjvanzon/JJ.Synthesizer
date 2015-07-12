using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class OperatorTypeRepository : JJ.Data.Synthesizer.DefaultRepositories.OperatorTypeRepository
    {
        public OperatorTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Add");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Adder");
            RepositoryHelper.EnsureEnumEntity(this, 3, "Divide");
            RepositoryHelper.EnsureEnumEntity(this, 4, "Multiply");
            RepositoryHelper.EnsureEnumEntity(this, 5, "PatchInlet");
            RepositoryHelper.EnsureEnumEntity(this, 6, "PatchOutlet");
            RepositoryHelper.EnsureEnumEntity(this, 7, "Power");
            RepositoryHelper.EnsureEnumEntity(this, 8, "Sine");
            RepositoryHelper.EnsureEnumEntity(this, 9, "Substract");
            RepositoryHelper.EnsureEnumEntity(this, 10, "TimeAdd");
            RepositoryHelper.EnsureEnumEntity(this, 11, "TimeDivide");
            RepositoryHelper.EnsureEnumEntity(this, 12, "TimeMultiply");
            RepositoryHelper.EnsureEnumEntity(this, 13, "TimePower");
            RepositoryHelper.EnsureEnumEntity(this, 14, "TimeSubstract");
            RepositoryHelper.EnsureEnumEntity(this, 15, "Value");
            RepositoryHelper.EnsureEnumEntity(this, 16, "CurveIn");
            RepositoryHelper.EnsureEnumEntity(this, 17, "Sample");
            RepositoryHelper.EnsureEnumEntity(this, 18, "WhiteNoise");
            RepositoryHelper.EnsureEnumEntity(this, 19, "Resample");
        }          
    }
}