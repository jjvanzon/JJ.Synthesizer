using System.Collections.Generic;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
	internal class CurrentPatchPictureButtonConverter
	{
		private readonly Dictionary<int, (Picture, MouseDownGesture)> _dictionary = new Dictionary<int, (Picture, MouseDownGesture)>();

		public (Picture, MouseDownGesture) Convert(int patchID, Element parentElement, object underlyingPicture)
		{
			if (!_dictionary.TryGetValue(patchID, out (Picture, MouseDownGesture) tuple))
			{
				tuple = CreatePicture(parentElement, underlyingPicture);

				_dictionary[patchID] = tuple;
			}

			return tuple;
		}

		private (Picture picture, MouseDownGesture mouseDownGesture) CreatePicture(Element parentElement, object underlyingPicture)
		{
			var picture = new Picture(parentElement)
			{
				UnderlyingPicture = underlyingPicture
			};
			picture.Position.Width = StyleHelper.ICON_SIZE;
			picture.Position.Height = StyleHelper.ICON_SIZE;

			var mouseDownGesture = new MouseDownGesture();
			picture.Gestures.Add(mouseDownGesture);

			return (picture, mouseDownGesture);
		}

		public void TryRemove(int patchID)
		{
			if (_dictionary.TryGetValue(patchID, out (Picture, MouseDownGesture) tuple))
			{
				(Picture picture, _) = tuple;

				picture.Dispose();

				_dictionary.Remove(patchID);
			}
		}
	}
}
