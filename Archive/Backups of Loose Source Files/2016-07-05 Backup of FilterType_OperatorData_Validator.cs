﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.DataProperty
//{
//    internal class FilterType_DataProperty_Validator : FluentValidator_WithoutConstructorArgumentNullCheck<string>
//    {
//        public FilterType_DataProperty_Validator(string obj) 
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            string data = Object;

//            if (DataPropertyParser.DataIsWellFormed(data))
//            {
//                string filterTypeString = DataPropertyParser.TryGetString(Object, PropertyNames.FilterType);

//                For(() => filterTypeString, PropertyDisplayNames.FilterType)
//                    .NotNullOrEmpty()
//                    .IsEnum<FilterTypeEnum>()
//                    .IsNot(FilterTypeEnum.Undefined);
//            }
//        }
//    }
//}
