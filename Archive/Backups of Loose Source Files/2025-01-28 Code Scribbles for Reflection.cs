Reflection Code Scribbles

        /// <summary>
        /// Gets a list of this interface or its deeper interfaces recursively.
        /// </summary>
        public static IList<Type> GetInterfacesRecursive(Type type)
        {
            if (type == null) throw new NullException(() => type);
            
            var list = new List<Type>();
            
            if (type.IsInterface) list.Add(type);
            
            var interfaces = type.GetInterfaces();
            
            foreach (var @interface in interfaces)
            {
                list.Add(@interface
            }
            
            
            //foreach (Type @interface in type.GetInterfaces())
            //{
            //    var deeperInterfaces = GetInterfacesRecursive(@interface);
            //    list.AddRange(deeperInterfaces);
            //}
            
            return list;
        }

        /// <summary>
        /// If its a class, gets a list with this class and its base classes recursively.
        /// </summary>
        public static IList<Type> GetClassesRecursive(Type type)
        {
            if (type == null) throw new NullException(() => type);
            
            var list = new List<Type>();
            
            if (!type.IsClass)
            {
                return list;
            }
            
            while (type != null)
            {
                list.Add(type);
                type = type.BaseType;
            }

            return list;
        }

        
        //[TestMethod]
        //public void GetFieldOrExceptionTest()
        //{
        //    Test(x => GetType().GetFieldOrException(x));
        //    Test(x => GetFieldOrException(GetType(), x));

        //    void Test(Func<string, FieldInfo> synonym)
        //    {
        //        FieldInfo field = synonym("_field");
                
        //        IsNotNull(() => field);
        //        AreEqual("_field",       () => field.Name);
        //        AreEqual("value",        () => field.GetValue(this));
        //        AreEqual(typeof(string), () => field.FieldType);

        //        ThrowsException(
        //            () => synonym("❌"), 
        //            $"Field '❌' not found on type '{GetType().FullName}'.");
        //    }
        //}


        [TestMethod]
        public void AddTypesRecursiveTest()
        {
            var types = new HashSet<Type>();
            AddTypesRecursive(typeof(Exception), types);

            IsTrue(() => types.Contains(typeof(Exception)));
            IsTrue(() => types.Contains(typeof(object)));
            IsTrue(() => types.Contains(typeof(ISerializable)));
            IsFalse(() => types.Contains(typeof(ReflectionWishes)));
        }

        [TestMethod]
        public void GetClassesRecursiveTest()
        {
            var types = GetClassesRecursive(typeof(Exception));
            IsTrue (() => types.Contains(typeof(Exception)));
            IsTrue (() => types.Contains(typeof(object)));           // Base class
            IsFalse(() => types.Contains(typeof(ISerializable)));    // Not a class
            IsFalse(() => types.Contains(typeof(ReflectionWishes))); // Not part of Exception's list of base classes
        }

        [TestMethod]
        public void AddClassesRecursiveTest()
        {
            var types = new HashSet<Type>();
            AddClassesRecursive(typeof(Exception), types);
            IsTrue(() => types.Contains(typeof(Exception)));
            IsTrue(() => types.Contains(typeof(object)));
            IsFalse(() => types.Contains(typeof(ISerializable)));    // Not a class
            IsFalse(() => types.Contains(typeof(ReflectionWishes))); // Not part of Exception's list of base classes
        }

        [TestMethod]
        public void AddInterfacesRecursiveTest()
        {
            var types = new HashSet<Type>();
            AddInterfacesRecursive(typeof(Exception), types);
            IsTrue(() => types.Contains(typeof(ISerializable)));
            IsFalse(() => types.Contains(typeof(Exception))); // Not an interface
            IsFalse(() => types.Contains(typeof(object))); // Not an interface
        }

        IsFalse(() => HasInterfaceRecursive(typeof(Exception), typeof(IEnumerable))); 
