using System;
using System.Threading;

namespace Drive
{
    class Program
    {
        //https://www.researchgate.net/publication/263351551_Kalman_filtered_GPS_accelerometerbased_accident_detection_and_location_system_A_low-cost_approach
        //https://gist.github.com/GLeBaTi/011f338e037124553217abfda531d1e7
        //https://www.researchgate.net/publication/286508977_Integrated_Vehicle_Accident_Detection_and_Location_System
        //https://scholar.sun.ac.za/bitstream/handle/10019.1/86105/zeeman_combining_2013.pdf?sequence=1&isAllowed=y
        //https://ijltemas.in/DigitalLibrary/Vol.7Issue4/167-170.pdf
        //https://upcommons.upc.edu/bitstream/handle/2099.1/20910/Final%20report%20Automotive%20Events%20Detection%20using%20MEMS%20accelerometers%20Lorena%20San%20Vicente%20bis.pdf?sequence=1&isAllowed=y
        static void Main(string[] args)
        {
            var gps = new Gps("cases/accidents/frontal-crash-gps.json");
            var acelerometer = new Acelerometer("cases/accidents/frontal-crash-accel.json");
            var accidentDetector = new RightCrashDetector(null);

            var coordenade = gps.Get();
            var acceleration = acelerometer.Get();
            var data = new Data();
            while(coordenade != null && acceleration != null)
            {
                data.AddCoordenade(coordenade);
                data.AddAcceleration(acceleration);
                Console.WriteLine(coordenade.Timestamp);
                Console.WriteLine(accidentDetector.IsAnAccident(data));

                Thread.Sleep(1000); //Tomo valores cada .5 segundos
                coordenade = gps.Get();
                acceleration = acelerometer.Get();
            }
        }
    }
}
