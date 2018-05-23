using Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn
{
    public class ReverseTransform : IPlugin
    {
        public string Name
        {
            get
            {
                return "Переворот изображения";
            }
        }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        public string Author
        {
            get
            {
                return "Me";
            }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;

            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height / 2; ++j)
                {
                    Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, bitmap.GetPixel(i, bitmap.Height - j - 1));
                    bitmap.SetPixel(i, bitmap.Height - j - 1, color);
                }

            app.Image = bitmap;
        }
    }
    public class RandomTransform : Interface.IPlugin
    {
        public string Name
        {
            get
            {
                return "Случайная трансформация";
            }
        }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        public string Author
        {
            get
            {
                return "Me";
            }
        }

        public void Transform(Interface.IMainApp app)
        {
            Bitmap bitmap = app.Image;
            Random rand = new Random(DateTime.Now.Millisecond);
            int pixels = (int)(0.1 * bitmap.Width * bitmap.Height);

            for (int i = 0; i < pixels; ++i)
                bitmap.SetPixel(rand.Next(bitmap.Width - 1), rand.Next(bitmap.Height), Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));

            app.Image = bitmap;
        }
    }

}
