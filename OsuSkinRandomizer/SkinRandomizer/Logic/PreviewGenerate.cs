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
    public class PreviewGenerate
    {

        public BitmapImage GenerateBitmap(string skinFolder)
        {
            // neue bitmap erstellen
            Bitmap preview = new Bitmap("Assets\\previewBackground.png");
            Bitmap cursor = new Bitmap("Assets\\previewBackground.png");
            if(System.IO.File.Exists(skinFolder + "\\cursor@2x.png"))
            {
                cursor = new Bitmap(skinFolder + "\\cursor@2x.png");
            }
                

            Bitmap hitcircle = new Bitmap("Assets\\previewBackground.png");
            if (System.IO.File.Exists(skinFolder + "\\hitcircle@2x2.png"))
            {
                hitcircle = new Bitmap(skinFolder + "\\hitcircle@2x2.png");
            }


            using (Graphics g = Graphics.FromImage(preview))
            {
                g.DrawImage(hitcircle, cursor.Width + 15, 0, hitcircle.Width, hitcircle.Height);
                g.DrawImage(cursor, 0, 0, cursor.Width, cursor.Height);
            }

            cursor.Dispose();
            hitcircle.Dispose();
            

            return BitmapToImageSource(preview);
        }


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
