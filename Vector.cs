using System;

namespace Drive
{
  public class Vector 
  {
    public double X { get; set; }
    public double Y { get; set; }

    public double DotProduct(Vector other) 
    {
      return X * other.X + Y * other.Y;
    }

    public virtual double Distance(Vector other)
    {
      return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
    }

    public double Module() 
    {
      return Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));
    }

    public double Angle(Vector other) 
    {
      var angleRadians = Math.Acos(DotProduct(other) / (Module() * other.Module()));
      return (
        ((angleRadians > 0 ? angleRadians : 2 * Math.PI + angleRadians) * 360) /
        (2 * Math.PI)
      );
    }
  }
}
