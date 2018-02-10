using System.Collections.Generic;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
	internal class CurrentPatchPictureButtonConverter
	{
		private const float BUTTON_SIZE = 24f;

		private readonly Dictionary<int, (Picture, MouseDownGesture)> _dictionary = new Dictionary<int, (Picture, MouseDownGesture)>();

		public (Picture, MouseDownGesture) Convert(int patchID, Element parentElement, object underlyingPicture)
		{
			if (!_dictionary.TryGetValue(patchID, out (Picture, MouseDownGesture) tuple))
			{
				var picture = new Picture
				{
					Diagram = parentElement.Diagram,
					Parent = parentElement,
					UnderlyingPicture = underlyingPicture
				};
				picture.Position.Width = BUTTON_SIZE;
				picture.Position.Height = BUTTON_SIZE;

				var mouseDownGesture = new MouseDownGesture();
				picture.Gestures.Add(mouseDownGesture);

				_dictionary[patchID] = (picture, mouseDownGesture);
			}

			return tuple;
		}

		public void TryRemove(int patchID)
		{
			if (_dictionary.TryGetValue(patchID, out (Picture, MouseDownGesture) tuple))
			{
				(Picture picture, _) = tuple;

				picture.Children.Clear();
				picture.Parent = null;
				picture.Diagram = null;

				_dictionary.Remove(patchID);
			}
		}
	}
}
