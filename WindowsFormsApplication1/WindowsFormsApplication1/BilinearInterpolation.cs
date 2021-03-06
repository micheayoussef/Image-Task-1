﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WindowsFormsApplication1;

namespace TestMatrixTransformations
{
    class BilinearInterpolation
    {

        /**
         * find 4 adjacent pixels
            X1 = floor (OldX)	; X2 = X1 + 1
            Y1 = floor (OldY)	; Y2 = Y1 + 1
            P1 = OrigBuf[X1,Y1] 	; P2 = OrigBuf[X2,Y1]
            P3 = OrigBuf[X1,Y2] 	; P4 = OrigBuf[X2,Y2] 
            Calculate X, Y fractions
            Xfraction = OldX – X1
            Yfraction = OldY – Y1
            Interpolate in X-Direction
            Z1 = P1 × (1 – Xfraction) + P2 × Xfraction
            Z2 = P3 × (1 – Xfraction) + P4 × Xfraction
            Interpolate in Y-Direction
            NewPixel = Z1 × (1 – Yfraction) + Z2 × Yfraction
         */
        public Color calculate(bufferedLockBitmap _original_buffer, float _old_x, float _old_y,int _new_width, int _new_height)
        {
            int new_red, new_green, new_blue;
            Color ret;
            int x_1 = (int)Math.Floor(_old_x);
            int x_2 = x_1 + 1;

            int y_1 = (int)Math.Floor(_old_y);
            int y_2 = y_1 + 1;

            Color p_1 = Color.FromArgb(0);
            Color p_2 = Color.FromArgb(0); 
            Color p_3 = Color.FromArgb(0);
            Color p_4 = Color.FromArgb(0);
            if (x_2 < _new_width && y_2 < _new_height)
            {
                p_1 = _original_buffer.Getpixel(x_1, y_1);
                p_2 = _original_buffer.Getpixel(x_2, y_2);
                p_3 = _original_buffer.Getpixel(x_1, y_2);
                p_4 = _original_buffer.Getpixel(x_2, y_2);

            }

            float x_fraction = _old_x - x_1;
            float y_fraction = _old_y - y_1;

            // Interpolate in X-Direction
            // Red
            int r_1 = (int)Math.Floor(p_1.R * (1 - x_fraction) + p_2.R * x_fraction);
            int r_2 = (int)Math.Floor(p_3.R * (1 - x_fraction) + p_4.R * x_fraction);

            // Green
            int g_1 = (int)Math.Floor(p_1.G * (1 - x_fraction) + p_2.G * x_fraction);
            int g_2 = (int)Math.Floor(p_3.G * (1 - x_fraction) + p_4.G * x_fraction);

            // Blue
            int b_1 = (int)Math.Floor(p_1.B * (1 - x_fraction) + p_2.B * x_fraction);
            int b_2 = (int)Math.Floor(p_3.B * (1 - x_fraction) + p_4.B * x_fraction);

            // Interpolate in Y-Direction
            new_red = (int)Math.Floor(r_1 * (1 - y_fraction) + r_2 * y_fraction);
            new_green = (int)Math.Floor(g_1 * (1 - y_fraction) + g_2 * y_fraction);
            new_blue = (int)Math.Floor(b_1 * (1 - y_fraction) + b_2 * y_fraction);
            ret = Color.FromArgb(new_red, new_green, new_blue);
            return ret;
        }
    }
}
