using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal interface ICase : ICaseProp
    {
        string             Name        { get; set; }
        string             Descriptor  { get; }
        object[]           DynamicData { get; }
        bool               Strict      { get; set; }
        IList<ICaseProp>   Props       { get; }
        /// <inheritdoc cref="docs._casetemplate" />
        ICollection<ICase> FromTemplate(ICollection<ICase> cases);
    }
}