using Microsoft.VisualBasic.ApplicationServices;
using System.Globalization;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;



namespace zaliczenie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string nazwa_plikw = textBox1.Text;
                string nazwa_plikz = textBox2.Text;
                string plik_wczytaj = @"C:\Users\kamil\OneDrive\Pulpit\studia\c#\Zaliczenie\Zaliczenie\bin\Debug\" + nazwa_plikw + ".txt";
                string plik_zapisz = @"C:\Users\kamil\OneDrive\Pulpit\studia\c#\Zaliczenie\Zaliczenie\bin\Debug\" + nazwa_plikz + ".txt";

                using (StreamReader sr = new StreamReader(plik_wczytaj))
                {
                    double suma = 0;
                    int i = 0;
                    string line;

                    CultureInfo culture = CultureInfo.InvariantCulture;

                    List<double> temperatureDifferences = new List<double>();

                    double previousTemperature = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');

                        if (parts.Length == 2)
                        {
                            if (double.TryParse(parts[1], NumberStyles.Float, culture, out double temperatura))
                            {
                                suma += temperatura;
                                i++;

                                double temperatureDifference = temperatura - previousTemperature;
                                temperatureDifferences.Add(temperatureDifference);

                                previousTemperature = temperatura;
                            }

                        }
                    }
                    double sredniaTemperatura = Math.Round(suma / (double)i, 2);

                    using (StreamWriter sw = new StreamWriter(plik_zapisz))
                    {
                        sw.WriteLine($"Œrednia temperatura w Polsce w latach 2000 - 2022 wynosi {sredniaTemperatura} stopni Celsjusza.");
                    }

                    var chart = new Chart();



                    chart.Dock = DockStyle.Fill;
                    chart.ChartAreas.Add("area");
                    chart.Series.Add("series");
                    chart.Series["series"].ChartType = SeriesChartType.Line;

                    for (int j = 1; j < temperatureDifferences.Count; j++)
                    {
                        chart.Series["series"].Points.AddXY(2000 + j, temperatureDifferences[j]);
                    }

                    chart.ChartAreas["area"].AxisX.Interval = 1;
                    chart.ChartAreas["area"].AxisX.Minimum = 2000;
                    chart.ChartAreas["area"].AxisX.Maximum = 2020;
                    chart.ChartAreas["area"].AxisY.Title = "Ró¿nica temperatur";
                    chart.Series["series"].BorderWidth = 3;

                    Controls.Add(chart);                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyst¹pi³ b³¹d. " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

