using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ThreadTest.ImageResize
{
    class Images
    {
        public Bitmap bitmap;
        public string pathToImage;

        public Images(Bitmap bitmap, string pathToImage)
        {
            this.bitmap = bitmap;
            this.pathToImage = pathToImage; 
        }
    }
}
