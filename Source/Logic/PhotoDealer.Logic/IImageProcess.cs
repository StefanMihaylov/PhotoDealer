namespace PhotoDealer.Logic
{
    using System.IO;

    public interface IImageProcess
    {
        MemoryStream Resize(MemoryStream inputStream, int pictureWidth, int quality);
    }
}
