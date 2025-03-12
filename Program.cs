using System;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.VisualBasic;
using System.IO;
using System.Threading;
using Serilog;

// LinQ
    class Employee  {
        public string Name { get; set; }
        public int YearJoined { get; set; }
    }

    class Transaction
    {
        public string TransactionId { get; set; }
        public DateTime Date { get; set; }
    }

    class Karyawan {
        public int Id { get; set; }
        public string Nama { get; set; }
    }

    class indonesia {
        public int id { get; set; }
        public string koruptor { get; set; }
    }

    public class DatabaseService {
        public async Task<string> GetDataAsync() {
            await Task.Delay(2000);
            return "database result";
        }
    }

    public class Logger
    {
        public async Task LogAsync(string message)
        {
            try
            {
                await Task.Delay(1000);
                Console.WriteLine($"Log: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging failed: {ex.Message}");
            }
        }
    }

    public class ReportGenerator
    {
        public async Task GenerateReportAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 5; i++)
            {
                cancellationToken.ThrowIfCancellationRequested(); // Cek jika ada permintaan pembatalan
                Console.WriteLine($"Generating part {i + 1}...");
                await Task.Delay(2000, cancellationToken); // Task.Delay bisa dibatalkan
            }
        }
    }

class Program
{
    // Basic Sintax Debugging Test
    // 1. Buat function yang menerima integer N dan mengembalikan jumlah digit dalam angka tersebut.
    static int CountDigits(int N)
    {
        return N.ToString().Length;
    }

    // 2. Buat function yang menerima suhu dalam Celcius dan mengonversinya ke Fahrenheit dan Kelvin dalam bentuk tuple atau dictionary.
    static (double fahrenheit, double kelvin) ConvertTemperature(double C)
    {
        double fahrenheit = (C * 9 / 5) + 32;
        double kelvin = C + 275.15;
        return (fahrenheit, kelvin);
    }

    // 3. Buat function yang menerima integer N dan mengembalikan daftar semua faktor dari N.
    static List<int> getFactor(int F)
    {
        List<int> factors = new List<int>();

        for (int i = 1; i <= F; i++)
        {
            if (F % i == 0)
            {
                factors.Add(i);
            }
        }

        return factors;
    }


    // 4. Buat function yang menerima integer (maksimal 9999) dan mengembalikannya dalam format teks dalam bahasa Indonesia.
    static readonly Dictionary<int, string> angkaKata = new Dictionary<int, string>
    {
    {0, "Nol"}, {1, "Satu"}, {2, "Dua"}, {3, "Tiga"}, {4, "Empat"}, {5, "Lima"}, {6, "Enam"}, {7,"Tujuh"}, {8, "Delapan"}, {9, "Sembilan"},
    {10, "Sepuluh"}, {11, "Sebelas"}, {12, "Dua Belas"}, {13, "Tiga Belas"}, {14, "Empat Belas"}, {15, "Lima Belas"}, {16, "Enam Belas"},
    {17, "Tujuh Belas"}, {18, "Delapa Belas"}, {19, "Sembilan Belas"}, {20, "Dua Puluh"}, {30, "Tiga Puluh"}, {40, "Empat Puluh"},
    {50, "Lima Puluh"}, {60, "Enam Puluh"}, {70, "Tujuh Puluh"}, {80, "Delapan Puluh"}, {90, "Sembilan Puluh"}
    };

    static string ConvertToWords(int number)
    {
    if (number == 0) return angkaKata[0];

    string resultKata = " ";

    if (number >= 1000)
    {
        int ribuan = number / 1000;
        resultKata += (ribuan == 1 ? "Seribu" : angkaKata[ribuan] + " Ribu");
        number %= 1000;
        if (number > 0) resultKata += " ";
    }

    if (number >- 100)
    {
        int ratusan = number / 100;
        resultKata += (ratusan == 1 ? "Seratus" : angkaKata[ratusan] + " Ratus");
        number %= 100;
        if (number > 0) resultKata += " ";
    }

    if (number >= 20)
    {
        int puluhan = (number / 10) * 10;
        resultKata += angkaKata[puluhan];
        number %= 10;
        if (number > 0) resultKata += " ";
    }

    if ( number > 0 )
    {
        resultKata += angkaKata[number];
    }

    return resultKata;
    }

    // 5. Buat function yang menerima string dan mengembalikan bentuk kompresi sederhana di mana setiap huruf diikuti oleh jumlah kemunculannya jika lebih dari 1.
    static string CompressString (string inputString) {
        if (string.IsNullOrEmpty(inputString)) return inputString;

        StringBuilder compressed = new StringBuilder();
        int count = 1;

        for (int i = 0; i < inputString.Length; i++) {
            if(i + 1 < inputString.Length && inputString[i] == inputString[i + 1]) {
                count++;
            } else {
                compressed.Append(inputString[i]).Append(count);
                count = 0;
            }
        }

        return compressed.ToString();
    }


    // Basic Sintax Code Review
    // 1. Loop Tidak Efisien dalam Mencari Nilai Maksimum
    public static int FindMax(int[] nums) {
        int max = nums[0];
        for (int i = 1;i < nums.Length; i++){
            if (nums[i] > max) {
                max = nums[i];
            }
        }
        return max;
    }
    // 3. 


     static void SaveLog(string message) {
            if (string.IsNullOrEmpty(message)) {
                LogError.Warning("Pesan log kosong tidak akan disimpan");
                return;
            }

            string logFilePath = $"logs_{DateTime.Now:yyyymmdd}.log";
            int maxRetries = 3;
            int retryDelay = 1000;

            for (int attempt = 1; attempt <= maxRetries; attempt++) {
                try {
                    File.AppendAllText($"Percobaan {attempt}: Gagal menyimpan log. Error: {ex.Message}");
                    if (attempt == maxRetries) {
                        Log.Fatal("Gagal menyimpan log setelah beberapa percobaan. Log tidak akan disimpan.");
                        break;
                    }

                    Thread.Sleep(retryDelay);
                } 
            }
        }

        static void LogError(string message)
        {
            string logFileName = $"error_{DateTime.Now:yyyy-MM-dd}.log"; 

            try
            {
                using (StreamWriter logWriter = new StreamWriter(logFileName, append: true))
                {
                    logWriter.WriteLine($"{DateTime.Now:HH:mm:ss} - {message}");
                }
            }
            catch
            {
                Console.WriteLine("Gagal menulis log kesalahan.");
            }
        }

        static void SaveUser(string filePath, string data) {
            if(string.IsNullOrWhiteSpace(data)) {
                Console.WriteLine("Data tidak boleh kosong");
                return;
            }

            int maxRetries = 3; 
            int delayBetweenRetries = 2000; 

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    // Menggunakan 'using' agar StreamWriter otomatis ditutup
                    using (StreamWriter writer = new StreamWriter(filePath, append: true))
                    {
                        writer.WriteLine(data);
                    }

                    Console.WriteLine("User data saved successfully.");
                    return;
                }
                catch (UnauthorizedAccessException ex)
                {
                    LogError($"ERROR: Tidak memiliki izin untuk mengakses file: {ex.Message}");
                    Console.WriteLine("Gagal menyimpan data: Izin tidak mencukupi.");
                    return; 
                }
                catch (IOException ex)
                {
                    LogError($"ERROR: Kesalahan IO pada percobaan {attempt}: {ex.Message}");
                    Console.WriteLine($"Gagal menyimpan data, mencoba lagi... ({attempt}/{maxRetries})");

                    if (attempt < maxRetries)
                        Thread.Sleep(delayBetweenRetries); 
                        Console.WriteLine("Gagal menyimpan data setelah beberapa percobaan.");
                }
                catch (Exception ex)
                {
                    LogError($"ERROR: Kesalahan tidak terduga: {ex.Message}");
                    Console.WriteLine("Terjadi kesalahan yang tidak diketahui.");
                    return;
                }
            }
        }
 
    static void Main()
    // // debugging async
    // static async Task Main()
    {
        // 1
        Console.WriteLine(CountDigits(12345));
        
        // 2
        var result = ConvertTemperature(25);
        Console.WriteLine($"Fahrenheit : {result.fahrenheit} \nKelvin : {result.kelvin}");

        // 3
        int F = 12;
        List<int> resultFactor = getFactor(F);
        Console.WriteLine($"[{string.Join(", ", resultFactor)}]");

        // 4
        int input = 356;
        Console.WriteLine($"Angka : {input} -> Kata : \"{ConvertToWords(input)}\"");

        // 5
        string resultCompressString = CompressString("abbbcccdddd");
        Console.WriteLine(resultCompressString);


        // 1. Code review
        int[] maxNumber = [-1, -3, -4, -8, -2, -1, -3];
        Console.WriteLine(FindMax(maxNumber));

        Console.WriteLine();


        // 2. mengubah string menjadi string builder
        StringBuilder resultString = new StringBuilder();

        for (int i = 0; i < 100; i++) 
        {
            resultString.Append(i.ToString());
        }

        string finalResult = result.ToString();
        Console.WriteLine(finalResult);

        Console.WriteLine();

        // 3. 
        int[]  Numbers = {1, 2, 3, 4, 5};
        
        int total = Numbers.Sum();
        Console.WriteLine($"total = {total}");

        Console.WriteLine("");

        // Collections and LinQ debugging 
        // 1.  
        Employee[] employees= new Employee[] {
            new Employee { Name = "Agus", YearJoined = 2010},
            new Employee { Name = "Bambang", YearJoined = 2015},
            new Employee { Name = "Citra", YearJoined = 2008},
            new Employee { Name = "Dodi", YearJoined = 2012}
        };

        // Mencari pegawai dengan masa kerja terlama
        Employee PegawaiTerlama = employees.OrderBy(e => e.YearJoined).First();
        Console.WriteLine($"Pegawai dengan masa kerja terlama: {PegawaiTerlama.Name} bergabung sejak {PegawaiTerlama.YearJoined}");
        Console.WriteLine("");


        // 2. 
        Dictionary<string, double> customers = new Dictionary<string, double> {
            {"Erika", 2500000},
            {"Fajar", 5000000},
            {"Gina", 3000000},
            {"Hadi", 7000000}
        };

        // menemukan 3 pelanggan dengan total belanja terbanyak
        var topPelanggan = customers.OrderByDescending(c => c.Value).Take(3);
        Console.WriteLine("Pelanggan dengan jumlah belanja terbanyak : ");
        foreach (var customer in topPelanggan) {
            Console.WriteLine($"{customer.Key} : Rp.{customer.Value}");
        }

        Console.WriteLine("");


        // 3. 
        string[] departments = { "IT", "IT", "HR", "HR", "Financce"};
        
        Dictionary<string, int> departmentCount = departments
            .GroupBy(d => d)
            .ToDictionary(g => g.Key, g => g.Count());

        Console.WriteLine("Jumlah pegawai di setiap departemen:");
        foreach (var dept in departmentCount)
        {
            Console.WriteLine($"{dept.Key}: {dept.Value}");
        }

        Console.WriteLine("");

        // 4. 
        // Cari Produk dengan Nama Mengandung Huruf Tertentu
        string[] products = { "Keyboard", "Mouse", "Monitor", "Laptop", "Speaker" };

        var groupedProduct = products
            .GroupBy(p => p.ToLower().Contains("o"))
            .ToDictionary(g => g.Key ? "Mengandung huruf o" : "Tidak mengandung huruf o", g => g.ToList());

        foreach (var group in groupedProduct) {
            Console.WriteLine($"{group.Key} : {string.Join(", ", group.Value)}");
        };

        Console.WriteLine("");


        // 5
        List<Transaction> transactions = new List<Transaction>
        {
            new Transaction { TransactionId = "T001", Date = new DateTime(2024, 1, 5) },
            new Transaction { TransactionId = "T002", Date = new DateTime(2024, 2, 10) },
            new Transaction { TransactionId = "T003", Date = new DateTime(2024, 3, 15) },
            new Transaction { TransactionId = "T004", Date = new DateTime(2024, 4, 20) }
        };

        var lastTwoTransaction = transactions
            .OrderByDescending(t => t.Date)
            .Take(3);

        foreach (var transaction in lastTwoTransaction) {
            Console.WriteLine($"ID : {transaction.TransactionId} , Date {transaction.Date:yyyy-MM-dd}");
        }

        Console.WriteLine("");

        // code review 
        // 1. 
        Dictionary<int, Karyawan> KaryawanDict = new Dictionary<int, Karyawan> {
            { 1, new Karyawan{Id = 1, Nama = "Budi"}},
            { 2, new Karyawan{Id = 2, Nama = "Andi"}},
            { 3, new Karyawan{Id = 3, Nama = "Cindy"}}
        }; 

        if (KaryawanDict.TryGetValue(2, out Karyawan emp)) {
            Console.WriteLine(emp.Nama);
        }
        else {
            Console.WriteLine("Pegawai tidak ditemukan.");
        }

        Console.WriteLine();

        // 2. menghapus select
        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var evenNumbers = numbers.Where(n => n % 2 == 0).ToList();
        Console.WriteLine(string.Join(", ", evenNumbers));

        Console.WriteLine();

        // 3. menggunakan sum
        List<int> sales = new List<int> { 100, 200, 150, 300, 250 };

        int totalSales = sales.Sum();

        Console.WriteLine($"Total Penjualan: {totalSales}");

        // 4. menggunakan orderBy
        List<string> names = new List<string> { "Zara", "Andi", "Budi", "Cindy" };

        var sortedNames = names.OrderByDescending(n => n).ToList();

        Console.WriteLine(string.Join(", ", names));

        // 5. penggunaan var kurang cocok karna type data kurang jelas
        List<int> nmbers = new List<int> { 10, 20, 30, 40, 50 };
        IEnumerable<int> reslt = nmbers.Select(x => x * 2).Where(x => x > 50);

        foreach (int item in reslt)
        {
            Console.WriteLine(item);
        }

        // Debugging Asyncronous 
        // 1. 
        var dbService = new DatabaseService();
        Console.WriteLine("Fetching Data...");
        string rslt = await dbService.GetDataAsync();
        Console.WriteLine(rslt);

        // 2. 
        var logger = new Logger();

        // Fire-and-forget dengan exception handling
        _ = Task.Run(async () => await logger.LogAsync("Application started"));

        Console.WriteLine("Main method done");
        Task.Delay(2000).Wait();

        // 3. 
        var reportGenerator = new ReportGenerator();
        var cts = new CancellationTokenSource();

        var task = reportGenerator.GenerateReportAsync(cts.Token);

        await Task.Delay(5000);
        Console.WriteLine("User decided to cancel!");
        cts.Cancel();

        try
        {
            await task;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Report generation was cancelled.");
        }

        // exception handling debugging
        SaveUser("user_data.txt", "John Doe, johndoe@gmail.com, 25");

        //exception handling code review

        Log.Logger = new LoggerConfiguration()
            .Write.Console()
            .WriteTo.File("error.log", rollingInterval: RollingInterval.Day)
            .CreateLogger;


    }    
}
