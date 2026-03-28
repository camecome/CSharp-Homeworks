using System;
using hw2.Cars;

namespace hw2
{
    public static class CarFactory
    {
        public static ICar CreateCar(CarType type)
        {
            return type switch
            {
                CarType.Tesla => new Tesla(),
                CarType.BMW => new BMW(),
                _ => throw new ArgumentException("Unknown car type")
            };
        }
    }
}
