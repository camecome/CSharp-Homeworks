using hw2.Abstract;
using hw2.Interfaces;

namespace hw2.Cars
{
    public class Tesla : ACar, IElectric, IAutomatical
    {
        public Tesla()
        {
            seats = 5;
            engine = "electric car";
            transmission = "automatic transmission";
        }

        public string ElectricFeature()
        {
            return "Runs on electricity";
        }

        public string Transmission()
        {
            return "Automatic";
        }

        public override string GetDescription()
        {
            return $"Tesla: {engine} with {transmission}, {seats} seats, Android onboard";
        }
    }
}
