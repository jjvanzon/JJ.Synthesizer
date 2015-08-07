using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary>
    /// Validates the configuration of names, inlets and outlets.
    /// </summary>
    public abstract class OperatorValidator_Base : FluentValidator<Operator>
    {
        private OperatorTypeEnum _expectedOperatorTypeEnum;
        private IList<string> _expectedInletNames;
        private IList<string> _expectedOutletNames;

        public OperatorValidator_Base(
            Operator obj, 
            OperatorTypeEnum expectedOperatorTypeEnum, 
            int expectedInletCount, 
            params string[] expectedInletAndOutletNames)
            : base(obj, postponeExecute: true)
        {
            if (expectedInletAndOutletNames == null) throw new NullException(() => expectedInletAndOutletNames);
            if (expectedInletCount < 0) throw new LessThanException(() => expectedInletCount, 0);
            if (expectedInletAndOutletNames.Length < expectedInletCount) throw new Exception("expectedInletAndOutletNames must have a length of at least the expectedInletCount.");

            _expectedOperatorTypeEnum = expectedOperatorTypeEnum;
            _expectedInletNames = expectedInletAndOutletNames.Take(expectedInletCount).ToArray();
            _expectedOutletNames = expectedInletAndOutletNames.Skip(expectedInletCount).ToArray();

            Execute();
        }

        protected override void Execute()
        {
            Operator op = Object;

            For(() => op.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).Is(_expectedOperatorTypeEnum);

            For(() => op.Inlets.Count, GetPropertyDisplayName_ForInletCount())
                .Is(_expectedInletNames.Count);

            if (op.Inlets.Count == _expectedInletNames.Count)
            {
                for (int i = 0; i < op.Inlets.Count; i++)
                {
                    For(() => op.Inlets[i].Name, GetPropertyDisplayName_ForInletName(i))
                        .Is(_expectedInletNames[i]);
                }
            }

            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount())
                .Is(_expectedOutletNames.Count);

            if (op.Outlets.Count == _expectedOutletNames.Count)
            {
                for (int i = 0; i < op.Outlets.Count; i++)
                {
                    For(() => op.Outlets[i].Name, GetPropertyDisplayName_ForOutletName(i))
                        .Is(_expectedOutletNames[i]);
                }
            }
        }

        private string GetPropertyDisplayName_ForInletCount()
        {
            return CommonTitleFormatter.EntityCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonTitleFormatter.EntityCount(PropertyDisplayNames.Outlets);
        }

        private string GetPropertyDisplayName_ForInletName(int index)
        {
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Inlet, index + 1, CommonTitles.Name);
        }

        private string GetPropertyDisplayName_ForOutletName(int index)
        {
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Outlet, index + 1, CommonTitles.Name);
        }
    }
}
