using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.IO.Path;

class Program
{
    public static void Main()
    {
        string dirPathOne = @"c:\Otus\TestDir1";
        string dirPathTwo = @"c:\Otus\TestDir2";

        //------------------------------------------------------                                                            // Задание 1
        DirectoryInfo dirOne = new DirectoryInfo(dirPathOne);
        DirectoryInfo dirTwo = new DirectoryInfo(dirPathTwo);
        CreateDirOrFile(dirOne);
        CreateDirOrFile(dirTwo);
        //------------------------------------------------------

        Console.ReadKey();

        //------------------------------------------------------                                                            // Задание 2
        CreateFiles(10, dirPathOne);
        CreateFiles(10, dirPathTwo);

        Console.ReadKey();

        //-----------------------------------------------------                                                             // Задание 3
        WriteInAllFiles(dirPathOne, false);
        WriteInAllFiles(dirPathTwo, false);

        Console.ReadKey();

        //-----------------------------------------------------                                                             // Задание 4
        WriteInAllFiles(dirPathOne, true, DateTime.Now.ToString());
        WriteInAllFiles(dirPathTwo, true, DateTime.Now.ToString());

        Console.ReadKey();

        //-----------------------------------------------------                                                             // Задание 5
        Console.WriteLine(printDataFile(ReadAllFiles(dirPathOne)));
        Console.WriteLine(printDataFile(ReadAllFiles(dirPathTwo)));
    }

    static void CreateDirOrFile(DirectoryInfo directory)                                                                    // Метод для создания директории
    {
        try
        {
            if (directory.Exists)
            {
                Console.WriteLine("Директория уже существует.");
                return;
            }

            directory.Create();
            Console.WriteLine("Директория успешно создана.");
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

    static void CreateFiles(int count, string path, string name = "File")                                                   // Метод для создания файлов
    {

        for (int i = 1; i <= count; i++)
        {

            string fileName = name + i + ".txt";
            string textFile = Combine(path, fileName);

            try
            {
                if (File.Exists(textFile))
                {
                    Console.WriteLine("Файл уже существует.");
                }
                else
                {
                    var textWriter = File.CreateText(textFile);
                    textWriter.Close();
                    Console.WriteLine("Файл успешно создан.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }

    static void WriteInAllFiles(string directoryPuth, bool rewrite)                                                         // Метод для пере-/записи в файл его имени
    {
        try
        {
            string[] allfiles = Directory.GetFiles(directoryPuth, "*.txt");
            foreach (string filename in allfiles)
            {
                using (var writer = new StreamWriter(filename, rewrite, Encoding.UTF8))
                {
                    writer.WriteLine(Path.GetFileName(filename));
                    Console.WriteLine("Файл перезаписан.");
                }
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

    static void WriteInAllFiles(string directoryPuth, bool rewrite, string data)                                            // Метод для пере-/записи в файл текст
    {
        try
        {
            string[] allfiles = Directory.GetFiles(directoryPuth, "*.txt");
            foreach (string filename in allfiles)
            {
                using (var writer = new StreamWriter(filename, rewrite, Encoding.UTF8))
                {
                    writer.WriteLine(data);
                    Console.WriteLine("Запись успешно добавлена в файл.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

    static List<string> ReadAllFiles(string directoryPuth)                                                                  // Метод чтения файлов
    {
        var result = new List<string>();
        try
        {
            string[] allfiles = Directory.GetFiles(directoryPuth, "*.txt");
            string outLine;
            foreach (string filename in allfiles)
            {
                using (var reader = new StreamReader(filename))
                {
                    outLine = $"{Path.GetFileName(filename)}: ";
                    while (!reader.EndOfStream)
                    {
                        outLine += reader.ReadLine() + " + ";
                    }
                }
                result.Add(outLine.Substring(0, outLine.Length-3));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }

        return result;
    }

    static string printDataFile(List<string> list)                                                                          // Метод вывода содержимого файлов
    {
        string output = "";
        foreach (var item in list)
        {
            output += item + "\n";
        }
        return output;
    }
}