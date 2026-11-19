using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlowerShop.Services.HelpHashMethods
{
    public static class RehashImage
    {
        public static Bitmap? ConvertToImage(byte[] hashedImage)
        {
            if (hashedImage == null || hashedImage.Length == 0)
            {
                return null;
            }
            using (var stream = new MemoryStream(hashedImage))
            {
                return new Bitmap(stream);
            }
        }

        public static byte[]? ConvertToHash(Bitmap selectedImage)
        {
            if (selectedImage == null)
            {
                return null;
            }
            using (var stream = new MemoryStream())
            {
                selectedImage.Save(stream);
                return stream.ToArray();
            }
        }
    }
}
