using System;
using System.Diagnostics;
using HW01_2024.Interfaces;
using HW01_2024.Models;

namespace HW01_2024.Logic;

public class Battle : IBattle
{
    private double AttackMultiplier(FImon attacker, FImon defender)
    {
        if (attacker.Type == FImonType.Fire && defender.Type == FImonType.Leaf ||
            attacker.Type == FImonType.Sea && defender.Type == FImonType.Fire ||
            attacker.Type == FImonType.Leaf && defender.Type == FImonType.Sea)
        {
            return 1.5;
        }
        return 1;
    }
    
    public FImon PerformDuel(FImon playerFImon, FImon enemyFImon)
    {
        FImon faster, slower;
        if (playerFImon.Speed >= enemyFImon.Speed)
        {
            faster = playerFImon;
            slower = enemyFImon;
        }
        else
        {
            faster = enemyFImon;
            slower = playerFImon;
        }

        int attack;
        while (faster.CurrentHp > 0 && slower.CurrentHp > 0)
        {
            attack = (int)Math.Round(faster.Attack * AttackMultiplier(faster, slower));
            slower.CurrentHp -= attack;
            Console.WriteLine($"{faster.Name} attacks {slower.Name} for {attack} damage. {slower.Name} has {slower.CurrentHp} HP left.");
            if (slower.CurrentHp <= 0)
            {
                return faster;
            }
            
            attack = (int)Math.Round(slower.Attack * AttackMultiplier(slower, faster));
            faster.CurrentHp -= attack;
            Console.WriteLine($"{slower.Name} attacks {faster.Name} for {attack} damage. {faster.Name} has {faster.CurrentHp} HP left.");
            if (faster.CurrentHp <= 0)
            {
                return slower;
            }
        }

        Debug.Assert(false, "Duel should not reach this point without a winner.");
        return null; // This line is unreachable, added to satisfy compiler
    }

    public Trainer PerformBattle(Trainer player, Trainer enemy)
    {
        int round = 1;
        int playerFImonIndex = 0;
        int enemyFImonIndex = 0;
        
        while (playerFImonIndex < player.FImons.Count && enemyFImonIndex < enemy.FImons.Count)
        {
            Console.WriteLine($"Round {round}");
            FImon winner = PerformDuel(player.FImons[playerFImonIndex], enemy.FImons[enemyFImonIndex]);
            if (winner == player.FImons[playerFImonIndex])
            {
                Console.WriteLine($"{player.FImons[playerFImonIndex].Name} wins!");
                enemyFImonIndex++;
            }
            else
            {
                Console.WriteLine($"{enemy.FImons[enemyFImonIndex].Name} wins!");
                playerFImonIndex++;
            }
            round++;
        }
        
        if (playerFImonIndex == player.FImons.Count)
        {
            Console.WriteLine("You lost the battle.");
            return enemy;
        }
        Console.WriteLine("You won the battle!");
        return player;
        
    }
}