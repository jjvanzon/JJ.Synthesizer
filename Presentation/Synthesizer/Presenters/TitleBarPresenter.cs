using System.Text;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class TitleBarPresenter
    {
        private static readonly string _titleBarExtraText = GetTitleBarExtraText();

        public string Show(Document openDocument = null)
        {
            var sb = new StringBuilder();

            if (openDocument != null)
            {
                sb.AppendFormat("{0} - ", openDocument.Name);
            }

            sb.Append(Titles.ApplicationName);

            sb.Append(_titleBarExtraText);

            return sb.ToString();
        }

        private static string GetTitleBarExtraText()
        {
            return CustomConfigurationManager.GetSection<ConfigurationSection>().TitleBarExtraText;
        }
    }
}
