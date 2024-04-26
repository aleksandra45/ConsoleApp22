using System;

public interface ILogger
{
    void Log(string message);
    void LogError(string message);
}

public interface ISum
{
    int Sum(int a, int b);
}

class Calculator : ISum
{
    private readonly ILogger logger;

    public Calculator(ILogger logger)
    {
        this.logger = logger;
    }

    public int Sum(int a, int b)
    {
        logger.Log("Выполняется сложение чисел " + a + " и " + b);
        return a + b;
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine("\u001b[34m" + message);
    }

    public void LogError(string errorMessage)
    {
        Console.WriteLine("\u001b[31m" + errorMessage);
    }
}

class Program
{
    static void Main(string[] args)
    {
        ILogger logger = new ConsoleLogger();
        Calculator calculator = new Calculator(logger);
        try
        {
            Console.WriteLine("Введите а");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введите b");
            int b = Convert.ToInt32(Console.ReadLine());

            int result = calculator.Sum(a, b);
            Console.WriteLine("Сумма чисел {0} и {1} равна {2}", a, b, result);
        }
        catch (FormatException)
        {
            logger.LogError("Ошибка формата ввода числа");
        }
        finally
        {
            Console.WriteLine("Завершение программы");
        }
    }
}