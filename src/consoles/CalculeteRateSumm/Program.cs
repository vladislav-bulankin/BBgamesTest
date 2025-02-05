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
            Console.WriteLine($"Сумма очков {i + 1} команды равна ");
            Console.ReadLine();
        }
        int max1, max2, max3;
        max1 = FindMax(arr);
        int count = CountNumm(arr, max1);
        if (count > 2) {
            max3 = max2 = max1;
        } else if (count > 1) {
            max2 = max1;
            max3 = FindNextMax(arr, max2);
        } else {
            max2 = FindNextMax(arr, max1);
            count = CountNumm(arr, max2);
            if (count > 1) {
                max3 = max2;
            } else {
                max3 = FindNextMax(arr, max2);
            }
        }
        int sum = arr[max1] + arr[max2] + arr[max3];
        Console.WriteLine($"Сумма очков трёх команд, занявших первые три места равна {sum}");
        Console.Write("Нажмите любую клавишу");
        Console.ReadKey();
    }

    private static int FindNextMax (int[] arr, int max) {
        int next = 0;
        for (int i = 0; i < arr.Length; i++) {
            if (arr[i] > arr[next] && arr[i] < arr[max]) {
                next = i;
            }else if (arr[next] == arr[max]) {
                next = i;
            }
        }
        return next;
    }

    private static int CountNumm (int[] arr, int numm) { 
        int count = 0;
        for (int i = 0; i < arr.Length; i++) {
            if (arr[i] == arr[numm]) {
                count++;
            }
        }
        return count;
    }

    private static int FindMax (int[] arr) {
        int x = 0;
        for (int i = 0; i < arr.Length; i++) {
            if(arr[i] > arr[x]) { 
                x = i; 
            }
        }
        return x;
    }
}
