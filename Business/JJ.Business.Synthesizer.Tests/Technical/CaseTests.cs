using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.ConfigTests;
using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class CaseTests
    {
        [TestMethod]
        public void Test_Case_Key()
        { 
            var    testCase = new FrameCountWishesTests.Case();
            string key      = testCase.Key;
            Static.Log(key);
            NotNullOrEmpty(() => key);
        }
    }
}
