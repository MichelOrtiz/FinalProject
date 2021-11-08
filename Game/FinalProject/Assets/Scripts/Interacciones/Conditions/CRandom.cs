using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newCondition", menuName ="Interaction/Conditions/Random")]
public class CRandom : InterCondition
{
    [SerializeField] float probability;
    protected override bool checkIsDone()
    {
        return RandomGenerator.MatchProbability(probability);
    }

}
