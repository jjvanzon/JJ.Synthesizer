using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
    public class UnderlyingPictureWrapper
    {
        public object PictureAdd { get; }
        public object PictureAddToInstrument { get; }
        public object PictureBrowse { get; }
        public object PictureClose { get; }
        public object PictureDelete { get; }
        public object PictureExpand { get; }
        public object PictureMoveBackward { get; }
        public object PictureMoveForward { get; }
        public object PictureNew { get; }
        public object PicturePlay { get; }
        public object PictureRedo { get; }
        public object PictureRefresh { get; }
        public object PictureRename { get; }
        public object PictureSave { get; }
        public object PictureTreeStructure { get; }
        public object PictureUndo { get; }

        public UnderlyingPictureWrapper(
            object pictureAdd,
            object pictureAddToInstrument,
            object pictureBrowse,
            object pictureClose,
            object pictureDelete,
            object pictureExpand,
            object pictureMoveBackward,
            object pictureMoveForward,
            object pictureNew,
            object picturePlay,
            object pictureRedo,
            object pictureRefresh,
            object pictureRename,
            object pictureSave,
            object pictureTreeStructure,
            object pictureUndo)
        {
            PictureAdd = pictureAdd ?? throw new ArgumentNullException(nameof(pictureAdd));

            PictureAddToInstrument =
                pictureAddToInstrument ?? throw new ArgumentNullException(nameof(pictureAddToInstrument));

            PictureBrowse = pictureBrowse ?? throw new ArgumentNullException(nameof(pictureBrowse));
            PictureClose = pictureClose ?? throw new ArgumentNullException(nameof(pictureClose));
            PictureDelete = pictureDelete ?? throw new ArgumentNullException(nameof(pictureDelete));
            PictureExpand = pictureExpand ?? throw new ArgumentNullException(nameof(pictureExpand));
            PictureMoveBackward = pictureMoveBackward ?? throw new ArgumentNullException(nameof(pictureMoveBackward));
            PictureMoveForward = pictureMoveForward ?? throw new ArgumentNullException(nameof(pictureMoveForward));
            PictureNew = pictureNew ?? throw new ArgumentNullException(nameof(pictureNew));
            PicturePlay = picturePlay ?? throw new ArgumentNullException(nameof(picturePlay));
            PictureRedo = pictureRedo ?? throw new ArgumentNullException(nameof(pictureRedo));
            PictureRefresh = pictureRefresh ?? throw new ArgumentNullException(nameof(pictureRefresh));
            PictureRename = pictureRename ?? throw new ArgumentNullException(nameof(pictureRename));
            PictureSave = pictureSave ?? throw new ArgumentNullException(nameof(pictureSave));

            PictureTreeStructure =
                pictureTreeStructure ?? throw new ArgumentNullException(nameof(pictureTreeStructure));

            PictureUndo = pictureUndo ?? throw new ArgumentNullException(nameof(pictureUndo));
        }
    }
}