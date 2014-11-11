namespace PhotoDealer.Logic
{
    using System.IO;

    public interface IImageProcess
    {
        MemoryStream Resize(byte[] fileContent, int pictureWidth, int quality, bool watermark = true);
    }
}
