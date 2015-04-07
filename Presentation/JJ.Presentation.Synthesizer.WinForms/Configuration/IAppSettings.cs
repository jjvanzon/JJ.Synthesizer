using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.Configuration
{
    internal interface IAppSettings
    {
        bool MustCreateMockPatch { get; set; }
        int TestPatchID { get; set; }
        bool MustShowInvisibleElements { get; set; }
    }
}
