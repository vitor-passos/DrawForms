using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace DrawForms
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        PointF[] pontos;
        PointF[] originalPontos;
        Graphics obj;
        int oldAngle = 0;
        Transformation transformationPolygon = new Transformation();
        Polygon polygon = new Polygon("teste.txt");

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            label1.Text = "Angulo: ";
            label3.Text = "Escala: ";
            textBox1.Text = "0";
            textBox2.Text = "1";
            pontos = polygon.getPoints();
            obj = CreateGraphics();
            DrawLines(obj,pontos);
            originalPontos = pontos;

            
        }
       
        public void DrawLines(Graphics obj, PointF[] desenhar)
        {
            for (int i = 0; i < desenhar.Length; i++)
            {

                if (i == desenhar.Length - 1)
                {
                    Bresenham(desenhar[0].X, desenhar[0].Y, desenhar[i].X, desenhar[i].Y, obj);
                }
                else
                {
                    Bresenham(desenhar[i].X, desenhar[i].Y, desenhar[i + 1].X, desenhar[i + 1].Y, obj);
                }

                Font f = new Font(Font, FontStyle.Bold);
                Brush red = new SolidBrush(Color.Red);
                obj.DrawString("[" + desenhar[i].X + " | " + desenhar[i].Y + "]", f, red, desenhar[i]);
            }

        }

        public void Bresenham(float x, float y, float x2, float y2, Graphics obj)
        {
            Brush blue = new SolidBrush(Color.Blue);
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
        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);
            this.textBox1.Text = trackBar1.Value.ToString();
        
            if(oldAngle > 0)
            {
                pontos = transformationPolygon.RotationEixo(pontos,trackBar1.Value - oldAngle);
                originalPontos = transformationPolygon.RotationEixo(originalPontos, trackBar1.Value - oldAngle);
            }
            else
            {
                pontos = transformationPolygon.RotationEixo(pontos,trackBar1.Value);
                originalPontos = transformationPolygon.RotationEixo(originalPontos, trackBar1.Value);
            }
            oldAngle = trackBar1.Value;
            DrawLines(obj,pontos);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);
            Double realScale = trackBar2.Value * 0.1;
            textBox2.Text = (trackBar2.Value * 0.1).ToString();
            Console.WriteLine("Real scale = " + realScale + "Value = " + trackBar2.Value);
            pontos = transformationPolygon.Scale(realScale, originalPontos);
            
            DrawLines(obj,pontos);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);

            if (e.KeyData == Keys.A)
            {
                transformationPolygon.MovePolygon(pontos,-1, 0);
                transformationPolygon.MovePolygon(originalPontos, -1, 0);
                DrawLines(obj, pontos);

            }
            if (e.KeyData == Keys.S)
            {
                transformationPolygon.MovePolygon(pontos,0, 1);
                transformationPolygon.MovePolygon(originalPontos,0, 1);
                DrawLines(obj, pontos);

            }
            if (e.KeyData == Keys.D)
            {
                transformationPolygon.MovePolygon(pontos,1, 0);
                transformationPolygon.MovePolygon(originalPontos, 1, 0);
                DrawLines(obj, pontos);

            }
            if (e.KeyData == Keys.W)
            {
                transformationPolygon.MovePolygon(pontos,0, -1);
                transformationPolygon.MovePolygon(originalPontos, 0, -1);
                DrawLines(obj, pontos);

            }
        }
        
    }

}