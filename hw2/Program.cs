using System;
using hw2;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("Введите марку автомобиля или done: ");

            string? input = Console.ReadLine();

            if (input == null)
                continue;

            if (input.ToLower() == "done")
                break;

            if (Enum.TryParse(input, true, out CarType type))
            {
                ICar car = CarFactory.CreateCar(type);
                Console.WriteLine(car.GetDescription());
            }
            else
            {
                Console.WriteLine("Неизвестная марка");
            }
        }
    }
}
