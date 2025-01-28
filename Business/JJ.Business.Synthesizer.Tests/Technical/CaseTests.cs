using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class CaseTests
    {
        [TestMethod]
        public void Test_Case_Key()
        { 
            var testCase = new Case();
            string key = testCase.Key;
            LogWishes.Log(key);
            NotNullOrEmpty(() => key);
        }
    }
}
