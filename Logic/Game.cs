using System;
using System.Collections.Generic;
using HW01_2024.Interfaces;
using HW01_2024.Models;

namespace HW01_2024.Logic;

public class Game : IGame
{
    private Trainer player {get; set;}
    private Trainer enemy {get; set;}
    private int roundsWon {get; set;}
    
    public void Start()
    {
        roundsWon = 0;
        player = selectFImons();
        enemy = new Trainer();
    }
    
    private Trainer selectFImons()
    {
        Console.WriteLine("Welcome to FImon Championship! Please, choose your three FImons:");
        List<FImon> fImons = new List<FImon>();
        for (int i = 0; i < 6; i++)
        {
            FImon fimon = new FImon();
            fImons.Add(fimon);
            Console.WriteLine($"{i + 1}. {fimon.ToString()}");
        }
        
        while (true) 
        {
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) 
            {
                continue;
            }
            string[] splittedInput = input.Split(" ");
            
            if (splittedInput.Length != 4 || splittedInput[0].ToLower() != "start")
            {
                Console.WriteLine("Invalid input! Enter input in the format 'start x y z' (where x, y, z are numbers between 1 and 6):");
                continue;
            }
            
            HashSet<int> uniqueNumbers = new HashSet<int>();
            bool validNumbers = true;
            for (int i = 1; i < splittedInput.Length; i++)
            {
                if (!int.TryParse(splittedInput[i], out int number) || number < 1 || number > 6 || !uniqueNumbers.Add(number))
                {
                    validNumbers = false;
                }
            }
            if (!validNumbers)
            {
                Console.WriteLine("Invalid input! Please enter unique numbers between 1 and 6:");
                continue;
            }

            FImon fst = fImons[int.Parse(splittedInput[1]) - 1];
            FImon snd = fImons[int.Parse(splittedInput[2]) - 1];
            FImon trd = fImons[int.Parse(splittedInput[3]) - 1];
            Console.WriteLine($"You have chosen {fst.Name}, {snd.Name} and {trd.Name}");
            Console.WriteLine("Your commands are: check, fight, info, sort, quit.");
            
            return new Trainer([fst, snd, trd]);
        }
    }
}