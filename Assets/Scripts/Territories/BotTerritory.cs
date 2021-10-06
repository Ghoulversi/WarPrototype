using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BotTerritory : Territory
{
    public float TimeBetweenAttacks = 5f;
    private float _timerWaitingForAttack;

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

            if (territoryToAttack == null) return;

            War.Attack(this, territoryToAttack, this.gameObject.transform);
        }
    }

    private Territory GetTerritoryToAttack()
    {
        var randomWidth = Random.Range(0, GamePlayManager.Grid.width);
        var randomHeight = Random.Range(0, GamePlayManager.Grid.height);

        var objToAttack = GamePlayManager.Grid.GetObjectAtPos(new Vector2(randomWidth, randomHeight));

        if (objToAttack == null)
        {
            Debug.Log("object is null");
            return null;
        }
        var territoryToAttack = objToAttack.GetComponent<Territory>();

        if (objToAttack == gameObject || territoryToAttack.TerritoryType == TerritoryType.Enemy)
            return null;

        return territoryToAttack;
    }
}
