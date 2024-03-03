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
        Play();
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

    private void Play()
    {
        while (roundsWon < 3)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) 
            {
                continue;
            }
    
            switch (input.Split(" ")[0].ToLower())
            {
                case "check":
                    OutputManager.DisplayEnemyInfo(enemy);
                    break;
                case "fight":
                    break;
                case "info":
                    OutputManager.DisplayInfo(roundsWon, player);
                    break;
                case "sort":
                    OutputManager.DisplaySortMessage(player);
                    while (true)
                    {
                        string order_string = Console.ReadLine();
                        if (string.IsNullOrEmpty(order_string)) 
                        {
                            continue;
                        }
                        
                        string[] order = order_string.Split(" ");
                        
                        if (order.Length == 3 && int.TryParse(order[0], out int x) && int.TryParse(order[1], out int y) && int.TryParse(order[2], out int z))
                        {
                            if (x >= 1 && x <= 3 && y >= 1 && y <= 3 && z >= 1 && z <= 3 && x != y && x != z && y != z)
                            {
                                player.FImons = new List<FImon> {player.FImons[x - 1], player.FImons[y - 1], player.FImons[z - 1]};
                                OutputManager.DisplayMessage("The order of your FImons has been updated.");
                                break;
                            }
                        }
                        OutputManager.DisplayErrorMessage("Invalid input! Enter the order in the format 'x y z' (where x, y, z are uniqie numbers between 1 and 3):");
                        
                    }
                    break;
                case "quit":
                    OutputManager.DisplayQuitMessage();
                    return;
                default:
                    OutputManager.DisplayErrorMessage("Invalid command! Your commands are: check, fight, info, sort, quit.");
                    break;
            }
            
            // IBattle battle = new Battle();
            // Trainer winner = battle.PerformBattle(player, enemy);
            // if (winner == player)
            // {
            //     roundsWon++;
            // }
        }
        OutputManager.DisplayWinnerMessage();
    }
}