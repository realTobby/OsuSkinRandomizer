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
            Bitmap cursor = new Bitmap("Assets\\void.png");
            if(System.IO.File.Exists(skinFolder + "\\cursor@2x.png"))
            {
                cursor = new Bitmap(skinFolder + "\\cursor@2x.png");
            }
                
            Bitmap hitcircle = new Bitmap("Assets\\void.png");
            if (System.IO.File.Exists(skinFolder + "\\hitcircle@2x2.png"))
            {
                hitcircle = new Bitmap(skinFolder + "\\hitcircle@2x2.png");
            }


            List<Bitmap> comboImages = new List<Bitmap>();
            for(int i = 0; i < 9; i++)
            {
                if(System.IO.File.Exists(skinFolder + "\\combo-" + i + ".png"))
                    comboImages.Add(new Bitmap(skinFolder + "\\combo-" + i + ".png"));
            }


            using (Graphics g = Graphics.FromImage(preview))
            {
                g.DrawImage(hitcircle, cursor.Width + 15, 0, hitcircle.Width, hitcircle.Height);
                g.DrawImage(cursor, 0, 0, cursor.Width, cursor.Height);

                for (int i = 0; i < 9; i++)
                {
                    if(comboImages.Count() > i)
                    {
                        g.DrawImage(comboImages[i], 128 + i * comboImages[i].Width, 256, comboImages[i].Width, comboImages[i].Height);
                        comboImages[i].Dispose();
                    }
                    
                }
                

            }



            comboImages.Clear();

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
