Case code scribbles

// Before inheriting Case from CaseProp<int> again for its main property.

    // Quasi-Inherited Properties

    ///// <inheritdoc cref="docs._from" />
    //public NullyPair<int> From { get => FrameCount.From; set => FrameCount.From = value; } 
    ///// <inheritdoc cref="docs._from" />
    //public NullyPair<int> Init { get => FrameCount.Init; set => FrameCount.Init = value; }
    ///// <inheritdoc cref="docs._from" />
    //public NullyPair<int> Source { get => FrameCount.Source; set => FrameCount.Source = value; }
    ///// <inheritdoc cref="docs._to" />
    //public NullyPair<int> To { get => FrameCount.To; set => FrameCount.To = value; }
    ///// <inheritdoc cref="docs._to" />
    //public NullyPair<int> Value { get => FrameCount.Value; set => FrameCount.Value = value; }
    ///// <inheritdoc cref="docs._to" />
    //public NullyPair<int> Val { get => FrameCount.Val; set => FrameCount.Val = value; }
    ///// <inheritdoc cref="docs._to" />
    //public NullyPair<int> Dest  { get => FrameCount.Dest; set => FrameCount.Dest = value; }
    //public int? Nully { get => FrameCount.Nully; set => FrameCount.Nully = value; }
    //public int Coalesced { get => FrameCount.Coalesced; set => FrameCount.Coalesced = value; }

    //public static implicit operator int (Case testCase) => testCase.FrameCount;
    //public static implicit operator int?(Case testCase) => testCase.FrameCount;


// While making Case structure generally reusable:

    //public string Prefix { get; set; }
    //public string Suffix { get; set; }

    // For Interface
    //public T? GetNully() => Nully;
    //public T GetCoalesced() => Coalesced;
    
    public T? GetNully<T>() where T : struct     =>  Nully;
    public T  GetCoalesced<T>() where T : struct => Coalesced;


CaseProp:

    ///// <inheritdoc cref="docs._to" />
    //public NullyPair<T> Val   { get => To; set => To = value; }

        //public void Set(T value) => From = To = value;

CaseBase:

    //while (_props.Count <= index) _props.Add(null); // Auto-size collection
    //_props[index] = _props[index] ?? new CaseProp<T>(); // Get or Create
    //return (CaseProp<T>)_props[index]; // Cast


    // Auto-size dest collection
    //while (destCase._props.Count < _props.Count) destCase._props.Add(null);

    // Get
    //ICaseProp destProp = _props[j]; 
    
    // Or overwrite
    //destProp = destProp ?? (ICaseProp)Activator.CreateInstance(sourceProp.GetType());


        //public Case( int? frameCount) : base(frameCount) { }

        //public Case( int  from ,  int? to) : base(from, to) { }
        //public Case( int? from ,  int  to) : base(from, to) { }
        //public Case( int? from ,  int? to) : base(from, to) { }
        //public Case((int  from ,  int  to) values) : base(values) { }
        //public Case((int? from ,  int  to) values) : base(values) { }
        //public Case((int  from ,  int? to) values) : base(values) { }
        //public Case((int? from ,  int? to) values) : base(values) { }
        //public Case( int  from , (int? nully, int coalesced) to) : base(from, to) { }
        //public Case((int? nully,  int coalesced) from,  int to) : base(from, to) { }
        //public Case((int? nully,  int coalesced) from, (int? nully, int coalesced) to) : base(from, to) { }

        // Conversion Operators

        //public static implicit operator Case(int  value) => new Case(value);
        //public static implicit operator Case(int? value) => new Case(value);
        //public static implicit operator Case((int  from, int  to) values) => new Case(values);
        //public static implicit operator Case((int? from, int  to) values) => new Case(values);
        //public static implicit operator Case((int  from, int? to) values) => new Case(values);
        //public static implicit operator Case((int? from, int? to) values) => new Case(values);
        //public static implicit operator Case((int from, (int? nully, int coalesced) to) x) => new Case(x.from, x.to);
        //public static implicit operator Case(((int? nully, int coalesced) from, int to) x) =>  new Case(x.from, x.to);
        //public static implicit operator Case(((int? nully, int coalesced) from, (int? nully, int coalesced) to) x) => new Case(x.from, x.to);


        //public Case[] CloneTo(params Case[] cases)
        //{
        //    if (cases == null) throw new NullException(() => cases);
        //    for (int i = 0; i < cases.Length; i++)
        //    {
        //        if (cases[i] == null) throw new NullException(() => cases[i]);
        //        var testCase = cases[i];
        //        testCase.Name = Coalesce(testCase.Name, Name);
        //        if (Strict == false) testCase.Strict = false; // Yield over alleviation from strictness.
                
        //        testCase.FrameCount    .CloneFrom(FrameCount    );
        //        testCase.SamplingRate  .CloneFrom(SamplingRate  );
        //        testCase.AudioLength   .CloneFrom(AudioLength   );
        //        testCase.CourtesyFrames.CloneFrom(CourtesyFrames);
        //    }
        //    return cases;
        //}

        //public CaseProp<int> SamplingRate { get; set; } = new CaseProp<int>();

    //[DebuggerDisplay("{DebuggerDisplay}")]
    //string DebuggerDisplay => DebuggerDisplay(this);



        
        //private readonly IList<ICaseProp> _props = new List<ICaseProp>();
        
        //protected ICaseProp GetProp(Type type, int index) 
        //{
        //    if (type == null) throw new NullException(() => type);
        //    while (_props.Count <= index) _props.Add(null); // Auto-size collection
        //    return _props[index] = _props[index] ?? (ICaseProp)Activator.CreateInstance(type); // Get or Create
        //}

        //protected CaseProp<TProp> GetProp<TProp>(int index) where TProp: struct
        //{
        //    return (CaseProp<TProp>)GetProp(typeof(CaseProp<TProp>), index); // Cast
        //}

        //protected void SetProp<T>(int index, CaseProp<T> prop) where T: struct
        //{
        //    if (prop == null) throw new NullException(() => prop);
        //    while (_props.Count <= index) _props.Add(null); // Auto-size collection
        //    _props[index] = prop;
        //}

            
        ...
            //return template.CloneTo(destCases).ToArray();
        //}

        //public CaseBase<TMainProp>[] CloneTo(params CaseBase<TMainProp>[] destCases)
        //{
        ...


        // Lambdas `() =>` improve assertion messages but can complicate debugging.
        // TODO: Remove `() =>` when complex tests ensure clear assertion messages.
        

        //public IEnumerable<object[]> DynamicData => _dictionary.Values.Select(x => x.DynamicData).ToArray();

        
        //static object[][] SimpleCaseData => SimpleCases; 



Case Code Scribbles

                var props = new HashSet<ICaseProp>(); // HasSet to prevent duplicates.
                foreach (PropertyInfo propertyInfo in properties)
                {
                    props.Add((ICaseProp)propertyInfo.GetValue(this));
                }
                return props.ToArray();;

        Dictionary<string, Case> _caseDictionary
            = Empty<Case>().Concat(BasicCases.GetAll())
                           .Concat(AudioLengthCases.GetAll())
                           .Concat(SamplingRateCases.GetAll())
                           .Concat(CourtesyFramesCases.GetAll())
                           .Concat(NullyAudioLengthCases.GetAll())
                           .Concat(NullySamplingRateCases.GetAll())
                           .Concat(NullyCourtesyFramesCases.GetAll())
                           .Concat(NullyFrameCountCases.GetAll())
                           .Concat(InitCases.GetAll())
                           //.Distinct(x => x.Descriptor)
                           .ToDictionary(x => x.Descriptor);

        // Templating

        public static Case[] FromTemplate(Case template, params Case[] cases)
        {
            if (template == null) throw new NullException(() => template);
            return template.FromTemplate(cases).Cast<Case>().ToArray();
        }

// Initialization

            AssertCases(cases);
            _cases.AddRange(cases);

            // TODO: Check duplicate keys and throw exception with descriptor in it.
            cases.ForEach(x => _dictionary.Add(x.Descriptor, x));

        
        private ICollection<TCase> AssertCases(ICollection<TCase> cases)
        {
            if (cases == null) throw new NullException(() => cases);
            if (cases.Contains(default)) throw new Exception($"{nameof(cases)} collection contains empty elements.");
            return cases;
        }

            
            var caseCollection = new CaseCollection<TCase>(cases);
            Add(caseCollection);
            return caseCollection;
