namespace FindPositivElements;
internal class Program {
    /*
     * Задан массив из n целых чисел. Вывести только его положительные элементы, в 
        обратном порядке 
     * не совсем понятно что тут от меня ожидали может кучу сложных циклов
     * */
    static void Main(string[] args) {
        Console.Write("Введите размерность массива ");
        int n = Convert.ToInt32(Console.ReadLine());
        int[] arr = new int[n];
        for (int i = 0; i < n; i++) {
            Console.Write($"элемент номер {i + 1} = ");
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }
        var list = arr.Where(x => x > 0).Reverse().ToList();
        Console.Write("положительные элементы в обратном порядке ");
        foreach (var item in list) { 
            Console.Write($"{item.ToString()} ");
        }
    }
}
