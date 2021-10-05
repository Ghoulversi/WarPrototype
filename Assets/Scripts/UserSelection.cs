using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UserSelection : MonoBehaviour
{
    public Territory SelectedFriendlyTerritory;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                var territory = hit.transform.gameObject.GetComponent<Territory>();
                if (territory != null)
                {
                    if (territory.TerritoryType == TerritoryType.Player)
                    {
                        ManipulatePlayerTerritory(territory);
                    }
                    else if (territory.TerritoryType != TerritoryType.Player)
                    {
                        ManipulateOtherTerritory(territory);
                    }
                }
            }
        }
    }

    private void ManipulatePlayerTerritory(Territory territory)
    {
        ResetSelectedTerritories();
        if (territory.IsActivated)
        {
            territory.IsActivated = false;
            territory.StopAnimationElement();
            ResetSelectedTerritories();
        }
        else
        {
            territory.IsActivated = true;
            territory.ActivateElement();
            SelectedFriendlyTerritory = territory;
        }
    }

    private void ManipulateOtherTerritory(Territory territory)
    {
        Attack(territory);
        Debug.Log("Attacked!!");
    }

    private void Attack(Territory territoryToAttack)
    {
        var currentAttackingPoints = SelectedFriendlyTerritory.GetPoints();
        var currentAttackingType = SelectedFriendlyTerritory.TerritoryType;
        var currentAttackingMat = SelectedFriendlyTerritory.TerritoryMat;
        
        var currentFriendlyTerritoryTransform = SelectedFriendlyTerritory.gameObject.transform;
        var currentTerritoryToAttackTransform = territoryToAttack.gameObject.transform;

        if (SelectedFriendlyTerritory.WarriorPrefab != null)
        {

            Debug.Log($"Territory points: {currentAttackingPoints}");
            for (var x = 0; x < currentAttackingPoints; x++)
            {
                var warrior = GameObject.Instantiate(SelectedFriendlyTerritory.WarriorPrefab, SelectedFriendlyTerritory.gameObject.transform.position, Quaternion.identity);
                warrior.GetComponent<Warrior>().SetWarrior(1, currentAttackingType, currentAttackingMat, territoryToAttack);
                SelectedFriendlyTerritory.MinusWarriors(1);
                var distance = Vector3.Distance(currentFriendlyTerritoryTransform.position, currentTerritoryToAttackTransform.position);
                warrior.transform.DOMove(currentTerritoryToAttackTransform.position, distance / 2);
            }
        }


        //territoryToAttack.Attacked(currentAttackingPoints, currentAttackingType, currentAttackingMat);
    }

    private void ResetSelectedTerritories()
    {
        if (SelectedFriendlyTerritory != null)
        {
            SelectedFriendlyTerritory.StopAnimationElement();
            SelectedFriendlyTerritory = null;
        }
    }
}
