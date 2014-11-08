namespace PhotoDealer.Logic
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    using ImageProcessor;
    using ImageProcessor.Imaging;

    public class ImageProcess : IImageProcess
    {
        public const string FontName = "Freestyle Script";
        public const string WaterMark = "PhotoDealer";
        public Color FontColor = Color.Black;
        public const FontStyle Style = FontStyle.Regular;
        public const int WatermarkOpacity = 40;
        public const bool WatermarkShadow = true;

        public MemoryStream Resize(MemoryStream inputStream, int pictureWidth, int quality)
        {
            var watermarkTextLayer = WatermarkSettings(WaterMark, FontName, pictureWidth, 95);
            MemoryStream outStream = new MemoryStream();

            // Initialize the ImageFactory using the overload to preserve EXIF metadata.
            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
            {
                // Load, resize, set the format and quality and save an image.
                imageFactory.Load(inputStream)
                            .Resize(new Size(pictureWidth, 0))
                            .Quality(quality)
                            .Watermark(watermarkTextLayer)
                            .Save(outStream);
            }

            return outStream;
        }

        private TextLayer WatermarkSettings(string waterMarktext, string fontName, int widthSize, int ratio)
        {
            var fontFamily = FontFamily.Families.FirstOrDefault(f => f.Name == fontName);
            if (fontFamily == null)
            {
                fontFamily = FontFamily.GenericSansSerif;
            }

            var watermarkTextLayer = new TextLayer()
            {
                Text = waterMarktext,
                FontColor = FontColor,
                FontFamily = fontFamily,
                Style = Style,
                Opacity = WatermarkOpacity,
                DropShadow = WatermarkShadow
            };

            watermarkTextLayer.FontSize = CalculateFontSize(watermarkTextLayer, widthSize, ratio);
            return watermarkTextLayer;
        }

        private int CalculateFontSize(TextLayer watermark, int pictureWidth, int ratio, int maxSize = 200)
        {
            var graphics = Graphics.FromImage(new Bitmap(1, 1));

            int key = pictureWidth * ratio / 100;

            int minSize = 1;

            while (maxSize >= minSize)
            {
                // calculate the midpoint for roughly equal partition
                int sizeMid = minSize + (maxSize - minSize) / 2;

                var font = new Font(watermark.FontFamily.Name, sizeMid, watermark.Style, GraphicsUnit.Point);
                var sizeF = graphics.MeasureString(watermark.Text, font);
                var newSize = Convert.ToInt32(sizeF.Width);

                if (newSize == key)
                {
                    // key found at index imid
                    return sizeMid;
                }
                // determine which subarray to search
                else if (newSize < key)
                {
                    // change min index to search upper subarray
                    minSize = sizeMid + 1;
                }
                else
                {
                    // change max index to search lower subarray
                    maxSize = sizeMid - 1;
                }

            }
            // key was not found

            return minSize;
        }
    }
}
