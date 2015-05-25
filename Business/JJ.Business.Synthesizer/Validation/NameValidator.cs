using JJ.Business.Synthesizer.Configuration;
using JJ.Framework.Common;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    public class NameValidator : FluentValidator<string>
    {
        private static int? _nameMaxLength;

        static NameValidator()
        {
            var config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _nameMaxLength = config.NameMaxLength;
        }

        public NameValidator(string obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            string name = Object;

            For(() => name, CommonTitles.Name)
                .NotNullOrWhiteSpace()
                .NotInteger();

            if (_nameMaxLength.HasValue)
            {
                For(() => name, CommonTitles.Name)
                    .MaxLength(_nameMaxLength.Value);
            }
        }
    }
}
