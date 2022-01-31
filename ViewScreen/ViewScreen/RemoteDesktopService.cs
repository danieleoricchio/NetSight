using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class RemoteDesktopService 
    {
        // An instance of the screen capture class.
        //
        private ScreenCapture capture = new ScreenCapture();

        /// <summary>
        /// Capture the screen image and return bytes.
        /// </summary>
        /// <returns>4 ints [top,bot,left,right] (16 bytes) + image data bytes</returns>
        public byte[] UpdateScreenImage()
        {
            // Capture minimally sized image that encompasses
            //    all the changed pixels.
            //
            Rectangle bounds = new Rectangle();
            Bitmap img = capture.GetScreen(ref bounds);
            if (img != null)
            {
                // Something changed.
                //
                byte[] result = PackScreenCaptureData(img, bounds);
                
                return result;
            }
            else
            {
                return null;
            }
        }

        public static byte[] PackScreenCaptureData(Image image, Rectangle bounds)
        {
            // Pack the image data into a byte stream to
            //    be transferred over the wire.
            //

            // Get the bytes of the image data.
            //    Note: We are using JPEG compression.
            //
            byte[] imgData;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgData = ms.ToArray();
            }

            // Get the bytes that describe the bounding
            //    rectangle.
            //
            byte[] topData = BitConverter.GetBytes(bounds.Top);
            byte[] botData = BitConverter.GetBytes(bounds.Bottom);
            byte[] leftData = BitConverter.GetBytes(bounds.Left);
            byte[] rightData = BitConverter.GetBytes(bounds.Right);

            // Create the final byte stream.
            // Note: We are streaming back both the bounding
            //    rectangle and the image data.
            //
            int sizeOfInt = topData.Length;
            byte[] result = new byte[imgData.Length + 4 * sizeOfInt];
            Array.Copy(topData, 0, result, 0, topData.Length);
            Array.Copy(botData, 0, result, sizeOfInt, botData.Length);
            Array.Copy(leftData, 0, result, 2 * sizeOfInt, leftData.Length);
            Array.Copy(rightData, 0, result, 3 * sizeOfInt, rightData.Length);
            Array.Copy(imgData, 0, result, 4 * sizeOfInt, imgData.Length);

            return result;
        }

    }

    
}
