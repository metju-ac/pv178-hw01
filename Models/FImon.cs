using System;
using System.Collections.Generic;

namespace HW01_2024.Models;

public enum FImonType
{
    Fire,
    Sea,
    Leaf
}

public class FImon
{
    private static List<string> _names =
    [
        "Baba pod korenem",
        "Svarta",
        "Kluk s kamenim",
        "Kluk z autobusu",
        "Kluk s retizkem",
        "Jirka Kara",
        "Majsneros",
        "Mrtvy krt",
        "Jolanda",
        "Pochcan",
        "Chcankoptak",
        "Standa Rezac",
        "Milan Buricin",
        "Mackas mi hada",
        "Baba - ma pravo sedet",
        "Ondrej (prosim)"
    ];
    
    public string Name { get; private set;  }
    public int Attack { get; private set; }
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }
    public int Speed { get; private set; }
    public int Level { get; private set; }
    public int Xp { get; private set; }
    public FImonType Type { get; private set; }
    
    public FImon()
    {
        Name = _names[new Random().Next(0, _names.Count)];
        Attack = new Random().Next(1, 10);
        MaxHp = new Random().Next(1, 10);
        CurrentHp = MaxHp;
        Speed = new Random().Next(1, 10);
        Level = 1;
        Xp = 0;
        Type = (FImonType)new Random().Next(0, 3);
    }
    
    public void Heal()
    {
        CurrentHp = MaxHp;
    }
    
    public void GainXp(int xp)
    {
        Xp += xp;
        while (Xp >= 100)
        {
            Level++;
            Xp -= 100;
            Attack += new Random().Next(1, 4);
            MaxHp += new Random().Next(1, 4);
        }
    }
    
}