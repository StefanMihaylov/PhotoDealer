namespace PhotoDealer.Web.Controllers
{
    using ImageProcessor;
    using ImageProcessor.Imaging;
    using ImageProcessor.Imaging.Formats;
    using PhotoDealer.Data;
    using PhotoDealer.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class PhotoController : BaseController
    {
        public PhotoController(IPhotoDealerData photoDb)
            : base(photoDb)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetImage()
        {
            var picture = this.PhotoDb.Pictures.All().First();

            int widthSize = 300;
            var fontName = "Freestyle Script";
            var watermarkTextLayer = WatermarkSettings("PhotoDealer", fontName, widthSize, 95);

            using (MemoryStream inStream = new MemoryStream(picture.Content))
            {
                MemoryStream outStream = new MemoryStream();

                // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                {
                    // Load, resize, set the format and quality and save an image.
                    imageFactory.Load(inStream)
                                .Resize(new Size(widthSize, 0))
                                .Quality(60)
                                .Watermark(watermarkTextLayer)
                                .Save(outStream);
                }

                outStream.Seek(0, SeekOrigin.Begin);
                return File(outStream, picture.ContentType);
            }
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
                FontColor = Color.Black,
                FontFamily = fontFamily,
                FontSize = 70,
                Style = FontStyle.Regular,
                Opacity = 40,
                DropShadow = true
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


        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                string fileName = file.FileName;
                string contentType = file.ContentType;
                int lenght = file.ContentLength;

                if (IsPicture(contentType))
                {
                    BinaryReader reader = new BinaryReader(file.InputStream);
                    byte[] byteContent = reader.ReadBytes(lenght);

                    var image = Image.FromStream(file.InputStream);
                    var picture = new Picture()
                    {
                        Content = byteContent,
                        ContentType = contentType,
                        FileName = fileName,
                        WidthPixels = image.Width,
                        HeightPixels = image.Height
                    };

                    this.PhotoDb.Pictures.Add(picture);
                    this.PhotoDb.SaveChanges();
                }
                else
                {
                    // wrong file format
                }
            }
            else
            {
                // no file
            }


            return View();
        }

        private bool IsPicture(string contentType)
        {
            var contentTypeParts = contentType.Split('/');
            return contentTypeParts[0] == "image";
        }
    }
}