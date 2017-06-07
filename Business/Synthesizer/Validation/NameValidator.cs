using JJ.Business.Synthesizer.Configuration;
using JJ.Framework.Configuration;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary>
    /// Validates that name does not exceed the common max length (probably 256),
    /// and optionally is it required. You can pass a different property name than 'Name',
    /// if it is about a property that is not called Name.
    /// </summary>
    internal class NameValidator : VersatileValidator_WithoutConstructorArgumentNullCheck<string>
    {
        private const bool DEFAULT_REQUIRED = true;
        private static readonly string _defaultPropertyDisplayName = CommonResourceFormatter.Name;
        private static readonly int? _nameMaxLength = GetNameMaxLength();

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

        protected sealed override void Execute()
        {
            string name = Obj;

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
            return CustomConfigurationManager.GetSection<ConfigurationSection>().NameMaxLength;
        }
    }
}
