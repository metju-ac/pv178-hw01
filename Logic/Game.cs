using System;
using System.Collections.Generic;
using HW01_2024.Interfaces;
using HW01_2024.Models;
using HW01_2024.Utils;

namespace HW01_2024.Logic;

public class Game : IGame
{
    private Trainer Player {get; set;}
    private Trainer Enemy {get; set;}
    private int RoundsWon {get; set;}
    
    public void Start()
    {
        RoundsWon = 0;
        Player = SelectFImons();
        Enemy = new Trainer(RoundsWon + 1);
        Play();
    }
    
    private Trainer SelectFImons()
    {
        OutputManager.DisplayWelcomeMessage();
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
            string[] splitInput = input.Split(" ");
            
            if (splitInput.Length != 4 || splitInput[0].ToLower() != "start")
            {
                OutputManager.DisplayErrorMessage("Invalid input! Enter input in the format 'start x y z' (where x, y, z are numbers between 1 and 6):");
                continue;
            }
            
            HashSet<int> uniqueNumbers = new HashSet<int>();
            bool validNumbers = true;
            for (int i = 1; i < splitInput.Length; i++)
            {
                if (!int.TryParse(splitInput[i], out int number) || number < 1 || number > 6 || !uniqueNumbers.Add(number))
                {
                    validNumbers = false;
                }
            }
            if (!validNumbers)
            {
                OutputManager.DisplayErrorMessage("Invalid input! Please enter unique numbers between 1 and 6:");
                continue;
            }

            FImon fst = fImons[int.Parse(splitInput[1]) - 1];
            FImon snd = fImons[int.Parse(splitInput[2]) - 1];
            FImon trd = fImons[int.Parse(splitInput[3]) - 1];
            
            OutputManager.DisplaySelectedFImons([fst, snd, trd]);
            return new Trainer([fst, snd, trd]);
        }
    }

    private void Play()
    {
        while (RoundsWon < 3)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) 
            {
                continue;
            }
    
            switch (input.Split(" ")[0].ToLower())
            {
                case "check":
                    OutputManager.DisplayEnemyInfo(Enemy);
                    break;
                case "fight":
                    IBattle battle = new Battle();
                    Trainer winner = battle.PerformBattle(Player, Enemy);
                    if (winner == Player)
                    {
                        Player.BattleEnded(true);
                        Enemy = new Trainer(++RoundsWon + 1);
                    }
                    else
                    {
                        Player.BattleEnded(false);
                        Enemy.HealFImons();
                    }
                    break;
                case "info":
                    OutputManager.DisplayInfo(RoundsWon, Player);
                    break;
                case "sort":
                    OutputManager.DisplaySortMessage(Player);
                    while (true)
                    {
                        string orderString = Console.ReadLine();
                        if (string.IsNullOrEmpty(orderString)) 
                        {
                            continue;
                        }
                        
                        string[] order = orderString.Split(" ");
                        
                        if (order.Length == 3 && int.TryParse(order[0], out int x) && int.TryParse(order[1], out int y) && int.TryParse(order[2], out int z))
                        {
                            if (x >= 1 && x <= 3 && y >= 1 && y <= 3 && z >= 1 && z <= 3 && x != y && x != z && y != z)
                            {
                                Player.FImons = [Player.FImons[x - 1], Player.FImons[y - 1], Player.FImons[z - 1]];
                                OutputManager.DisplayMessage("The order of your FImons has been updated.");
                                break;
                            }
                        }
                        OutputManager.DisplayErrorMessage("Invalid input! Enter the order in the format 'x y z' (where x, y, z are unique numbers between 1 and 3):");
                    }
                    break;
                case "quit":
                    OutputManager.DisplayQuitMessage();
                    return;
                default:
                    OutputManager.DisplayErrorMessage("Invalid command! Your commands are: check, fight, info, sort, quit.");
                    break;
            }
        }
        OutputManager.DisplayWinnerMessage();
    }
}