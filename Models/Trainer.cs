using System.Collections.Generic;

namespace HW01_2024.Models;

public class Trainer
{
    public List<FImon> FImons { get; private set; }
    
    public Trainer()
    {
        FImons = new List<FImon>();
        for (int i = 0; i < 3; i++)
        {
            FImons.Add(new FImon());
        }
    }
    
    public Trainer(List<FImon> fImons)
    {
        FImons = fImons;
    }
    
}