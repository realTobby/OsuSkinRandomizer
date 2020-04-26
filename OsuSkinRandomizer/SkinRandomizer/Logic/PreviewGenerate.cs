using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SkinRandomizer.Logic
{
    /// <summary>
    /// PreviewGenerate will create a bitmap and the random skin elements on it so the user can see the result before jumping into osu
    /// </summary>
    public class PreviewGenerate
    {
        /// <summary>
        /// this is a really dumb way of doing it, its cost very much lines, this needs a redesign
        /// </summary>
        /// <param name="skinFolder"></param>
        /// <returns></returns>
        public BitmapImage GenerateBitmap(string skinFolder)
        {
            #region Cursor
            Bitmap preview = new Bitmap("Assets\\previewBackground.png"); // load background
            Bitmap cursor = new Bitmap("Assets\\void.png"); // prepare cursor element, make it black if its not found
            if(System.IO.File.Exists(skinFolder + "\\cursor@2x.png"))
            {
                cursor = new Bitmap(skinFolder + "\\cursor@2x.png"); // assign the correct cursor element
            }
            #endregion

            #region HitCircle
            Bitmap hitcircle = new Bitmap("Assets\\void.png");
            if (System.IO.File.Exists(skinFolder + "\\hitcircle@2x2.png"))
            {
                hitcircle = new Bitmap(skinFolder + "\\hitcircle@2x2.png");
            }
            #endregion

            #region ComboNumbers
            List<Bitmap> comboImages = new List<Bitmap>();
            for(int i = 0; i < 9; i++)
            {
                if(System.IO.File.Exists(skinFolder + "\\combo-" + i + ".png"))
                    comboImages.Add(new Bitmap(skinFolder + "\\combo-" + i + ".png"));
            }

            #endregion

            using (Graphics g = Graphics.FromImage(preview)) // draw the collected images onto the background
            {
                g.DrawImage(hitcircle, cursor.Width + 15, 0, hitcircle.Width, hitcircle.Height); // draw the hitcircle
                g.DrawImage(cursor, 0, 0, cursor.Width, cursor.Height); // draw the cursor

                // draw the combo numbers
                for (int i = 0; i < 9; i++)
                {
                    if(comboImages.Count() > i)
                    {
                        g.DrawImage(comboImages[i], 128 + i * comboImages[i].Width, 256, comboImages[i].Width, comboImages[i].Height);
                        comboImages[i].Dispose();
                    }
                }
            }
            // take care of releasing the resources, or else the application cannot create a new skin when these are still in use:
            comboImages.Clear();
            cursor.Dispose();
            hitcircle.Dispose();

            return BitmapToImageSource(preview);
        }

        // helper function to get a wpf imagesource from a normal bitmap
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
