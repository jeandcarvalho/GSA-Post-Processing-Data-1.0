using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FfmpegApp.Dominio;
using GMap.NET.MapProviders;
using System.Threading;
using System.Diagnostics;
using GMap.NET;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using GMap.NET.WindowsForms;
using FfmpegApp.Class;
using System.Reflection.Emit;
using System.Reflection;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;

namespace FfmpegApp.Forms
{
    public partial class FormGSA : Form
    {
        int Bar=0;
        public string CsvFile, LogFile, VideoFile;
        List<DateTime> ListaIni = new List<DateTime>();
        List<DateTime> ListaFinal = new List<DateTime>();
        public List<DadosCondicoes> Condicoes = new List<DadosCondicoes>();
        public List<DadosCondicoes> CondicoesDef = new List<DadosCondicoes>();


        public FormGSA()
        {
            InitializeComponent();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            textBox1.ReadOnly = true;
            textBox1.Enabled = false;
        }


        public void button1_Click(object sender, EventArgs e)                           // acha todos os dados automaticamente pela pasta selecionada
        {

            try
            {
                folderdataTxt.Text = new Directory_Handler().Directory_Finder();

                VideoFile = new Directory_Handler().SearchFile(folderdataTxt.Text, ".mp4");

                CsvFile = new Directory_Handler().SearchFile(folderdataTxt.Text, ".csv");

                LogFile = new Directory_Handler().SearchFile(folderdataTxt.Text, ".log");

                if (LogFile != null)
                {
                    string[] logLines = System.IO.File.ReadAllLines(LogFile);
                    new Data_Functions().SaveData(Condicoes, LogFile);
                }
                else
                {
                    MessageBox.Show("Error: Log File not found!");
                }

                map.MapProvider = GMapProviders.GoogleMap;                                          //mapa settings
                map.Position = new PointLatLng(-23.526544, -46.865866);
                map.MinZoom = 5;
                map.MaxZoom = 70;
                map.Zoom = 10;
            }
            catch(Exception ex)
           
            {
                MessageBox.Show(ex.Message);
            }
           
        }


        public void button2_Click(object sender, EventArgs e)
        {
            try
            {
                outputfolderTxt.Text = new Directory_Handler().Directory_Finder();
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }

       
        public void button4_Click(object sender, EventArgs e)                  
        {
            try
            {
                button4.Enabled = false;
                if (comboBox1.Text != string.Empty && comboBox2.Text != string.Empty &&
                    comboBox3.Text != string.Empty && comboBox4.Text != string.Empty &&                       // verifica se todos os campos foram preenchidos
                    comboBox7.Text != string.Empty && comboBox7.Text != string.Empty &&
                    comboBox8.Text != string.Empty && comboBox11.Text != string.Empty)
                {
                    var resultado = from Condicoes in Condicoes
                                    where Condicoes.RoadType == comboBox1.Text &&
                                    Condicoes.RoadConditions == comboBox2.Text &&
                                    Condicoes.RoadNumbers == comboBox3.Text &&
                                    Condicoes.Traffic == comboBox4.Text &&
                                    Condicoes.Weather == comboBox7.Text &&
                                    Condicoes.Visibility == comboBox8.Text &&
                                    Condicoes.Driver == comboBox11.Text
                                    select Condicoes; //&& curves e speed 

                    CondicoesDef = resultado.ToList();
                }
                else
                {
                    MessageBox.Show("Preencha todos os campos!");
                }

                new Data_Functions().FindParts(CondicoesDef, ListaIni, ListaFinal);
                textBox1.Text = Convert.ToString(ListaIni.Count);
            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();

            Application.Restart();
        }

        public void button3_Click(object sender, EventArgs e)                                                                       // Cortar trechos de video
        {
            try
            {
                button3.Enabled = false;

                progressBar1.Step = 50;

                progressBar1.PerformStep();

                new Data_Functions().CutParts(Condicoes, ListaIni, ListaFinal, VideoFile, outputfolderTxt.Text);

                Thread.Sleep(1000);

                progressBar1.PerformStep();

                new User_Info().InfoStop();

                progressBar1.PerformStep();

                Application.Exit();

                Application.Restart();

            }
            catch (Exception ex)

            {
                MessageBox.Show(ex.Message);
            }
        }

      

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
    

