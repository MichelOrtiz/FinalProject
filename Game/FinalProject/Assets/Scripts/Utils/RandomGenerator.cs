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
        
        num = (ushort) random.Next(min, max+1);

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
            num = (ushort) random.Next(min, max+1);
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

    public static T RandomElement<T>(List<T> list)
    {
        return list[NewRandom(0, list.Count-1)];
    }

    public static int MatchedElement(List<float> percentages)
    {
        System.Random random = new System.Random();
        bool elementMatched = false;
        int matchedElement = 0;

        double number = NewRandomDouble();
        float probabilitySum = 0;

        for (int i = 0; i < percentages.Capacity; i++)
        {
            probabilitySum += percentages[i];

            if (i == 0)
            {
                elementMatched = number <= percentages[i];
            }
            else
            {
                elementMatched = number > percentages[i-1] && number <= probabilitySum;
            }

            if (elementMatched)
            {
                matchedElement = i;
                break;
            }
        }

        return matchedElement;
    }

    public static T MatchedElement<T>(List<ObjectProbability<T>> elements)
    {
        System.Random random = new System.Random();
        double number = NewRandomDouble();

        List<float> probabilities = new List<float>();
        elements.ForEach(e => probabilities.Add(e.Probability));

        return elements[MatchedElement(probabilities)].TObject;
    }

    public static Vector2 RandomPointInBounds(Bounds bounds)
    {
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(x, y);
    }
}
