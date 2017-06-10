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
        private static readonly int? _nameMaxLength = CustomConfigurationManager.GetSection<ConfigurationSection>().NameMaxLength;

        public NameValidator(string name)
            : this(name, DEFAULT_REQUIRED)
        { }

        public NameValidator(string name, bool required)
            : this(name, _defaultPropertyDisplayName, required)
        { }

        public NameValidator(string name, string propertyDisplayName)
            : this(name, propertyDisplayName, DEFAULT_REQUIRED)
        { }

        public NameValidator(string name, string propertyDisplayName, bool required)
            : base(name)
        {
            if (required)
            {
                For(() => name, propertyDisplayName).NotNullOrWhiteSpace();
            }

            if (_nameMaxLength.HasValue)
            {
                For(() => name, propertyDisplayName).MaxLength(_nameMaxLength.Value);
            }
        }
    }
}
