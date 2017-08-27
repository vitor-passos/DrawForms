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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            ArrayList stringsTexto = new ArrayList();
            label1.Text = "Angulo: ";
            label2.Text = trackBar1.Value.ToString();

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

            Point[] pontos = new Point[stringsTexto.Count - 1];
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

            Graphics obj = CreateGraphics();
            Brush red = new SolidBrush(Color.Red);
            Brush blue = new SolidBrush(Color.Blue);
            Pen redPen = new Pen(red, 8);
            Font f = new Font(Font, FontStyle.Bold);


            for (int i = 0; i < pontos.Length; i++)
            {

                if (i == pontos.Length - 1)
                {
                    Bresenham(pontos[0].X, pontos[0].Y, pontos[i].X, pontos[i].Y, obj);
                }
                else
                {
                    Bresenham(pontos[i].X, pontos[i].Y, pontos[i + 1].X, pontos[i + 1].Y, obj);
                }
                obj.DrawString("[" + pontos[i].X + "," + pontos[i].Y + "]", f, red, pontos[i]);
            }
        }

        public void Bresenham(int x, int y, int x2, int y2, Graphics obj)
        {
            Brush blue = new SolidBrush(Color.Blue);
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
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
            label2.Text = trackBar1.Value.ToString();
            //Calculo do angulo
            //redesenhar linhas
        }
    }

}
