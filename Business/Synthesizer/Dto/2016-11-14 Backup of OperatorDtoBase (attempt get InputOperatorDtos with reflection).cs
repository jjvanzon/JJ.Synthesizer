//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Reflection;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Framework.Common;
//using JJ.Framework.Reflection;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Dto
//{
//    [DebuggerDisplay("{DebuggerDisplay}")]
//    internal abstract class OperatorDtoBase
//    {
//        // TODO: Put caching and converting to InputOperatorDtos in an separate object and use it in Visitors.
//        private static object _inputOperatorDtoPropertiesDictionaryLock = new object();
//        private static Dictionary<Type, IList<PropertyInfo>> _inputOperatorDtoPropertiesDictionary = new Dictionary<Type, IList<PropertyInfo>>();

//        public int DimensionStackLevel { get; set; }

//        public abstract string OperatorTypeName { get; }

//        /// <summary> Automatically filled in by base class. If that does not suffice, pass it to the constructor or override. </summary>
//        public virtual IList<OperatorDtoBase> InputOperatorDtos { get; }

//        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);

//        public OperatorDtoBase(IList<OperatorDtoBase> inputOperatorDtos)
//        {
//            InputOperatorDtos = inputOperatorDtos;
//        }

//        public OperatorDtoBase()
//        {
//            IList<PropertyInfo> inputOperatorProperties = GetInputOperatorProperties();

//            int count = inputOperatorProperties.Count;

//            InputOperatorDtos = new OperatorDtoBase[count];

//            for (int i = 0; i < count; i++)
//            {
//                PropertyInfo inputOperatorProperty = inputOperatorProperties[i];
//                OperatorDtoBase inputOperatorDto = (OperatorDtoBase)inputOperatorProperty.GetValue(this);
//                InputOperatorDtos[i] = inputOperatorDto;
//            }
//        }

//        private static IList<PropertyInfo> GetInputOperatorProperties()
//        {
//            Type concreteType = GetType();

//            lock (_inputOperatorDtoPropertiesDictionaryLock)
//            {
//                IList<PropertyInfo> inputOperatorDtoProperties;
//                if (!_inputOperatorDtoPropertiesDictionary.TryGetValue(concreteType, out inputOperatorDtoProperties))
//                {
//                    inputOperatorDtoProperties = new List<PropertyInfo>();
//                    IList<Type> concreteTypesAndBaseTypes = ReflectionHelper.GetTypeAndBaseClasses(concreteType);

//                    foreach (Type concreteTypeAndBaseType in concreteTypesAndBaseTypes)
//                    {
//                        IList<PropertyInfo> inputOperatorDtoProperties2 = concreteType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
//                                                                                      .Where(x => x.PropertyType == typeof(OperatorDtoBase))
//                                                                                      .ToArray();
//                        inputOperatorDtoProperties.AddRange(inputOperatorDtoProperties2);
//                    }

//                    _inputOperatorDtoPropertiesDictionary.Add(concreteType, inputOperatorDtoProperties);
//                }

//                return inputOperatorDtoProperties;
//            }
//        }
//    }
//}
