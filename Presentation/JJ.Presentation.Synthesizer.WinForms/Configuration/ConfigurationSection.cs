using JJ.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal class ConfigurationSection
    {
        public TestingConfiguration Testing { get; set; }
        public GeneralConfiguration General { get; set; }
    }
}
