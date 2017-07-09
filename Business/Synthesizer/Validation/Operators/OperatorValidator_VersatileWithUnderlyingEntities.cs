using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Validation.Curves;
using JJ.Business.Synthesizer.Validation.Samples;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_VersatileWithUnderlyingEntities : ValidatorBase
    {
        /// <summary>
        /// Validates an operator, but not its descendant operators.
        /// Does validate underlying curves and samples.
        /// Makes sure that objects are only validated once to 
        /// prevent excessive validation messages.
        /// The reason that underlying entities such as samples and curves are validated here,
        /// is because even though it already happens when you validate a whole document,
        /// in some cases you do not validate the whole document, but a narrower scope,
        /// such as a patch.
        /// </summary>
        public OperatorValidator_VersatileWithUnderlyingEntities(
            [NotNull] Operator op,
            [NotNull] ICurveRepository curveRepository,
            [NotNull] ISampleRepository sampleRepository,
            [NotNull] HashSet<object> alreadyDone)
        {
            if (op == null) throw new NullException(() => op);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (alreadyDone == null) throw new AlreadyDoneIsNullException();

            if (alreadyDone.Contains(op))
            {
                return;
            }
            alreadyDone.Add(op);

            ExecuteValidator(new OperatorValidator_Versatile(op));

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.Curve)
            {
                int curveID;
                if (int.TryParse(op.Data, out curveID))
                {
                    Curve curve = curveRepository.TryGet(curveID);
                    if (curve != null)
                    {
                        if (!alreadyDone.Contains(curve))
                        {
                            alreadyDone.Add(curve);

                            string curveMessagePrefix = ValidationHelper.GetMessagePrefix(curve);

                            ExecuteValidator(new CurveValidator_WithoutNodes(curve), curveMessagePrefix);
                            ExecuteValidator(new CurveValidator_Nodes(curve), curveMessagePrefix);
                        }
                    }
                }
            }

            if (operatorTypeEnum == OperatorTypeEnum.Sample)
            {
                int sampleID;
                if (int.TryParse(op.Data, out sampleID))
                {
                    Sample sample = sampleRepository.TryGet(sampleID);
                    if (sample != null)
                    {
                        if (!alreadyDone.Contains(sample))
                        {
                            alreadyDone.Add(sample);
                            ExecuteValidator(new SampleValidator(sample), ValidationHelper.GetMessagePrefix(sample));
                        }
                    }
                }
            }
        }
    }
}
