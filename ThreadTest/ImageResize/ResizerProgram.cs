using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadTest.ImageResize
{
    class ResizerProgram
    {
        static BlockingCollection<Images> imageCollection;
        private string pathWithImage = "";
        private string pathToMove = "";
        Point resolution = new Point(800,600);


        public void Start()
        {
            while (IsJpgExist(pathWithImage))
            {
                Console.WriteLine("Try to resize image");
                ThreadPool.QueueUserWorkItem(Resize, GetRandomImage(pathWithImage));
            }
        }

        private void Resize(object o)
        {
            Images inputImage = (Images) o;
            //
            try
            {
                inputImage.bitmap.SetResolution(resolution.X,resolution.Y);
                Console.WriteLine("Image resized");
                ThreadPool.QueueUserWorkItem(RenameAndMove, inputImage);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with set resolution");
                Console.WriteLine(e.Message);
            }

            //
        }

        private void RenameAndMove(object o)
        {

            Images inputImage = (Images)o;
            try
            {
                Console.WriteLine("Try to save image");
                inputImage.bitmap.Save($"{pathToMove}{new Random().Next(99999999)}.jpg");
                Console.WriteLine("Image saved");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with save");
                Console.WriteLine(e.Message);
            }

            //

        }

        private Bitmap GetRandomImage(string path)
        {
            string randomImagePath = Directory.GetFiles(path)[new Random().Next(Directory.GetFiles(path).Length)];
            return new Bitmap(Image.FromFile(randomImagePath));
        }

        private static bool IsJpgExist(string path)
        {
            try
            {
                if (Directory.GetFiles(path, "*.dll") != null) return true;
                else return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.ToString()}");
                return false;
            }
        }
    }
}
