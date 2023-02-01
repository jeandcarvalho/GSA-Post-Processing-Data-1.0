using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FfmpegApp.Dominio
{
    public class DadosCondicoes
    {
        public DateTime TimeStemp { get; set; }

        public string RoadType { get; set; }

        public string RoadConditions { get; set; }

        public string Visibility { get; set; }

        public string RoadNumbers { get; set; }

        public string Traffic { get; set; }

        public string DayPeriod { get; set; }

        public string Weather { get; set; }

        public string Driver { get; set; }

        public float WheelAngle { get; set; }

        public float Curves { get; set; }




        public DadosCondicoes(string rowData)
        {
            string[] data = rowData.Split(',');

            this.TimeStemp = Convert.ToDateTime(data[0]);
            this.RoadType = data[2];
            this.RoadConditions = data[4];
            this.Visibility = data[7];
            this.RoadNumbers = data[6];
            this.Traffic = data[8];
            this.DayPeriod = data[7];
            this.Weather = data[3];
            this.Driver = data[5];

            //this.WheelAngle = Convert.ToSingle(data[9]);
            //this.Curves = Convert.ToSingle(data[10]);










        }

        public override string ToString()
        {
            string str = $"{TimeStemp},{RoadType},{RoadConditions},{Visibility},{RoadNumbers},{Traffic},{DayPeriod},{Weather},{Driver}";


            return str;
        }

    }
}

