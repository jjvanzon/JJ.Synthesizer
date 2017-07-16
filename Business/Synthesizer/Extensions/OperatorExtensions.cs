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

            if (Enum.TryParse(op.UnderlyingPatch?.Name, out OperatorTypeEnum operatorTypeEnum))
            {
                return operatorTypeEnum;
            }

            return OperatorTypeEnum.Undefined;
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

        public static bool CanSetInletCount(this Operator op)
        {
            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Add:
                case OperatorTypeEnum.AverageOverInlets:
                case OperatorTypeEnum.ClosestOverInlets:
                case OperatorTypeEnum.ClosestOverInletsExp:
                case OperatorTypeEnum.InletsToDimension:
                case OperatorTypeEnum.MaxOverInlets:
                case OperatorTypeEnum.MinOverInlets:
                case OperatorTypeEnum.Multiply:
                case OperatorTypeEnum.SortOverInlets:
                    return true;

                case OperatorTypeEnum.PatchInlet:
                case OperatorTypeEnum.PatchOutlet:
                    return false;
            }

            bool hasRepeatingInlet = op.Inlets.Reverse().Any(x => x.IsRepeating);
            return hasRepeatingInlet;
        }

        public static bool CanSetOutletCount(this Operator op)
        {
            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.DimensionToOutlets:
                case OperatorTypeEnum.RangeOverOutlets:
                    return true;

                case OperatorTypeEnum.PatchInlet:
                case OperatorTypeEnum.PatchOutlet:
                    return false;
            }

            bool hasRepeatingOutlet = op.Outlets.Reverse().Any(x => x.IsRepeating);
            return hasRepeatingOutlet;
        }
    }
}
