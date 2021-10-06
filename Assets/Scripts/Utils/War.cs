using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class War
{
    

    public static void Attack(Territory territoryAttack, Territory territoryToAttack)
    {
        if (territoryAttack == null || territoryToAttack == null) return;

        var currentAttackingPoints = territoryAttack.GetPoints();
        var currentAttackingType = territoryAttack.TerritoryType;
        var currentAttackingMat = territoryAttack.TerritoryMat;

        var currentFriendlyTerritoryTransform = territoryAttack.gameObject.transform;
        var currentTerritoryToAttackTransform = territoryToAttack.gameObject.transform;

        if (territoryToAttack is NeutralTerritory neutralTerritory)
        {
            if (currentAttackingPoints > neutralTerritory.MaxRangePoints) currentAttackingPoints = neutralTerritory.MaxRangePoints;
        }

        if (territoryAttack.WarriorPrefab != null)
        {
            var posToGo = Vector3.zero;
            for (var x = 0; x < currentAttackingPoints; x++)
            {
                territoryAttack.MinusWarriors(1);

                var warrior = GameObject.Instantiate(territoryAttack.WarriorPrefab, territoryAttack.gameObject.transform.position, Quaternion.identity);
                warrior.GetComponent<Warrior>().SetWarrior(1, currentAttackingType, currentAttackingMat, territoryToAttack);

                var distance = Vector3.Distance(currentFriendlyTerritoryTransform.position, currentTerritoryToAttackTransform.position);

                warrior.GetComponent<Warrior>().SetPos(territoryAttack.gameObject.transform.position, currentTerritoryToAttackTransform.position, distance);
            }
        }
    }
}
