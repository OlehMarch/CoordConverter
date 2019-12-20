using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            double earthRadius = 6371;
            
            WriteData("Helena", earthRadius, 46.586510, -112.024106, 1.181);
            WriteData("Odessa", earthRadius, 31.843255, -102.369538, 0.884);
            WriteData("Washington", earthRadius, 38.900935, -77.035462, 0.02);
            WriteData("Salem", earthRadius, 44.923752, -123.042361, 0.047);
            WriteData("San Francisco", earthRadius, 37.768081, -122.428166, 0.282);
            WriteData("New York", earthRadius, 40.675673, -73.943607, 0.01);
            WriteData("Tallinn", earthRadius, 59.431826, 24.751452, 0.01);
            WriteData("Riga", earthRadius, 56.942567, 24.093858, 0.007);
            WriteData("Praha", earthRadius, 50.066638, 14.427852, 0.399);
            WriteData("Köln", earthRadius, 50.934123, 6.957407, 0.075);

            Console.ReadKey();
        }

        private static void WriteData(string cityName, double radius, double tetta, double fi, double heightOverSeaLevel)
        {
            Console.WriteLine(cityName);
            Console.WriteLine("Origin Radius: " + radius);
            Console.WriteLine("Origin Tetta: " + tetta);
            Console.WriteLine("Origin Fi: " + fi);
            Console.WriteLine("Origin Height: " + heightOverSeaLevel);
            Console.WriteLine("--- - --- - ---");
            ToCartesian cart = new ToCartesian(radius, tetta, fi, heightOverSeaLevel);
            Console.WriteLine(cart.ToString());
            Console.WriteLine("--- - --- - ---");
            ToSpherical sphr = new ToSpherical(cart.X, cart.Y, cart.Z);
            Console.WriteLine(sphr.ToString() + Environment.NewLine);
        }
    }

    class ToCartesian
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public ToCartesian(double radius, double tetta, double fi, double heightOverSeaLevel)
        {
            PerformCalculations(radius + heightOverSeaLevel, tetta, fi);
        }

        private void PerformCalculations(double r, double tetta, double fi)
        {
            tetta *= Math.PI / 180;
            fi *= Math.PI / 180;

            X = r * Math.Sin(tetta) * Math.Cos(fi);
            Y = r * Math.Sin(tetta) * Math.Sin(fi);
            Z = r * Math.Cos(tetta);
        }

        public override string ToString()
        {
            var result = string.Empty;
            result += "X: " + X + Environment.NewLine;
            result += "Y: " + Y + Environment.NewLine;
            result += "Z: " + Z;
            return result;
        }
    }

    class ToSpherical
    {
        public double R { get; private set; }
        public double Tetta { get; private set; }
        public double Fi { get; private set; }

        public ToSpherical(double x, double y, double z)
        {
            PerformCalculations(x, y, z);
        }

        private void PerformCalculations(double x, double y, double z)
        {
            R = Math.Sqrt(x * x + y * y + z * z);
            Tetta = Math.Atan(Math.Sqrt(x * x + y * y) / z);
            Fi = Math.Atan(y / x);

            Tetta *= 57.2957795;
            Fi *= 57.2957795;
            
            if (x < 0)
            {
                Fi -= 180;
            }
        }

        public override string ToString()
        {
            var result = string.Empty;
            result += "R: " + R + Environment.NewLine;
            result += "Tetta: " + Tetta + Environment.NewLine;
            result += "Fi: " + Fi;
            return result;
        }
    }
}
