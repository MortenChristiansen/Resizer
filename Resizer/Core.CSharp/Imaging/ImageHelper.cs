using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CSharp.Imaging
{
    static class ImageHelper
    {
        public static Image ResizeImage(Image originalBitmap, int maxDimension, ResizingApproach resizingApproach)
        {
            float aspectX = maxDimension / (float)originalBitmap.Width;
            float aspectY = maxDimension / (float)originalBitmap.Height;

            float actualAspect = 0;
            if (resizingApproach == ResizingApproach.Max) actualAspect = Math.Min(aspectX, aspectY);
            else if (resizingApproach == ResizingApproach.Min) actualAspect = Math.Max(aspectX, aspectY);
            else if (resizingApproach == ResizingApproach.Horizontal) actualAspect = aspectX;
            else if (resizingApproach == ResizingApproach.Vertical) actualAspect = aspectY;

            int sourceWidth = (int)(originalBitmap.Width * actualAspect);
            int sourceHeight = (int)(originalBitmap.Height * actualAspect);

            var resized = new Bitmap(sourceWidth, sourceHeight);

            Graphics g = Graphics.FromImage(resized);
            g.DrawImage(originalBitmap, new Rectangle(0, 0, resized.Width, resized.Height));
            return resized;
        }

        public static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(codecInfo => codecInfo.MimeType == mimeType);
        }

        public static void SaveJpg(Image image, string fileName, long quality)
        {
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ImageCodecInfo codecInfo = GetEncoderInfo("image/jpeg");
            image.Save(fileName, codecInfo, parameters);
        }

        public static void CopyImageProperties(Image source, Image recepient)
        {
            foreach (PropertyItem prop in source.PropertyItems)
            {
                recepient.SetPropertyItem(prop);
            }
        }
    }
}
