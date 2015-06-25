using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class OperatorTypeRepository : JJ.Data.Synthesizer.DefaultRepositories.OperatorTypeRepository
    {
        public OperatorTypeRepository(IContext context)
            : base(context)
        {
            OperatorType entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            entity = Create();
            entity.Name = "Add";

            entity = Create();
            entity.Name = "Adder";

            entity = Create();
            entity.Name = "Divide";

            entity = Create();
            entity.Name = "Multiply";

            entity = Create();
            entity.Name = "PatchInlet";

            entity = Create();
            entity.Name = "PatchOutlet";

            entity = Create();
            entity.Name = "Power";

            entity = Create();
            entity.Name = "Sine";

            entity = Create();
            entity.Name = "Substract";

            entity = Create();
            entity.Name = "TimeAdd";

            entity = Create();
            entity.Name = "TimeDivide";

            entity = Create();
            entity.Name = "TimeMultiply";

            entity = Create();
            entity.Name = "TimePower";

            entity = Create();
            entity.Name = "TimeSubstract";

            entity = Create();
            entity.Name = "Value";

            entity = Create();
            entity.Name = "CurveIn";

            entity = Create();
            entity.Name = "Sample";

            entity = Create();
            entity.Name = "WhiteNoise";
        }
    }
}