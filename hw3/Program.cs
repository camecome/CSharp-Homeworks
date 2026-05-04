using System;
using System.Collections.Generic;

public interface IEntity
{
    int Id { get; }
}

public class Repository<T> where T : IEntity
{
    private readonly Dictionary<int, T> _items = new Dictionary<int, T>();

    public int Count
    {
        get { return _items.Count; }
    }

    public void Add(T item)
    {
        if (_items.ContainsKey(item.Id))
        {
            throw new InvalidOperationException($"Элемент с Id = {item.Id} уже существует");
        }

        _items[item.Id] = item;
    }

    public bool Remove(int id)
    {
        return _items.Remove(id);
    }

    public T? GetById(int id)
    {
        if (_items.ContainsKey(id))
        {
            return _items[id];
        }

        return default;
    }

    public IReadOnlyList<T> GetAll()
    {
        List<T> result = new List<T>();

        foreach (var pair in _items)
        {
            result.Add(pair.Value);
        }

        return result;
    }

    public IReadOnlyList<T> Find(Predicate<T> predicate)
    {
        List<T> result = new List<T>();

        foreach (var pair in _items)
        {
            if (predicate(pair.Value))
            {
                result.Add(pair.Value);
            }
        }

        return result;
    }
}

public class Product : IEntity
{
    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"Product: Id={Id}, Name={Name}, Price={Price}";
    }
}

public class User : IEntity
{
    public int Id { get; }
    public string Name { get; }

    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return $"User: Id={Id}, Name={Name}";
    }
}

public static class CollectionUtils
{
    public static List<T> Distinct<T>(List<T> source)
    {
        List<T> result = new List<T>();
        HashSet<T> seen = new HashSet<T>();

        foreach (T item in source)
        {
            if (!seen.Contains(item))
            {
                seen.Add(item);
                result.Add(item);
            }
        }

        return result;
    }

    public static Dictionary<TKey, List<TValue>> GroupBy<TValue, TKey>(
        List<TValue> source,
        Func<TValue, TKey> keySelector) where TKey : notnull
    {
        Dictionary<TKey, List<TValue>> result = new Dictionary<TKey, List<TValue>>();

        foreach (TValue item in source)
        {
            TKey key = keySelector(item);

            if (!result.ContainsKey(key))
            {
                result[key] = new List<TValue>();
            }

            result[key].Add(item);
        }

        return result;
    }

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        Dictionary<TKey, TValue> first,
        Dictionary<TKey, TValue> second,
        Func<TValue, TValue, TValue> conflictResolver) where TKey : notnull
    {
        Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

        foreach (var pair in first)
        {
            result[pair.Key] = pair.Value;
        }

        foreach (var pair in second)
        {
            if (result.ContainsKey(pair.Key))
            {
                result[pair.Key] = conflictResolver(result[pair.Key], pair.Value);
            }
            else
            {
                result[pair.Key] = pair.Value;
            }
        }

        return result;
    }

    public static T MaxBy<T, TKey>(List<T> source, Func<T, TKey> selector)
        where TKey : IComparable<TKey>
    {
        if (source.Count == 0)
        {
            throw new InvalidOperationException("Коллекция пуста");
        }

        T maxItem = source[0];
        TKey maxKey = selector(source[0]);

        for (int i = 1; i < source.Count; i++)
        {
            TKey currentKey = selector(source[i]);

            if (currentKey.CompareTo(maxKey) > 0)
            {
                maxKey = currentKey;
                maxItem = source[i];
            }
        }

        return maxItem;
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Задача 1: Repository<T> ===");

        Repository<Product> productRepository = new Repository<Product>();

        productRepository.Add(new Product(1, "Laptop", 1500));
        productRepository.Add(new Product(2, "Mouse", 500));
        productRepository.Add(new Product(3, "Phone", 1200));

        Console.WriteLine("Все продукты:");
        foreach (Product product in productRepository.GetAll())
        {
            Console.WriteLine(product);
        }

        Console.WriteLine();

        Product? foundProduct = productRepository.GetById(1);
        Console.WriteLine($"Поиск продукта по Id=1: {foundProduct}");

        Console.WriteLine();

        Console.WriteLine("Продукты дороже 1000:");
        IReadOnlyList<Product> expensiveProducts =
            productRepository.Find(product => product.Price > 1000);

        foreach (Product product in expensiveProducts)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine();

        try
        {
            productRepository.Add(new Product(1, "Duplicate laptop", 2000));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ошибка при добавлении дубликата: {ex.Message}");
        }

        Console.WriteLine();

        Repository<User> userRepository = new Repository<User>();

        userRepository.Add(new User(1, "Alice"));
        userRepository.Add(new User(2, "Bob"));

        Console.WriteLine("Все пользователи:");
        foreach (User user in userRepository.GetAll())
        {
            Console.WriteLine(user);
        }

        Console.WriteLine();

        Console.WriteLine("=== Задача 2: CollectionUtils ===");

        List<int> numbers = new List<int> { 1, 2, 2, 3, 1, 4 };
        List<int> distinctNumbers = CollectionUtils.Distinct(numbers);

        Console.WriteLine("Distinct для int:");
        foreach (int number in distinctNumbers)
        {
            Console.Write(number + " ");
        }

        Console.WriteLine();
        Console.WriteLine();

        List<string> strings = new List<string> { "cat", "dog", "cat", "bird", "dog" };
        List<string> distinctStrings = CollectionUtils.Distinct(strings);

        Console.WriteLine("Distinct для string:");
        foreach (string str in distinctStrings)
        {
            Console.Write(str + " ");
        }

        Console.WriteLine();
        Console.WriteLine();

        List<string> words = new List<string> { "one", "two", "three", "four", "five", "six" };
        Dictionary<int, List<string>> groupedWords =
            CollectionUtils.GroupBy(words, word => word.Length);

        Console.WriteLine("GroupBy по длине слова:");
        foreach (var pair in groupedWords)
        {
            Console.Write($"Длина {pair.Key}: ");

            foreach (string word in pair.Value)
            {
                Console.Write(word + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();

        Dictionary<string, int> first = new Dictionary<string, int>
        {
            ["apple"] = 2,
            ["banana"] = 3
        };

        Dictionary<string, int> second = new Dictionary<string, int>
        {
            ["banana"] = 4,
            ["orange"] = 5
        };

        Dictionary<string, int> merged =
            CollectionUtils.Merge(first, second, (a, b) => a + b);

        Console.WriteLine("Merge словарей:");
        foreach (var pair in merged)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        Console.WriteLine();

        List<Product> products = new List<Product>
        {
            new Product(1, "Laptop", 1500),
            new Product(2, "Mouse", 500),
            new Product(3, "Phone", 1200)
        };

        Product mostExpensive =
            CollectionUtils.MaxBy(products, product => product.Price);

        Console.WriteLine($"Самый дорогой товар: {mostExpensive}");
    }
}
