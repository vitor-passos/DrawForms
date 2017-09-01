using System;
using System.Collections;
using System.Drawing;
using System.IO;

public class Polygon
{

    private PointF[] pontos;
    private ArrayList stringsTexto = new ArrayList();

    public Polygon(String nameDoc)
	{
        try
        {   // Open the text file using a stream reader.

            Stream entrada = File.Open(nameDoc, FileMode.Open);
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

        FormPolygon();
        
    }

    private void FormPolygon ()
    {
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
    }

    public PointF[] GetPoints ()
    {
        return pontos;
    }
}
