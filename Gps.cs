using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Drive
{
  public class Gps
  {
    private readonly Queue<Coordenade> Coordenades;
    public Gps(string sourceFile)
    {
      Coordenades = new Queue<Coordenade>(JsonConvert.DeserializeObject<List<Coordenade>>(File.ReadAllText(sourceFile)));
    }
    public Coordenade Get()
    {
      if(Coordenades.Any())
        return Coordenades.Dequeue();
      return null;
    }
  }
}