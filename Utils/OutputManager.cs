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
    
    public static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }
    
    public static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to FImon Championship! Please, choose your three FImons:");
    }
    
    public static void DisplayQuitMessage()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n\nOops! You just activated the self-destruct sequence. Prepare for explosion in 3");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("2");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("1");
        System.Threading.Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Just kidding! Thanks for playing. Have a great day!");
    }
    
    public static void DisplayWinnerMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Congratulations! You have won the FImon Championship!");
        Console.ResetColor();
    }

    public static void DisplayFImonsWithNumbers(List<FImon> fimons)
    {
        for (int i = 0; i < fimons.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            DisplayFImon(fimons[i]);
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

    public static void DisplayInfo(int roundsWon, Trainer player)
    {
        Console.WriteLine($"Battles won: {roundsWon}");
        Console.WriteLine("Your FImons:");
        foreach (FImon fImon in player.FImons)
        {
            DisplayFImon(fImon);
        }
    }
    
    public static void DisplayEnemyInfo(Trainer enemy)
    {
        Console.WriteLine("The next trainer has these FImons:");
        foreach (FImon fImon in enemy.FImons)
        {
            DisplayFImon(fImon);
        }
    }
    
    public static void DisplaySortMessage(Trainer player)
    {
        Console.WriteLine("Choose the order:");
        DisplayFImonsWithNumbers(player.FImons);
    }
    
    public static void DisplayAttack(FImon attacker, FImon defender, int damage)
    {
        DisplayFImonName(attacker);
        Console.Write(" attacks ");
        DisplayFImonName(defender);
        Console.WriteLine($" for {damage} damage. ");
        DisplayFImonName(defender);
        if (defender.CurrentHp <= 0)
        {
            Console.WriteLine(" has fainted.");
            return;
        }
        Console.WriteLine($" has {defender.CurrentHp} HP left.");
        System.Threading.Thread.Sleep(1000);
    }
    
    public static void DisplayWinningFImom(FImon fImon)
    {
        DisplayFImonName(fImon);
        Console.WriteLine(" wins!");
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
    
    private static void DisplayFImon(FImon fimon)
    {
        DisplayFImonName(fimon);
        Console.WriteLine($": {fimon.FImonAttributes()}");
    }
    
}