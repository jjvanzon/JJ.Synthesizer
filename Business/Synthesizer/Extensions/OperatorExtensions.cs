using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class OperatorExtensions
    {
        public static OperatorTypeEnum GetOperatorTypeEnum(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            // Try explicitly stored
            if (op.OperatorType != null)
            {
                return (OperatorTypeEnum)op.OperatorType.ID;
            }

            // Try based on UnderlyingPatch
            if (Enum.TryParse(op.UnderlyingPatch?.Name, out OperatorTypeEnum operatorTypeEnum))
            {
                return operatorTypeEnum;
            }

            return OperatorTypeEnum.Undefined;
        }

        public static void SetOperatorTypeEnum(
            this Operator op,
            OperatorTypeEnum operatorTypeEnum,
            RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Undefined:
                {
                    // Handle Undefined.
                    op.UnlinkOperatorType();
                    return;
                }

                case OperatorTypeEnum.Absolute:
                {
                    // Try use system patch
                    var documentManager = new DocumentManager(repositories);
                    Patch patch = documentManager.GetSystemPatch(operatorTypeEnum);
                    op.UnderlyingPatch = patch;
                    return;
                }

                default:
                {
                    // Use classic OperatorType entity
                    OperatorType operatorType = repositories.OperatorTypeRepository.TryGet((int)operatorTypeEnum);
                    op.LinkTo(operatorType);
                    return;
                }
            }
        }

        public static IList<Operator> GetConnectedOperators(this Operator op)
        {
            if (op == null) throw new NullException(() => op);

            IList<Operator> connectedOperators =
                // ReSharper disable once InvokeAsExtensionMethod
                Enumerable.Union(
                              op.Inlets.Where(x => x.InputOutlet != null).Select(x => x.InputOutlet.Operator),
                              op.Outlets.SelectMany(x => x.ConnectedInlets).Select(x => x.Operator))
                          .ToArray();

            return connectedOperators;
        }
    }
}
