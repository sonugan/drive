using System;

namespace Drive
{
  public class Coordenade : Vector
  {
    public DateTime Timestamp { get; set; }

    //https://www.mkompf.com/gps/distcalc.html
    //En kilometros
    //Valido para distancias pequeñas
    public override double Distance(Vector other)
    {
      var lat = (X + other.X) / 2 * OneGradeRadians;
      var dx = LatitudeConst * Math.Cos(lat) * (Y - other.Y);
      var dy = LatitudeConst * (X + other.X);
      return Math.Sqrt(dx * dx + dy * dy);
    }

    //The average distance between two meridians in our latitudes
    private const double LongitudeConst = 71.5;

    //The distance between two circles of latitude in km
    private const double LatitudeConst = 111.3;

    //1° = π/180 rad ≈ 0.01745
    private const double OneGradeRadians = 0.01745;
  }
}
// function distance(lat1, lon1, lat2, lon2){
//     var lat = (lat1 + lat2) / 2 * 0.01745;
//     var dx = 111.3 * Math.cos(lat) * (lon1 - lon2);
//     var dy = 111.3 * (lat1 - lat2);
//     return Math.sqrt(dx * dx + dy * dy);
// }