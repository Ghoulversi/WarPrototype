using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public static class War
{
    public static void Attack(Territory territoryAttack, Territory territoryToAttack, Transform warriorParent)
    {
        if (territoryAttack == null || territoryToAttack == null) return;

        var currentAttackingPoints = territoryAttack.GetPoints();
        var currentAttackingType = territoryAttack.TerritoryType;
        var currentAttackingMat = territoryAttack.TerritoryMat;
        var currentAttackingQuadMat = territoryAttack.TerritoryQuadMat;

        var currentTerritoryToAttackTransform = territoryToAttack.gameObject.transform;

        if (territoryAttack.WarriorPrefab != null)
        {
            float timeDelay = 0f;
            int round = 0;
            float offSet = -.3f;
            for (var x = 0; x < currentAttackingPoints; x++)
            {
                territoryAttack.MinusWarriors(1);

                var territoryPos = territoryAttack.gameObject.transform.position;

                var posX = territoryPos.x + offSet;
                var posZ = territoryPos.z + offSet;

                var finalPos = new Vector3(posX, territoryPos.y, posZ);

                var warrior = GameObject.Instantiate(territoryAttack.WarriorPrefab, finalPos, Quaternion.identity, warriorParent);
                warrior.GetComponent<Warrior>().SetWarrior(1, currentAttackingType, currentAttackingMat, currentAttackingQuadMat, territoryToAttack);

                var distance = Vector3.Distance(finalPos, currentTerritoryToAttackTransform.position);

                warrior.GetComponent<Warrior>().SetPos(finalPos, currentTerritoryToAttackTransform.position, distance, timeDelay);

                round++;
                offSet += .3f;

                if (round > 3)
                {
                    timeDelay += 0.2f;
                    round = 0;
                    offSet = -.3f;
                }
            }
        }
    }
}
