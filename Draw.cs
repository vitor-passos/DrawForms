using System;
using System.Drawing;


public class Draw
{
    private Graphics obj;
    private FontFamily fontFamily;
    private Font font;
    private Brush red;
    private Brush blue;
    public Draw(Graphics o)
    {
        obj = o;
        fontFamily = new FontFamily("Arial");
        font = new Font(fontFamily, 11, FontStyle.Regular, GraphicsUnit.Pixel);
        red = new SolidBrush(Color.Red);
        blue = new SolidBrush(Color.Blue);
    }

    public void DrawLines(PointF[] polygon)
    {
        for (int i = 0; i < polygon.Length; i++)
        {

            if (i == polygon.Length - 1)
            {
                Bresenham(polygon[0].X, polygon[0].Y, polygon[i].X, polygon[i].Y);
            }
            else
            {
                Bresenham(polygon[i].X, polygon[i].Y, polygon[i + 1].X, polygon[i + 1].Y);
            }
            
            obj.DrawString("[" + polygon[i].X + " | " + polygon[i].Y + "]", font, red, polygon[i]);
        }

    }

    private void Bresenham(float x, float y, float x2, float y2)
    {
        float w = x2 - x;
        float h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Convert.ToInt32(Math.Abs(w));
        float shortest = Math.Abs(h);
        if (!(longest > shortest))
        {
            longest = Convert.ToInt32(Math.Abs(h));
            shortest = Math.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        float numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            obj.FillRectangle(blue, x, y, 1, 1);
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
    }
}
