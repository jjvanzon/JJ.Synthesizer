using System.Text;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Configuration;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters.Partials
{
	internal class TitleBarPresenter
	{
		private static readonly string _titleBarExtraText = GetTitleBarExtraText();

		public string Show(Document openDocument = null)
		{
			var sb = new StringBuilder();

			if (openDocument != null)
			{
				sb.Append($"{openDocument.Name} - ");
			}

			sb.Append(ResourceFormatter.ApplicationName);

			sb.Append(_titleBarExtraText);

			return sb.ToString();
		}

		private static string GetTitleBarExtraText() => CustomConfigurationManager.GetSection<ConfigurationSection>().TitleBarExtraText;
	}
}
