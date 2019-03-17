using System.Collections.Generic;
using System.Linq;

namespace Drive
{
  public class Data
  {
    public Data()
    {
      coordenades = new Queue<Coordenade>();
      accelerations = new Queue<Acceleration>();
    }

    public void AddCoordenade(Coordenade c)
    {
      if(coordenades.Any())
      {
        PrevoiusCoordenade = coordenades.TakeLast(1).Single();
      }
      coordenades.Enqueue(c);
    }

    public void AddAcceleration(Acceleration a)
    {
      if(accelerations.Any())
      {
        PrevoiusAcceleration = accelerations.TakeLast(1).Single();
      }
      accelerations.Enqueue(a);
    }

    ///Retorna la velocidad km/h 
    public double GetCurrentSpeed()
    {
      if(coordenades.Count() >= 2)
      {
        var current = coordenades.TakeLast(1).Single();
        return PrevoiusCoordenade.Distance(current) / ((current.Timestamp - PrevoiusCoordenade.Timestamp).Seconds*60);
      }
      return 0;
    }

    //Retorna el ángulo de direccón actual del vehículo en base a la posición anterior
    public double GetCurrentDirectionAngle()
    {
      if(accelerations.Count() >= 2)
      {
        var current = accelerations.TakeLast(1).Single();
        return PrevoiusAcceleration.Angle(current);
      }
      return 0;
    }

    public double GetCurrentAcceleration()
    {
      System.Console.WriteLine($"Acceleration1 {accelerations?.Take(1)?.FirstOrDefault()?.Module()}");
      return accelerations?.Take(1)?.FirstOrDefault()?.Module() ?? 0;
    }

    private readonly Queue<Coordenade> coordenades;
    private readonly Queue<Acceleration> accelerations;
    private Coordenade PrevoiusCoordenade;
    private Acceleration PrevoiusAcceleration;
  }
}