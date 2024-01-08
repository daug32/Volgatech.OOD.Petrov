using System.Text;
using Lab1.Models;
using Lab1.Tasks;
using Lab1.Tasks.Parsers;
using Libs.Extensions;

namespace Lab1;

/*
 
* Лабораторная работа №1.
** Цель лабораторной работы: продемонстрировать знания ООП и С++. Архитектура программного проекта должен демонстрировать механизмы наследования и полиморфизма.
Разработать программный проект,  выполняющий визуализацию и простейшие операции (расчет площади и периметра) для геометрических фигур: 
треугольника, прямоугольника, окружности. 
Для визуализации геометрических фигур использовать возможности библиотеки SFML.
Иерархия разрабатываемых классов должна реализовывать шаблон проектирования декоратор  над классами CircleShape, RectangleShape и ConvexShape библиотеки SFML.
** Входные и выходные данные: текстовые файлы. 
*** Пример входных данных: input.txt
TRIANGLE: P1=100,100; P2=200,200; P3:150, 150
RECTANGLE: P1=200,200; P2=300,300;
CIRCLE: C=100,100; R=50
*** Пример результата: output.txt
TRIANGLE: P=400; S=600
RECTANGLE: P=200; S=800
CIRCLE: P=300; S=500

* Лабораторная работа №2.
Выполнить рефакторинг лабораторной работы №1, используя паттерны «Фабричный метод» и Синглтон 
(Для каждого типа фигур создать свой класс- Creator, каждый из которых является Синглтоном).

* Лабораторная работа № 3.
Выполнить рефакторинг лабораторной работы №2 (из программы на оценку 3), реализуя печать площади и периметра,
используя паттерн Посетитель.
 */

public class Program
{
    public static void Main()
    {
        string? inputFile = AskForInputFilePath();
        if (inputFile is null)
        {
            return;
        }

        string? outputFile = AskForOutputFile();
        if (outputFile is null)
        {
            return;
        }

        TaskInput taskData;
        try
        {
            taskData = TaskInputParser.ParseFromFile(inputFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        var stringBuilder = new StringBuilder();
        foreach (ISurface surface in taskData.Surfaces)
        {
            stringBuilder.AppendLine(surface.GetSurfaceInfo());
        }

        File.WriteAllText(outputFile, stringBuilder.ToString());

        Console.WriteLine("Completed successfully");
    }

    private static string? AskForInputFilePath()
    {
        Console.WriteLine("Enter path to the input file:");

        string? path = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Path to the input file was not specified");
            return null;
        }

        path = Path.GetFullPath(path);

        if (!File.Exists(path))
        {
            Console.WriteLine($"Input file was not found. Path: {path}");
            return null;
        }

        return path;
    }

    private static string? AskForOutputFile()
    {
        Console.WriteLine("Enter path to the output file");

        string? path = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Path to the output file was not specified");
            return null;
        }

        path = Path.GetFullPath(path);

        return path;
    }
}