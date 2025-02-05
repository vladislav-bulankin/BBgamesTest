namespace CalculeteRateSumm;

/*
 Известна сумма очков, набранных каждой из 20 команд-участниц чемпионата по 
футболу. Определить сумму очков, набранных командами, занявшими в чемпионате 
три первых места
 */
internal class Program {
    const short q = 20;
    static void Main (string[] args) {
        int[] arr = new int[q];
        Console.WriteLine("Введите суммы очков каждой из команд");
        for (int i = 0; i < arr.Length; i++) {
            Console.Write($"Сумма очков {i + 1} команды равна: ");
            arr[i] = int.Parse(Console.ReadLine());
        }
        int max1 = int.MinValue, max2 = int.MinValue, max3 = int.MinValue;

        foreach (var score in arr) {
            if (score > max1) {
                max3 = max2;
                max2 = max1;
                max1 = score;
            } else if (score > max2 && score < max1) {
                max3 = max2;
                max2 = score;
            } else if (score > max3 && score < max2) {
                max3 = score;
            }
        }

        int sum = max1 + max2 + max3;
        Console.WriteLine($"Сумма очков трёх команд, занявших первые три места равна {sum}");
        Console.Write("Нажмите любую клавишу");
        Console.ReadKey();
    }
}
