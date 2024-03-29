﻿using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

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
            Operator op,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            HashSet<object> alreadyDone)
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

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Curve:
                    Curve curve = op.Curve;
                    if (curve != null)
                    {
                        if (alreadyDone.Contains(curve))
                        {
                            return;
                        }
                        alreadyDone.Add(curve);

                        string curveMessagePrefix = ValidationHelper.GetMessagePrefix(curve);

                        ExecuteValidator(new CurveValidator_WithoutNodes(curve), curveMessagePrefix);
                        ExecuteValidator(new CurveValidator_Nodes(curve), curveMessagePrefix);
                    }
                    break;

                case OperatorTypeEnum.Sample:
                    Sample sample = op.Sample;

                    if (sample != null)
                    {
                        if (alreadyDone.Contains(sample))
                        {
                            return;
                        }
                        alreadyDone.Add(sample);

                        byte[] bytes = sampleRepository.TryGetBytes(sample.ID);
                        ExecuteValidator(new SampleValidator(sample, bytes), ValidationHelper.GetMessagePrefix(sample));
                    }
                    break;
            }
        }
    }
}
