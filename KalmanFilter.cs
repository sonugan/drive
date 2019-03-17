using System;

namespace Drive
{
  public class KalmanFilter
  {
    private const float MinAccuracy = 1;
    private float metersPerSecond;
    public long TimeStampMilliseconds { get; private set; }
    public double Lat { get; private set; }
    public double Lng { get; private set; }
    public double Accurancy { get { return (float)Math.Sqrt(variance); }}
    private float variance; // P matrix.  Negative means object uninitialised.  NB: units irrelevant, as long as same units used throughout

    public KalmanFilter(float metersPerSecond) 
    { 
      this.metersPerSecond = metersPerSecond; 
      variance = -1; 
    }
    public void SetState(double lat, double lng, float accuracy, long TimeStampMilliseconds)
    {
      Lat = lat; 
      Lng = lng; 
      variance = accuracy * accuracy; 
      this.TimeStampMilliseconds = TimeStampMilliseconds;
    }

    /// <summary>
    /// Kalman filter processing for lattitude and longitude
    /// </summary>
    /// <param name="latMeasurement_degrees">new measurement of lattidude</param>
    /// <param name="lngMeasurement">new measurement of longitude</param>
    /// <param name="accuracy">measurement of 1 standard deviation error in metres</param>
    /// <param name="TimeStampMilliseconds">time of measurement</param>
    /// <returns>new state</returns>
    public void Process(double latMeasurement, double lngMeasurement, float accuracy, long TimeStampMilliseconds)
    {
      if (accuracy < MinAccuracy) accuracy = MinAccuracy;
      if (variance < 0)
      {
        // if variance < 0, object is unitialised, so initialise with current values
        this.TimeStampMilliseconds = TimeStampMilliseconds;
        Lat = latMeasurement; 
        Lng = lngMeasurement; 
        variance = accuracy * accuracy;
      }
      else
      {
        // else apply Kalman filter methodology

        long TimeIncMilliseconds = TimeStampMilliseconds - this.TimeStampMilliseconds;
        if (TimeIncMilliseconds > 0)
        {
          // time has moved on, so the uncertainty in the current position increases
          variance += TimeIncMilliseconds * metersPerSecond * metersPerSecond / 1000;
          this.TimeStampMilliseconds = TimeStampMilliseconds;
          //TODO [Tracker]: USE VELOCITY INFORMATION HERE TO GET A BETTER ESTIMATE OF CURRENT POSITION
        }

        // Kalman gain matrix K = Covarariance * Inverse(Covariance + MeasurementVariance)
        // NB: because K is dimensionless, it doesn't matter that variance has different units to lat and lng
        float K = variance / (variance + accuracy * accuracy);
        // apply K
        Lat += K * (latMeasurement - Lat);
        Lng += K * (lngMeasurement - Lng);
        // new Covarariance  matrix is (IdentityMatrix - K) * Covarariance 
        variance = (1 - K) * variance;
      }
    }
  }
}