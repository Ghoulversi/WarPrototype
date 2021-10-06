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
        if (SelectedFriendlyTerritory == null) return;

        War.Attack(SelectedFriendlyTerritory, territory);
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
