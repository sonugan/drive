using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Drive
{
  public class RightCrashDetector : AccidentDetector
  {
    public RightCrashDetector(AccidentDetector next) : base(next)
    {
    }
    
    protected override bool Detect(Data data)
    {
      return data.GetCurrentDirectionAngle() >= MinAccidentAngle;
    }

    private const double MinAccidentAngle = 45;
  }
}