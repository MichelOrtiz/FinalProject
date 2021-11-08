using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Interaction", menuName = "Interaction/OneOfMany")]
public class InterOFM : Interaction
{
    [SerializeField] List<Interaction> manyInteractions;
    [SerializeField] float probabiblity = 1;
    public override void DoInteraction(){
        if(condition!=null){
            if(condition.isDone){
                OneOfMany();
            }else{
                onEndInteraction();
            }
        }else{
            OneOfMany();
        }
    }

    void OneOfMany(){
        bool atLeastOne = false;
        while(!atLeastOne){
            foreach(Interaction inter in manyInteractions){
                if(RandomGenerator.MatchProbability(probabiblity)){
                    atLeastOne = true;
                    inter.gameObject = gameObject;
                    inter.RestardCondition();
                    inter.DoInteraction();
                    break;
                }
            }
        }
        onEndInteraction();
    }
}
