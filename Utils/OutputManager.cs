using System;
using System.Collections.Generic;
using HW01_2024.Models;

namespace HW01_2024.Utils;

public static class OutputManager
{
    public static void DisplayErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public static void DisplatWelcomeMessage()
    {
        Console.WriteLine("Welcome to FImon Championship! Please, choose your three FImons:");
    }

    public static void DisplayFImonsWithNumbers(List<FImon> fimons)
    {
        for (int i = 0; i < fimons.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            DisplayFImonName(fimons[i]);
            Console.WriteLine($": {fimons[i].FImonAttributes()}");
        }
    }
    
    public static void DisplaySelectedFImons(List<FImon> fimons)
    {
        Console.Write("You have chosen ");
        for (int i = 0; i < fimons.Count; i++)
        {
            DisplayFImonName(fimons[i]);
            if (i != fimons.Count - 1)
            {
                Console.Write(", ");
            }
            else
            {
                Console.WriteLine(".");
            }
        }
        Console.WriteLine("Your commands are: check, fight, info, sort, quit.");
    }

    private static ConsoleColor ColorForFImon(FImon fImon) => fImon.Type switch
    {
        FImonType.Fire => ConsoleColor.Red,
        FImonType.Sea => ConsoleColor.Blue,
        FImonType.Leaf => ConsoleColor.Green,
        _ => throw new ArgumentOutOfRangeException($"Invalid FImon type: {fImon.Type}")
    };

    private static void DisplayFImonName(FImon fImon)
    {
        Console.ForegroundColor = ColorForFImon(fImon);
        Console.Write(fImon.Name);
        Console.ResetColor();
    }
    
}