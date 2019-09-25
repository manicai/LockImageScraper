using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace LockImageScraper
{
    internal static class ImageUtilities
    {
        /// <summary>
        /// Resize the image to the specified side and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="side">The longest length of side to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int side, bool padToSquare)
        {
            // Based on https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
            if (image == null || side == 0)
            {
                return null;
            }

            var destRectHeight = side;
            var destRectWidth = side;
            if (image.Width > image.Height)
            {
                destRectHeight = (int)(image.Height * ((double)destRectWidth / image.Width));
            }
            else
            {
                destRectWidth = (int)(image.Width * ((double)destRectHeight / image.Height));
            }

            int destImageWidth = padToSquare ? side : destRectWidth;
            int destImageHeight = padToSquare ? side : destRectHeight;
            int destRectXOrigin = (destImageWidth - destRectWidth) / 2;
            int destRectYOrigin = (destImageHeight - destRectHeight) / 2;

            var destRect = new Rectangle(destRectXOrigin, destRectYOrigin, destRectWidth, destRectHeight);
            var destImage = new Bitmap(destImageWidth, destImageHeight);

            destImage.SetResolution(image.HorizontalResolution, 
                image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, 
                        image.Width, image.Height, 
                        GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}