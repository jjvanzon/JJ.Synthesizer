Trying to get Case Descriptors right:


    case string whitespace when !mustAddNextUnit && IsNullOrWhiteSpace(whitespace):
        // Trick to leave out next unit: add white
        mustAddNextUnit = false;
        break;
    

    protected override IList<string> PropertyPrefixes() => new [] { "", "", "", "+" };
    protected override IList<string> PropertySuffixes() => new [] { "f", "Hz", "s" };
    protected override IList<ICaseProp> Properties() => new ICaseProp[] { FrameCount, "f", SamplingRate, "Hz", AudioLength, "s", "+", CourtesyFrames };
    protected override IList<ICaseProp> Properties() => new ICaseProp[] { FrameCount, "f", SamplingRate, "Hz", AudioLength, "s", "+", CourtesyFrames };

        
    private string Descriptor10_DoesNotWork
    {
        get
        {
            var textElements = new List<object>();
            
            foreach (object descriptorElement in DescriptorElements)
            {

                switch (descriptorElement)
                {
                    case ValueTuple<string, object, string> triple:
                        // triple is (Item1, Item2, Item3)
                        string prefix = triple.Item1;
                        object prop   = triple.Item2;
                        string suffix = triple.Item3;

                        string propText = $"{prop}";
                        if (Has(propText))
                        {
                            textElements.Add(prefix);
                            textElements.Add(propText);
                            textElements.Add(suffix);
                        }
                        break;
                        
                    default:
                        textElements.Add(descriptorElement);
                        break;
                }
            }
            
            return Join(" ", textElements.Where(FilledIn));
        }
    }
        
    private string Descriptor8_DoesNotWork
    {
        get
        {
            var textElements = new List<string>();
            
            foreach (object descriptorElement in DescriptorElements)
            {
                if (descriptorElement is ValueTuple<string, object, string> tuple)
                {
                    string prefix = tuple.Item1;
                    object prop = tuple.Item2;
                    string suffix = tuple.Item3;

                    string propText = $"{prop}";
                    if (Has(propText))
                    {
                        textElements.Add(prefix);
                        textElements.Add(propText);
                        textElements.Add(suffix);
                    }
                }
                else
                {
                    textElements.Add($"{descriptorElement}");
                }
            }
            
            return Join(" ", textElements.Where(FilledIn));
        }
    }

    //private string Descriptor7_DoesNotCompile
    //{
    //    get
    //    {
    //        var textElements = new List<string>();
            
    //        foreach (object descriptorElement in DescriptorElements)
    //        {
    //            switch (descriptorElement)
    //            {
    //                case string str:
    //                    textElements.Add(str);
    //                    break;
                        
    //                case (string prefix, object prop, string suffix) tuple:
    //                    string propText = $"{prop}";
    //                    if (Has(propText))
    //                    {
    //                        textElements.Add(prefix);
    //                        textElements.Add(propText);
    //                        textElements.Add(suffix);
    //                    }
                        
    //                    break;
    //            }
    //        }
            
    //        return Join(" ", textElements.Where(FilledIn));
    //    }
    //}

    private string Descriptor5_DoesNotWork_DifficultCode
    {
        get
        {
            var elements = new List<string>();
            
            bool lastWasProp = false;
            bool lastWasUnit = false;
            
            foreach (object descriptorElement in DescriptorElements)
            {
                switch (descriptorElement)
                {
                    case null:
                        break;
                        
                    case string suffix when lastWasProp && suffix.Length <= 3:
                        // Only add unit if previous prop was filled in.
                        elements.Add(suffix);
                        lastWasUnit = true;
                        lastWasProp = false;
                        break;
                                                
                    case string prefix when lastWasUnit && prefix.Length <= 3:
                        elements.Add(prefix);
                        lastWasUnit = true;
                        lastWasProp = false;
                        break;

                    case string unusedUnit when unusedUnit.Length <= 3:
                        lastWasUnit = true;
                        lastWasProp = false;
                        break;
                        
                    case object prop:
                        string propText = $"{prop}";
                        if (Has(propText))
                        {
                            elements.Add(propText);
                            lastWasProp = true;
                            lastWasUnit = false;
                        }
                        break;
                }
            }
            
            return Join(" ", elements.Where(FilledIn));
        }
    }
    
    private string Descriptor4_DoesNotWork_DifficultCode
    {
        get
        {
            var elements = new List<string>();
            string lastSep = null;
            string lastPropElement = null;

            foreach (object descriptorElement in DescriptorElements)
            {
                switch (descriptorElement)
                {
                    case string sep:
                        if(Has(sep))
                        {
                            // Only add text if previous prop was filled in.
                            if (!Has(lastPropElement))
                            {
                                elements.Add(sep);
                            }
                        }
                        lastSep = sep;
                        break;
                        
                    case object prop:
                        string propText = $"{prop}";
                        if (Has(propText))
                        {
                            elements.Add(propText);
                        }
                        lastPropElement = propText;
                        break;
                }
            }
            
            return Join(" ", elements);
        }
    }
    
    private string Descriptor3_UglyResult
    {
        get
        {
            var textElements = new List<string>();
            
            foreach (object descriptorElement in DescriptorElements)
            {
                string textElement = $"{descriptorElement}";
                if (Has(textElement))
                {
                    textElements.Add(textElement);
                }
                else
                {
                    textElements.Add("_");
                }
            }
            
            return Join(" ", textElements);
        }
    }

    protected override string[] Units() => new [] { "f", "Hz", "s", "+" };

    .SelectMany(x => new object[] {x, "," }).ToArray();

    sourceList = new[] { Name, "~" }.Union(Properties.Cast<object>())
                                    .SelectMany(x => new[] { x, "," })
                                    .Take(Properties.Length * 2 - 1)
                                    .ToArray();


    protected object[] DescriptorElements => new object[] { Name, "~", PropDescriptor, "f", "(", SamplingRate, "Hz", "+", CourtesyFrames, "", ",", AudioLength, "s", ")" };
    protected object[] DescriptorElements => new object[] { ("", Name, "~"), ("", PropDescriptor, "f"), "(", ("", SamplingRate, "Hz"), ("+", CourtesyFrames, ""), (",", AudioLength, "s"), ")" };


        
        private string Descriptor_RawDescriptorElements => Join(" ", DescriptorElements);
        
        private string Descriptor_RawDescriptorElements_WithTypeStrings => Join(" ", DescriptorElements.Select(x => $"{x}{{{x?.GetType().FullName}}}"));

        private string Descriptor_Custom
        {
            get 
            {
                // Name
                string name = GetElementDescriptor(Name, "~");
                
                // Main Prop
                string frameCount = GetElementDescriptor(base.PropDescriptor, "f");
                
                // Other Props
                string samplingRate = GetElementDescriptor(SamplingRate, "Hz");
                string plusFrames = GetElementDescriptor("+", PlusFrames);
                string audioLength = GetElementDescriptor(",", AudioLength, "s");
                string braced = Join(" ", samplingRate, plusFrames, audioLength);
                braced = Has(braced) ? $"({braced.TrimStart(',').Trim()})" : "";

                // Return
                return Join(" ", name, frameCount, braced); 
            }
        }




        private string Descriptor_LoneUnitOmission
        {
            get
            {
                var elements = new List<string>();
                
                bool mustAddNextUnit = false;
                
                foreach (object descriptorElement in DescriptorElements)
                {
                    switch (descriptorElement)
                    {
                        case ICaseProp prop:
                            string propText = $"{prop}";
                            if (Has(propText))
                            {
                                elements.Add(propText);
                                mustAddNextUnit = true;
                            }
                            else
                            {
                                mustAddNextUnit = false;
                            }
                            break;
                            
                        case string unit when unit.Length <= 3:
                            if (mustAddNextUnit)
                            {
                                elements.Add(unit);
                            }
                            mustAddNextUnit = true;
                            break;
                            
                        default:
                           elements.Add($"{descriptorElement}");
                           break;
                    }
                }
                
                return Join(" ", elements.Where(FilledIn));
            }
        }

        private string Descriptor_FromTuples
        {
            get
            {
                var textElements = new List<string>();
                
                foreach (object descriptorElement in DescriptorElements)
                {
                    Type type = descriptorElement.GetType();
                    
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValueTuple<,,>))
                    {
                        string item1 = type.GetField("Item1")?.GetValue(descriptorElement).ToString();
                        string item2 = type.GetField("Item2")?.GetValue(descriptorElement).ToString();
                        string item3 = type.GetField("Item3")?.GetValue(descriptorElement).ToString();
                        if (Has(item2))
                        {
                            textElements.Add(item1);
                            textElements.Add(item2);
                            textElements.Add(item3);
                        }
                    }
                    else
                    {
                        textElements.Add($"{descriptorElement}");
                    }
                } 
                
                return Join(" ", textElements.Where(FilledIn));
            }
        }

        
        private bool UseUnitFromString(object item, out string unit, ref bool mustAddUnit)
        {
            unit = item as string;
            
            if (!Has(unit))
            {
                return false;
            }
            
            if (unit.Length > 3)
            {
                return false;
            }

            if (mustAddUnit)
            {
                return true;
            }
            else
            {
                mustAddUnit = false; // Skip at most 1 unit.
                return false;
            }
        }
                
        private string UnitFromStringOld(object item, ref bool mustAddUnit)
        {
            if (item is string unit && unit.Length <= 3)
            {
                if (mustAddUnit)
                {
                    return unit;
                }
                mustAddUnit = false;
            }
            
            return default;
        }

        
        private bool UseCasePropText(object item, out string text, ref bool mustAddUnit)
        {
            text = "";
            
            var prop = item as ICaseProp;
            if (prop.IsNully())
            {
                return false;
            }
            
            string propText = prop.PropDescriptor;
            if (propText.IsNully())
            {
                return false;
            }
         
            text = propText;
            mustAddUnit = true;
            return true;
        }

        
        private bool UseTextsFromTuple(object item, out IList<string> texts)
        {
            texts = new List<string>();
            
            if (item == null)
            {
                return false;
            }
            
            Type type = item.GetType();

            if (!type.IsGenericType)
            {
                return false;
            }
            
            if (type.GetGenericTypeDefinition() != typeof(ValueTuple<,,>))
            {
                return false;
            }
            
            string prefix = type.GetField("Item1").GetValue(item).ToString();
            string value  = type.GetField("Item2").GetValue(item).ToString();
            string suffix = type.GetField("Item3").GetValue(item).ToString();
            
            if (Has(value))
            {
                texts.Add(prefix);
                texts.Add(value);
                texts.Add(suffix);
            }

            return true;
        }

        
        private string DescriptorWorks
        {
            get
            {
                object[] descriptorElements = DescriptorElements;
                if (!Has(descriptorElements))
                {
                    return DescriptorFromProperties;
                }
                
                var destList = new List<object>();
                bool mustAddNextUnit = false;
                
                foreach (object item in descriptorElements)
                {
                    Type type = item.GetType();

                    if (item is ICaseProp prop)
                    {
                        var propText = $"{prop.PropDescriptor}";
                        if (Has(propText))
                        {
                            destList.Add(propText);
                        }
                        mustAddNextUnit = Has(propText);
                    }
                    else if (item is string unit && unit.Length <= 3)
                    {
                        if (mustAddNextUnit) 
                        { 
                            destList.Add(unit);
                        }
                        else
                        {
                            mustAddNextUnit = true;
                        }
                    }
                    else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValueTuple<,,>))
                    {
                        string prefix = type.GetField("Item1").GetValue(item).ToString();
                        string value  = type.GetField("Item2").GetValue(item).ToString();
                        string suffix = type.GetField("Item3").GetValue(item).ToString();
                        
                        if (Has(value))
                        {
                            destList.Add(prefix);
                            destList.Add(value);
                            destList.Add(suffix);
                        }
                    }
                    else
                    {
                        destList.Add(item);
                    }
                }
                
                string descriptor = Join(" ", destList.Select(x => x.ToString()).Where(FilledIn));
                return descriptor;
            }
        }

        private readonly ICaseProp        _mainProp;
        private readonly IList<ICaseProp> _properties;
        private readonly IList<object>    _descriptorElements;
        
        public CaseDescriptorBuilder(ICaseProp mainProp, IList<ICaseProp> props, IList<object> descriptorElements)
        {
            if (props == null) throw new NullException(() => props);
            if (props.Contains(null)) throw new Exception($"{nameof(props)} collection contains nulls."):
        
            _mainProp           = mainProp ?? throw new NullException(() => mainProp);
            _properties         = props;
            _descriptorElements = descriptorElements ?? throw new NullException(() => descriptorElements);
        }
