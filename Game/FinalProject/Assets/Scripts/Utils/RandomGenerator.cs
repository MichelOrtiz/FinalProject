using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class RandomGenerator
{
    public static ushort NewRandom(ushort min, ushort max)
    {
        ushort num;
        System.Random random = new System.Random();
        
        num = (ushort) random.Next(min, max);

        return num; 
    }

    public static ushort NewRandom(short min, short max)
    {
        ushort num;
        System.Random random = new System.Random();
        
        num = (ushort) random.Next(min, max);

        return num; 
    }

    public static int NewRandom(int min, int max)
    {
        int num;
        System.Random random = new System.Random();
        
        num = random.Next(min, max);

        return num; 
    }

    public static ushort NewRandom(List<ushort> list, ushort min, ushort max)
    {
        ushort num;
        System.Random random = new System.Random();
        
        do
        {
            num = (ushort) random.Next(min, max);
        }
        while (list.Contains(num));

        return num; 
    }
}
