### ConfigResolverAccessor

```cs
    private static readonly AccessorCore _staticAccessor = new (GetUnderlyingType());
    _accessor = new AccessorCore(obj, GetUnderlyingType());

    private static Type GetUnderlyingType()
    {
        Assembly assembly = typeof(SynthWishes).Assembly;
        string   typeName = "JJ.Business.Synthesizer.Wishes.Config.ConfigResolver";
        Type     type     = assembly.GetType(typeName, true);
        return   type;
    }
```

### ConfigSectionAccessor

```cs
            //Type type = GetUnderlyingType();
            //Obj       = Activator.CreateInstance(type);
            //_accessor = new AccessorCore(Obj);
            // _accessor = new AccessorCore("ConfigSection");

        //private Type GetUnderlyingType()
        //{
        //    Assembly assembly = typeof(SynthWishes).Assembly;
        //    string   typeName = "JJ.Business.Synthesizer.Wishes.Config.ConfigSection";
        //    return   assembly.GetType(typeName, true);
        //}
 
```