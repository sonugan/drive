using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Drive
{
  public class Acelerometer
  {
    private readonly Queue<Acceleration> Accelerations;
    public Acelerometer(string sourceFile)
    {
      Accelerations = new Queue<Acceleration>(JsonConvert.DeserializeObject<List<Acceleration>>(File.ReadAllText(sourceFile)));
    }
    public Acceleration Get()
    {
      if(Accelerations.Any())
        return Accelerations.Dequeue();
      return null;
    }
  }
}