using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Drive
{
  public class LeftCrashDetector : AccidentDetector
  {
    public LeftCrashDetector(AccidentDetector next) : base(next)
    {
    }
    protected override bool Detect(Data data)
    {
      return false;
    }
  }
}