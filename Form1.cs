﻿using System;
using System.Drawing;
using System.Windows.Forms;

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
        int oldAngle = 0;
        Transformation transformationPolygon = new Transformation();
        Polygon polygon = new Polygon("teste.txt");
        Draw draw;

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            draw = new Draw(CreateGraphics());

            label1.Text = "Angulo: ";
            label3.Text = "Escala: ";
            textBox1.Text = "0";
            textBox2.Text = "1";

            pontos = polygon.GetPoints();
            draw.DrawLines(pontos);
            originalPontos = pontos;
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
            draw.DrawLines(pontos);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);
            Double realScale = trackBar2.Value * 0.1;
            textBox2.Text = (trackBar2.Value * 0.1).ToString();
            Console.WriteLine("Real scale = " + realScale + "Value = " + trackBar2.Value);
            pontos = transformationPolygon.Scale(realScale, originalPontos);
            draw.DrawLines(pontos);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            this.CreateGraphics().Clear(Form1.ActiveForm.BackColor);
            
            if (e.KeyData == Keys.A)
            {
                transformationPolygon.MovePolygon(pontos,-1, 0);
                transformationPolygon.MovePolygon(originalPontos, -1, 0);
                draw.DrawLines(pontos);

            }
            if (e.KeyData == Keys.S)
            {
                transformationPolygon.MovePolygon(pontos,0, 1);
                transformationPolygon.MovePolygon(originalPontos,0, 1);
                draw.DrawLines(pontos);

            }
            if (e.KeyData == Keys.D)
            {
                transformationPolygon.MovePolygon(pontos,1, 0);
                transformationPolygon.MovePolygon(originalPontos, 1, 0);
                draw.DrawLines(pontos);

            }
            if (e.KeyData == Keys.W)
            {
                transformationPolygon.MovePolygon(pontos,0, -1);
                transformationPolygon.MovePolygon(originalPontos, 0, -1);
                draw.DrawLines(pontos);

            }
        }
        
    }

}