using JJ.Business.Synthesizer.Configuration;
using JJ.Framework.Common;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NameValidator : FluentValidator_WithoutConstructorArgumentNullCheck<string>
    {
        private static int? _nameMaxLength;
        private bool _required;

        static NameValidator()
        {
            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _nameMaxLength = config.NameMaxLength;
        }

        public NameValidator(string obj, bool required = true)
            : base(obj, postponeExecute: true)
        {
            _required = required;

            Execute();
        }

        protected override void Execute()
        {
            string name = Object;

            if (_required)
            {
                For(() => name, CommonTitles.Name)
                    .NotNullOrWhiteSpace();
            }

            if (_nameMaxLength.HasValue)
            {
                For(() => name, CommonTitles.Name)
                    .MaxLength(_nameMaxLength.Value);
            }
        }
    }
}
