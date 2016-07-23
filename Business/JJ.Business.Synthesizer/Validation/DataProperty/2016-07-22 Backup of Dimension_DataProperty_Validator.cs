//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.DataProperty
//{
//    internal class Dimension_DataProperty_Validator : FluentValidator_WithoutConstructorArgumentNullCheck<string>
//    {
//        public Dimension_DataProperty_Validator(string obj) 
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            string data = Object;

//            if (DataPropertyParser.DataIsWellFormed(data))
//            {
//                // Dimension can be Undefined, but key must exist.
//                string dimensionString = DataPropertyParser.TryGetString(data, PropertyNames.Dimension);

//                For(() => dimensionString, PropertyNames.Dimension)
//                    .NotNullOrEmpty()
//                    .IsEnum<DimensionEnum>();
//            }
//        }
//    }
//}
