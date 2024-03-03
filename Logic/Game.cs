using System;
using System.Collections.Generic;
using HW01_2024.Interfaces;
using HW01_2024.Models;
using HW01_2024.Utils;

namespace HW01_2024.Logic;

public class Game : IGame
{
    private Trainer player {get; set;}
    private Trainer enemy {get; set;}
    private int roundsWon {get; set;}
    
    public void Start()
    {
        roundsWon = 0;
        player = SelectFImons();
        enemy = new Trainer();
    }
    
    private Trainer SelectFImons()
    {
        OutputManager.DisplatWelcomeMessage();
        List<FImon> fImons = new List<FImon>();
        for (int i = 0; i < 6; i++)
        {
            fImons.Add(new FImon());
        }
        OutputManager.DisplayFImonsWithNumbers(fImons);
        
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
                OutputManager.DisplayErrorMessage("Invalid input! Enter input in the format 'start x y z' (where x, y, z are numbers between 1 and 6):");
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
                OutputManager.DisplayErrorMessage("Invalid input! Please enter unique numbers between 1 and 6:");
                continue;
            }

            FImon fst = fImons[int.Parse(splittedInput[1]) - 1];
            FImon snd = fImons[int.Parse(splittedInput[2]) - 1];
            FImon trd = fImons[int.Parse(splittedInput[3]) - 1];
            
            OutputManager.DisplaySelectedFImons([fst, snd, trd]);
            return new Trainer([fst, snd, trd]);
        }
    }


}