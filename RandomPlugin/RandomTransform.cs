using Interface;
using System;
using System.Drawing;

namespace PlugIn
{
    public class RandomTransform : IPlugin
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
            System.Drawing.Bitmap bitmap = app.Image;
            Random rand = new Random(DateTime.Now.Millisecond);
            int pixels = (int)(0.1 * bitmap.Width * bitmap.Height);

            for (int i = 0; i < pixels; ++i)
                bitmap.SetPixel(rand.Next(bitmap.Width - 1), rand.Next(bitmap.Height), Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255)));

            app.Image = bitmap;
        }
    }

}
