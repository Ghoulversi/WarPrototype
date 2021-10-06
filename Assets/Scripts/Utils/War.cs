using System.Collections;
using System.Collections.Generic;
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
            for (var x = 0; x < currentAttackingPoints; x++)
            {
                territoryAttack.MinusWarriors(1);

                var warrior = GameObject.Instantiate(territoryAttack.WarriorPrefab, territoryAttack.gameObject.transform.position, Quaternion.identity, warriorParent);
                warrior.GetComponent<Warrior>().SetWarrior(1, currentAttackingType, currentAttackingMat, currentAttackingQuadMat, territoryToAttack);

                var distance = Vector3.Distance(warrior.gameObject.transform.position, currentTerritoryToAttackTransform.position);

                warrior.GetComponent<Warrior>().SetPos(territoryAttack.gameObject.transform.position, currentTerritoryToAttackTransform.position, distance);
            }
        }
    }
}
