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

        PointF[] pontos;
        Graphics obj;
        int oldAngle = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            ArrayList stringsTexto = new ArrayList();
            label1.Text = "Angulo: ";
            label3.Text = "Escala: ";
            textBox1.Text = "0";
            textBox2.Text = "1";

            try
            {   // Open the text file using a stream reader.

                Stream entrada = File.Open("teste.txt", FileMode.Open);
                StreamReader leitor = new StreamReader(entrada);
                string linha = leitor.ReadLine();
                stringsTexto.Add("" + linha);
                while (linha != null)
                {
                    //MessageBox.Show(linha);
                    linha = leitor.ReadLine();
                    stringsTexto.Add(linha);

                }

                leitor.Close();
                entrada.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }

            pontos = new PointF[stringsTexto.Count - 1];
            int countPoints = 0;

            for (int i = 0; i < stringsTexto.Count - 1; i++)
            {
                String value = stringsTexto[i].ToString();
                Char delimiter = ',';
                String[] substrings = value.Split(delimiter);
                int counting = 0;
                int x = 0;
                int y = 0;
                foreach (var substring in substrings)
                {
                    if (counting == 0)
                    {
                        Int32.TryParse(substring, out x);
                        counting++;
                    }
                    else
                    {
                        Int32.TryParse(substring, out y);
                    }
                }
                pontos[countPoints] = new Point(x, y);
                countPoints++;

            }

            // for (int i = 0; i < pontos.Length - 1; i++)
            //   Console.WriteLine(pontos[i]);

            obj = CreateGraphics();
            DrawLines(obj,pontos);
            
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
                obj.DrawString("[" + desenhar[i].X + "," + desenhar[i].Y + "]", f, red, desenhar[i]);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);
            //label2.Text = trackBar1.Value.ToString();
            this.textBox1.Text = trackBar1.Value.ToString();
            

            if(oldAngle > 0)
            {
                RotationEixo(trackBar1.Value - oldAngle);
            }
            else
            {
                RotationEixo(trackBar1.Value);
            }
            oldAngle = trackBar1.Value;

            //redesenhar linhas
            DrawLines(obj,pontos);
        }

        private void RotationEixo (Double angle)
        {
            float[,] matriz = new float[2, 1];
            Double[,] rotationMatriz = new Double[2, 2];

            Double degress = Math.PI * angle / 180.0;

            rotationMatriz[0, 0] = Math.Cos(degress);
            rotationMatriz[0, 1] = Math.Sin(degress);
            rotationMatriz[1, 0] = -Math.Sin(degress);
            rotationMatriz[1, 1] = Math.Cos(degress);
            float centerX = 0;
            float centerY = 0;
            for (int j =0;j< pontos.Length; j++)
            {
                centerX += pontos[j].X;
                centerY += pontos[j].Y;
            }

            centerX = centerX/pontos.Length;
            centerY = centerY/pontos.Length;
            for (int i = 0; i < pontos.Length; i++)
            {

                matriz[0, 0] = pontos[i].X;
                matriz[1, 0] = pontos[i].Y;

                pontos[i].X = (float)(((matriz[0, 0] -centerX) * rotationMatriz[0, 0] + (matriz[1, 0]-centerY) * rotationMatriz[0, 1])+centerX);
                pontos[i].Y = (float)(((matriz[0, 0] - centerX)* rotationMatriz[1, 0] + (matriz[1, 0]-centerY) * rotationMatriz[1, 1])+centerY);

            }
        
        }

        private PointF[] Scale (Double scale, PointF[] a)
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
                newArray[i].X = (float) ((a[i].X - centerX) * scale + centerX);
                newArray[i].Y = (float)((a[i].Y - centerY) * scale + centerY);

            }

            return newArray;

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);
            Double realScale = trackBar2.Value * 0.1;
            textBox2.Text = (trackBar2.Value * 0.1).ToString();
            Console.WriteLine("Real scale = " + realScale + "Value = " + trackBar2.Value);
            PointF[] teste = Scale(realScale, pontos);
            
            DrawLines(obj,teste);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }

}