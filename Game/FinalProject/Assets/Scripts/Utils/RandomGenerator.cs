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
        
        num = new System.Random().Next(min,max+1);

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

    public static bool MatchProbability(float basedPercentage)
    {
        System.Random random = new System.Random();
        
        return NewRandomDouble() <= basedPercentage;
    }

    public static bool MatchProbability(float startPercentage, float endPercentage)
    {
        System.Random random = new System.Random();
        double per =  NewRandomDouble();
        
        return per > startPercentage && per <= endPercentage;;
    }

    public static double NewRandomDouble()
    {
        System.Random random = new System.Random();

        return random.NextDouble() * 100;
    }

    public static int MatchedElement(List<float> percentages)
    {
        System.Random random = new System.Random();
        bool elementMatched = false;
        int matchedElement = 0;

        double number = NewRandomDouble();

        for (int i = 0; i < percentages.Capacity; i++)
        {
            if (i == 0)
            {
                elementMatched = number <= percentages[i];
            }
            else
            {
                elementMatched = number > percentages[i-1] && number <= percentages[i-1] + percentages[i];
            }

            if (elementMatched)
            {
                matchedElement = i;
                break;
            }
        }

        return matchedElement;
    }
}
