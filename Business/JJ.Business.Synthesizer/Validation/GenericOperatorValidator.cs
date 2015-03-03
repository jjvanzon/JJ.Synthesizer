using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class GenericOperatorValidator : FluentValidator<Operator>
    {
        private string _expectedOperatorTypeName;
        private IList<string> _expectedInletNames;
        private IList<string> _expectedOutletNames;

        public GenericOperatorValidator(
            Operator obj, 
            string expectedOperatorTypeName, 
            int expectedInletCount, 
            params string[] expectedInletAndOutletNames)
            : base(obj, postponeExecute: true)
        {
            if (String.IsNullOrWhiteSpace(expectedOperatorTypeName)) throw new Exception("expectedOperatorTypeName cannot be null or white space.");
            if (expectedInletAndOutletNames == null) throw new NullException(() => expectedInletAndOutletNames);
            if (expectedInletCount < 0) throw new Exception("expectedInletCount cannot be less than 0.");
            if (expectedInletAndOutletNames.Length < expectedInletCount) throw new Exception("expectedInletAndOutletNames must have a length of at least the expectedInletCount.");

            _expectedOperatorTypeName = expectedOperatorTypeName;
            _expectedInletNames = expectedInletAndOutletNames.Take(expectedInletCount).ToArray();
            _expectedOutletNames = expectedInletAndOutletNames.Skip(expectedInletCount).ToArray();

            Execute();
        }

        protected override void Execute()
        {
            if (Object == null) throw new NullException(() => Object);

            For(() => Object.OperatorTypeName, PropertyDisplayNames.OperatorTypeName)
                .IsValue(_expectedOperatorTypeName);

            For(() => Object.Inlets.Count, GetPropertyDisplayName_ForInletCount())
                .IsValue(_expectedInletNames.Count);

            if (Object.Inlets.Count == _expectedInletNames.Count)
            {
                for (int i = 0; i < Object.Inlets.Count; i++)
                {
                    // TODO: ExpressionToValueVisitor chokes on:
                    // () => Object.Inlets[i].Name
                    var inlet = Object.Inlets[i];
                    For(() => inlet.Name, GetPropertyDisplayName_ForInlet(0))
                        .IsValue(_expectedInletNames[i]);
                }
            }

            For(() => Object.Outlets.Count, GetPropertyDisplayName_ForOutletCount())
                .IsValue(_expectedOutletNames.Count);

            if (Object.Outlets.Count == _expectedOutletNames.Count)
            {
                for (int i = 0; i < Object.Outlets.Count; i++)
                {
                    // TODO: ExpressionToValueVisitor chokes on:
                    // () => Object.Outlets[i].Name
                    var outlet = Object.Outlets[i];
                    For(() => outlet.Name, GetPropertyDisplayName_ForOutlet(0))
                        .IsValue(_expectedOutletNames[i]);
                }
            }
        }

        private string GetPropertyDisplayName_ForInletCount()
        {
            return CommonTitlesFormatter.EntityCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonTitlesFormatter.EntityCount(PropertyDisplayNames.Inlets);
        }

        private string GetPropertyDisplayName_ForInlet(int index)
        {
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Inlet, index + 1, PropertyDisplayNames.Name);
        }

        private string GetPropertyDisplayName_ForOutlet(int index)
        {
            return String.Format("{0} {1}: {2}", PropertyDisplayNames.Outlet, index + 1, PropertyDisplayNames.Name);
        }
    }
}
