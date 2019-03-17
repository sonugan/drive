using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Drive
{
  public abstract class AccidentDetector
  {
    public AccidentDetector(AccidentDetector next)
    {
      Next = next;
    }
    public bool IsAnAccident(Data data)
    {
      return IsAccidentSpeed(data) 
        && IsAccidentDeceleration(data) 
        && (Detect(data) || (Next != null && Next.IsAnAccident(data)));
    }
    protected abstract bool Detect(Data data);
    
    private AccidentDetector Next { get; set; }
    private const double MinAccidentSpeed = 23; //Velocidad minima que se considera para que sea un accidente (km/h)
    private const double MinAccidentDecelerationModule = 5; //cualquier desaceleración mayor o igual será considerada un potencial accidente
    
    private bool IsAccidentSpeed(Data data)
    {
      System.Console.WriteLine($"Speed {data.GetCurrentSpeed()}");
      return data.GetCurrentSpeed() > MinAccidentSpeed;
    }
    private bool IsAccidentDeceleration(Data data)
    {
      System.Console.WriteLine($"Acceleration {data.GetCurrentAcceleration()}");
      return data.GetCurrentAcceleration() > MinAccidentDecelerationModule;
    }
  }
}