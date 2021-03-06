﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace WindowsFormsApplication1
{
    public class bufferedLockBitmap
    {
         public Bitmap source = null;
        Bitmap source2 = null;

        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;
        Bitmap result = null;
        BitmapData bitmapData2 = null;
        public byte[] pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bufferedLockBitmap(Bitmap source)
        {

            this.source = source;
            this.Width = source.Width;
            this.Height = source.Height;
            source2 = new Bitmap(Width, Height);
           
        }
        public void LockBits()
        {
            try
            {
                Width = source.Width;
                Height = source.Height;
                int PixelCount = Width * Height;
                Rectangle rect = new Rectangle(0, 0, Width, Height);
              //  Rectangle rect2 = new Rectangle(0, 0, Width, Height);
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8 , 24 , 32 bpp images are supported. ");
                }
              //  bitmapData2 = source2.LockBits(rect2, ImageLockMode.ReadWrite, source.PixelFormat);
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite, source.PixelFormat);               
                int step = Depth / 8;
                pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;
                Marshal.Copy(Iptr, pixels, 0, pixels.Length);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UnlockBits()
        {
            try
            {
               
               // Iptr = bitmapData2.Scan0;
                Marshal.Copy(pixels,0,Iptr,pixels.Length);
                source.UnlockBits(bitmapData);
               // source2.UnlockBits(bitmapData2);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Color Getpixel(int x, int y)
        {
            Color clr = Color.Empty;
            int cCount = Depth / 8;
            int i = ((y * Width) + x) * cCount;
            if (i > pixels.Length - cCount)
                throw new IndexOutOfRangeException();
            if (Depth == 32)
            {
                byte b = pixels[i];
                byte g = pixels[i + 1];
                byte r = pixels[i + 2];
                byte a = pixels[i + 3];
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24)
            {
                byte b = pixels[i];
                byte g = pixels[i + 1];
                byte r = pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            {
                byte c = pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }
        public void SetPixel(int x, int y, Color color)
        {
            int cCount = Depth / 8;
            int i = ((y * Width) + x) * cCount;
            if (Depth == 32)
            {
                pixels[i] = color.B;
                pixels[i + 1] = color.G;
                pixels[i + 2] = color.R;
                pixels[i + 3] = color.A;
            }
            if (Depth == 24)
            {
                pixels[i] = color.B;
                pixels[i + 1] = color.G;
                pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            {
                pixels[i] = color.B;
            }
        }
    }
}
