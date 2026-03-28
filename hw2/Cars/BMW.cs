using hw2.Abstract;
using hw2.Interfaces;

namespace hw2.Cars
{
    public class BMW : ACar, IMechanical, IManual
    {
        public BMW()
        {
            seats = 5;
            engine = "gasoline engine";
            transmission = "manual transmission";
        }

        public string EngineType()
        {
            return "Internal combustion engine";
        }

        public string Transmission()
        {
            return "Manual";
        }

        public override string GetDescription()
        {
            return $"BMW: {engine} with {transmission}, {seats} seats";
        }
    }
}
