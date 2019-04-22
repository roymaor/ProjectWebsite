using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace HELPER
{
    public class ImageUtilities
    {
        // RESIZE IMAGE
        // http://stackoverflow.com/questions/1922040/resize-an-image-c-sharp

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

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
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /*
wrapMode.SetWrapMode(WrapMode.TileFlipXY) prevents ghosting around the image borders -- naïve resizing will sample transparent pixels beyond the image boundaries, but by mirroring the image we can get a better sample (this setting is very noticeable)
destImage.SetResolution maintains DPI regardless of physical size -- may increase quality when reducing image dimensions or when printing
Compositing controls how pixels are blended with the background -- might not be needed since we're only drawing one thing.
graphics.CompositingMode determines whether pixels from a source image overwrite or are combined with background pixels. SourceCopy specifies that when a color is rendered, it overwrites the background color.
graphics.CompositingQuality determines the rendering quality level of layered images.
graphics.InterpolationMode determines how intermediate values between two endpoints are calculated
graphics.SmoothingMode specifies whether lines, curves, and the edges of filled areas use smoothing (also called antialiasing) -- probably only works on vectors
graphics.PixelOffsetMode affects rendering quality when drawing the new image
Maintaining aspect ratio is left as an exercise for the reader (actually, I just don't think it's this function's job to do that for you).

Also, this is a good article (http://www.nathanaeljones.com/blog/2009/20-image-resizing-pitfalls) describing some of the pitfalls with image resizing. The above function will cover most of them, but you still have to worry about saving. (https://msdn.microsoft.com/en-us/library/bb882583(v=vs.110).aspx) 
         */



        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        // yourImage = resizeImage(yourImage, new Size(50,50));




        public static Image ResizeImage(string stPhotoPath, int newWidth, int newHeight)
        {
            Image imgPhoto = Image.FromFile(stPhotoPath);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;

            //Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                int buff = newWidth;

                newWidth = newHeight;
                newHeight = buff;
            }

            int sourceX = 0, sourceY = 0, destX = 0, destY = 0;
            float nPercent = 0, nPercentW = 0, nPercentH = 0;

            nPercentW = ((float)newWidth / (float)sourceWidth);
            nPercentH = ((float)newHeight / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((newWidth -
                          (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((newHeight -
                          (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap(newWidth, newHeight,
                          PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode =
                System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            imgPhoto.Dispose();

            return bmPhoto;
        }



        /// <summary>
        /// Resize image with a directory as source
        /// </summary>
        /// <param name="OriginalFileLocation">Image location</param>
        /// <param name="heigth">new height</param>
        /// <param name="width">new width</param>
        /// <param name="keepAspectRatio">keep the aspect ratio</param>
        /// <param name="getCenter">return the center bit of the image</param>
        /// <returns>image with new dimentions</returns>
        public static Image ResizeImageFromFile(String OriginalFileLocation, int heigth, int width, Boolean keepAspectRatio, Boolean getCenter)
        {
            int newheigth = heigth;
            System.Drawing.Image FullsizeImage = System.Drawing.Image.FromFile(OriginalFileLocation);

            // Prevent using images internal thumbnail
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (keepAspectRatio || getCenter)
            {
                int bmpY = 0;
                double resize = (double)FullsizeImage.Width / (double)width;//get the resize vector
                if (getCenter)
                {
                    bmpY = (int)((FullsizeImage.Height - (heigth * resize)) / 2);// gives the Y value of the part that will be cut off, to show only the part in the center
                    Rectangle section = new Rectangle(new Point(0, bmpY), new Size(FullsizeImage.Width, (int)(heigth * resize)));// create the section to cut of the original image
                    //System.Console.WriteLine("the section that will be cut off: " + section.Size.ToString() + " the Y value is minimized by: " + bmpY);
                    Bitmap orImg = new Bitmap((Bitmap)FullsizeImage);//for the correct effect convert image to bitmap.
                    FullsizeImage.Dispose();//clear the original image
                    using (Bitmap tempImg = new Bitmap(section.Width, section.Height))
                    {
                        Graphics cutImg = Graphics.FromImage(tempImg);//              set the file to save the new image to.
                        cutImg.DrawImage(orImg, 0, 0, section, GraphicsUnit.Pixel);// cut the image and save it to tempImg
                        FullsizeImage = tempImg;//save the tempImg as FullsizeImage for resizing later
                        orImg.Dispose();
                        cutImg.Dispose();
                        return FullsizeImage.GetThumbnailImage(width, heigth, null, IntPtr.Zero);
                    }
                }
                else newheigth = (int)(FullsizeImage.Height / resize);//  set the new heigth of the current image
            }//return the image resized to the given heigth and width

            return FullsizeImage.GetThumbnailImage(width, newheigth, null, IntPtr.Zero);
        }

        /// <summary>
        /// Resize image with a directory as source
        /// </summary>
        /// <param name="OriginalFileLocation">Image location</param>
        /// <param name="heigth">new height</param>
        /// <param name="width">new width</param>
        /// <returns>image with new dimentions</returns>
        public static Image ResizeImageFromFile(String OriginalFileLocation, int heigth, int width)
        {
            return ResizeImageFromFile(OriginalFileLocation, heigth, width, false, false);
        }

        /// <summary>
        /// Resize image with a directory as source
        /// </summary>
        /// <param name="OriginalFileLocation">Image location</param>
        /// <param name="heigth">new height</param>
        /// <param name="width">new width</param>
        /// <param name="keepAspectRatio">keep the aspect ratio</param>
        /// <returns>image with new dimentions</returns>
        public static Image ResizeImageFromFile(String OriginalFileLocation, int heigth, int width, Boolean keepAspectRatio)
        {
            return ResizeImageFromFile(OriginalFileLocation, heigth, width, keepAspectRatio, false);
        }



        public static Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
    }
}
