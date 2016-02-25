using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class TitleBarPresenter
    {
        public string Show(Document openDocument = null)
        {
            var sb = new StringBuilder();

            if (openDocument != null)
            {
                sb.AppendFormat("{0} - ", openDocument.Name);
            }

            sb.Append(Titles.ApplicationName);

            return sb.ToString();
        }
    }
}
