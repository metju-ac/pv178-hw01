using System;
using System.Collections.Generic;

namespace HW01_2024.Models;

public class Trainer
{
    public List<FImon> FImons { get; set; }
    
    public Trainer(int round)
    {
        FImons = new List<FImon>();
        for (int i = 0; i < 3; i++)
        {
            FImon fImon = new FImon();
            fImon.GainXp(new Random().Next(100, 250) * round);
            FImons.Add(fImon);
        }
    }
    
    public Trainer(List<FImon> fImons)
    {
        FImons = fImons;
    }
    
    public void HealFImons()
    {
        foreach (FImon fImon in FImons)
        {
            fImon.Heal();
        }
    }
    
    public void BattleEnded(bool won)
    {
        foreach (FImon fImon in FImons)
        {
            fImon.GainXp(new Random().Next(30, 100) * (won ? 2 : 1));
        }
        HealFImons();
    }
}