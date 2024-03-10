using System.Diagnostics;
using HW01_2024.Interfaces;
using HW01_2024.Models;
using HW01_2024.Utils;

namespace HW01_2024.Logic;

public class Battle : IBattle
{
    private int AttackMultiplier(FImon attacker, FImon defender)
    {
        if (attacker.Type == FImonType.Fire && defender.Type == FImonType.Leaf ||
            attacker.Type == FImonType.Sea && defender.Type == FImonType.Fire ||
            attacker.Type == FImonType.Leaf && defender.Type == FImonType.Sea)
        {
            return 2;
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

        while (faster.CurrentHp > 0 && slower.CurrentHp > 0)
        {
            int damage = faster.Attack * AttackMultiplier(faster, slower);
            slower.AbsorbDamage(damage);
            OutputManager.DisplayAttack(faster, slower, damage);
            if (slower.CurrentHp <= 0)
            {
                return faster;
            }
            
            damage = slower.Attack * AttackMultiplier(slower, faster);
            faster.AbsorbDamage(damage);
            OutputManager.DisplayAttack(slower, faster, damage);
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
            OutputManager.DisplayMessage($"Round {round}");
            FImon winner = PerformDuel(player.FImons[playerFImonIndex], enemy.FImons[enemyFImonIndex]);
            if (winner == player.FImons[playerFImonIndex])
            {
                OutputManager.DisplayWinningFImom(player.FImons[playerFImonIndex]);
                enemyFImonIndex++;
            }
            else
            {
                OutputManager.DisplayWinningFImom(enemy.FImons[enemyFImonIndex]);
                playerFImonIndex++;
            }
            round++;
        }
        
        if (playerFImonIndex == player.FImons.Count)
        {
            OutputManager.DisplayMessage("You lost the battle.");
            return enemy;
        }
        OutputManager.DisplayMessage("You won the battle!");
        return player;
    }
}