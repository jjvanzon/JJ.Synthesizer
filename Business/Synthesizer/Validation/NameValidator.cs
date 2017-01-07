using JJ.Business.Synthesizer.Configuration;
using JJ.Framework.Common;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class NameValidator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
    {
        private const bool DEFAULT_REQUIRED = true;
        private readonly static string _defaultPropertyDisplayName = CommonTitles.Name;
        private readonly static int? _nameMaxLength = GetNameMaxLength();

        private readonly bool _required;
        private readonly string _propertyDisplayName;

        public NameValidator(string obj)
            : this(obj, DEFAULT_REQUIRED)
        { }

        public NameValidator(string obj, bool required)
            : this(obj, _defaultPropertyDisplayName, required)
        { }

        public NameValidator(string obj, string propertyDisplayName)
            : this(obj, propertyDisplayName, DEFAULT_REQUIRED)
        { }

        public NameValidator(string obj, string propertyDisplayName, bool required)
            : base(obj, postponeExecute: true)
        {
            _propertyDisplayName = propertyDisplayName;
            _required = required;

            Execute();
        }

        protected override void Execute()
        {
            string name = Object;

            if (_required)
            {
                For(() => name, _propertyDisplayName)
                    .NotNullOrWhiteSpace();
            }

            if (_nameMaxLength.HasValue)
            {
                For(() => name, _propertyDisplayName)
                    .MaxLength(_nameMaxLength.Value);
            }
        }

        private static int? GetNameMaxLength()
        {
            return ConfigurationHelper.GetSection<ConfigurationSection>().NameMaxLength;
        }
    }
}
