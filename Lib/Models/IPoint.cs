namespace com.moosemorals.DWP.Models
{

    public interface IPoint
    {

        // Doubles for lat/lon is overkill, but
        // it saves a bunch of casts
        // https://xkcd.com/2170/

        public double Latitude { get; }

        public double Longitude { get; }

    }
}
