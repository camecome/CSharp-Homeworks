using System;

while (true)
{
    Console.Write("Введите первое число (или q для выхода): ");
    string input1 = Console.ReadLine()!;

    if (string.IsNullOrWhiteSpace(input1))
    {
        Console.WriteLine("Вы ничего не ввели.");
        continue;
    }

    if (input1 == "q")
    {
        Console.WriteLine("Программа завершена.");
        break;
    }

    double a = Convert.ToDouble(input1);

    Console.Write("Введите второе число (или q для выхода): ");
    string input2 = Console.ReadLine()!;

    if (string.IsNullOrWhiteSpace(input2))
    {
        Console.WriteLine("Вы ничего не ввели.");
        continue;
    }

    if (input2 == "q")
    {
        Console.WriteLine("Программа завершена.");
        break;
    }

    double b = Convert.ToDouble(input2);

    Console.Write("Операция (+ - * /): ");
    string op = Console.ReadLine()!;

    if (string.IsNullOrWhiteSpace(op))
    {
        Console.WriteLine("Операция не введена.");
        continue;
    }

    double result = 0;

    if (op == "+") result = a + b;
    else if (op == "-") result = a - b;
    else if (op == "*") result = a * b;
    else if (op == "/") result = a / b;
    else
    {
        Console.WriteLine("Неизвестная операция.");
        continue;
    }

    Console.WriteLine("Результат: " + result);
    Console.WriteLine();
}
