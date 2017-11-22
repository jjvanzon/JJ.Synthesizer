namespace JJ.Presentation.Synthesizer.Helpers
{
	internal static class RefreshIDProvider
	{
		private static int _currentRefreshID;

		public static int GetRefreshID() => _currentRefreshID++;
	}
}
