using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BotTerritory : Territory
{
    public float TimeBetweenAttacks = 5f;

    private Grid _grid;
    private float _timerWaitingForAttack;

    private void Awake()
    {
        _grid = TestScript.Grid;
    }

    protected override void Update()
    {
        base.Update();

        if (TerritoryType == TerritoryType.Enemy)
            TryToAttack();
    }

    private void TryToAttack()
    {
        _timerWaitingForAttack += Time.deltaTime;

        if (_timerWaitingForAttack >= TimeBetweenAttacks)
        {
            _timerWaitingForAttack = 0f;

            var territoryToAttack = GetTerritoryToAttack();

            if (territoryToAttack.gameObject == null) return;

            War.Attack(this, territoryToAttack);
        }
    }

    private Territory GetTerritoryToAttack()
    {
        var randomWidth = Random.Range(0, _grid.width);
        var randomHeight = Random.Range(0, _grid.height);

        var objToAttack = _grid.GetObjectAtPos(new Vector2(randomWidth, randomHeight));

        if (objToAttack == null)
        {
            Debug.Log("object is null");
            return null;
        }
        var territoryToAttack = objToAttack.GetComponent<Territory>();

        if (objToAttack == gameObject || territoryToAttack.TerritoryType == TerritoryType.Enemy)
            GetTerritoryToAttack();

        return territoryToAttack;
    }
}
