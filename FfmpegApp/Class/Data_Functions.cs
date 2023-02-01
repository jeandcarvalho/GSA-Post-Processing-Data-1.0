using FfmpegApp.Dominio;
using MediaToolkit.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FfmpegApp.Class
{
    public class Data_Functions
    {

        public Data_Functions()
        {

        }



        public void SaveData(List<DadosCondicoes> Condicoes, string LogFile)
        {
            string[] logLines = System.IO.File.ReadAllLines(LogFile);
            for (int i = 0; i < logLines.Length; i++)
            {
                DadosCondicoes dc = new DadosCondicoes(logLines[i]);
                Condicoes.Add(dc);
            }
        }


        public void FindParts(List<DadosCondicoes> CondicoesDef, List<DateTime> ListaIni, List<DateTime> ListaFinal)
        {

            bool Grav = false;

            for (int i = 0; i < (CondicoesDef.Count - 1); i++)
            {
                var dt = (CondicoesDef[i + 1].TimeStemp) - (CondicoesDef[i].TimeStemp);

                if (Grav == false)
                {
                    ListaIni.Add(CondicoesDef[i].TimeStemp);
                    Grav = true;
                }

                if (Grav == true && dt != TimeSpan.FromSeconds(1))
                {
                    ListaFinal.Add(CondicoesDef[i].TimeStemp);
                    Grav = false;
                }

                if (Grav == true && CondicoesDef.Count == (i + 2))
                {
                    ListaFinal.Add(CondicoesDef[i].TimeStemp);
                }
            }
        }

        public void CutParts(List<DadosCondicoes> Condicoes, List<DateTime> ListaIni, List<DateTime> ListaFinal, string VideoFile, string outputfolder)
        {

            for (int k = 0; k < ListaIni.Count; k++)
            {
                DateTime StartVideo = (Condicoes[0].TimeStemp);
                DateTime IniCut = (ListaIni[k]);
                DateTime FinCut = (ListaFinal[k]);

                TimeSpan InicioSeg = IniCut - StartVideo;
                TimeSpan FinalSeg = FinCut - IniCut;

                int start = (int)InicioSeg.TotalSeconds;
                int final = (int)FinalSeg.TotalSeconds;

                int InicioSegundos = Convert.ToInt16(start);
                int FimSegundos = Convert.ToInt16(final);

                string nameCut = "Cut " + k;

                string outputFilename = $@"{outputfolder}\{nameCut}.mp4";
                string command = $"-i \"{VideoFile}\" -ss {InicioSegundos} -t {FimSegundos} \"{outputFilename}\"";

                FfmpegHandler.ExecuteFFMpeg(command);
            }
        }
    }
}
