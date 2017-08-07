using System;

namespace JJ.Business.Synthesizer.Dto
{
    internal sealed class InputDto
    {
        public InputDto(double @const) => Const = @const;

        public InputDto(IOperatorDto var)
        {
            Var = var ?? throw new ArgumentNullException(nameof(var));
        }

        public double? Const { get; }
        public IOperatorDto Var { get; }
    }
}
