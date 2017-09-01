using System;
using System.Drawing;

public class Transformation
{
	public Transformation()
	{
	}

    public PointF[] Scale(Double scale, PointF[] a)
    {
        float centerX = 0;
        float centerY = 0;
        PointF[] newArray = new PointF[a.Length];
        for (int j = 0; j < a.Length; j++)
        {
            centerX += a[j].X;
            centerY += a[j].Y;
        }

        centerX = centerX / a.Length;
        centerY = centerY / a.Length;

        for (int i = 0; i < a.Length; i++)
        {
            newArray[i].X = (float)((a[i].X - centerX) * scale + centerX);
            newArray[i].Y = (float)((a[i].Y - centerY) * scale + centerY);

        }

        return newArray;

    }

    public PointF[] RotationAxis(PointF[] a, Double angle)
    {
        float[,] matriz = new float[2, 1];
        Double[,] rotationMatriz = new Double[2, 2];

        Double radiuns = Math.PI * angle / 180.0;

        rotationMatriz[0, 0] = Math.Cos(radiuns);
        rotationMatriz[0, 1] = Math.Sin(radiuns);
        rotationMatriz[1, 0] = -Math.Sin(radiuns);
        rotationMatriz[1, 1] = Math.Cos(radiuns);

        float centerX = 0;
        float centerY = 0;
        for (int j = 0; j < a.Length; j++)
        {
            centerX += a[j].X;
            centerY += a[j].Y;
        }

        centerX = centerX / a.Length;
        centerY = centerY / a.Length;

        for (int i = 0; i < a.Length; i++)
        {
            matriz[0, 0] = a[i].X;
            matriz[1, 0] = a[i].Y;

            a[i].X = (float)(((matriz[0, 0] - centerX) * rotationMatriz[0, 0] + (matriz[1, 0] - centerY) * rotationMatriz[0, 1]) + centerX);
            a[i].Y = (float)(((matriz[0, 0] - centerX) * rotationMatriz[1, 0] + (matriz[1, 0] - centerY) * rotationMatriz[1, 1]) + centerY);

        }

        return a;

    }

    public PointF[] MovePolygon(PointF[] a, float xDes, float yDes)
    {
        for (int i = 0; i < a.Length; i++)
        {
            a[i].X = a[i].X + xDes;
            a[i].Y = a[i].Y + yDes;
        }
        return a;
    }
}
