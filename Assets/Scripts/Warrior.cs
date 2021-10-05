using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private int _attackDmg;
    public int AttackDmg
    {
        get => _attackDmg;
        set => _attackDmg = value;
    }

    private TerritoryType _warriorType;
    public TerritoryType WarriorType
    {
        get => _warriorType;
        set => _warriorType = value;
    }

    private Material _warriorMat;
    public Material WarriorMat
    {
        get => _warriorMat;
        set => _warriorMat = value;
    }

    [SerializeField]
    private Territory _territoryToAttack;
    public Territory TerritoryToAttack
    {
        get => _territoryToAttack;
        set => _territoryToAttack = value;
    }

    //public Warrior(int attackingDmg, TerritoryType attackingSide, Material attackingMat)
    //{
    //    AttackDmg = attackingDmg;
    //    WarriorType = attackingSide;
    //    WarriorMat = attackingMat;
    //}

    public void SetWarrior(int attackingDmg, TerritoryType attackingSide, Material attackingMat, Territory territoryToAttack)
    {
        AttackDmg = attackingDmg; 
        WarriorType = attackingSide;
        WarriorMat = attackingMat;
        TerritoryToAttack = territoryToAttack;

        GetComponent<Renderer>().material = WarriorMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TerritoryToAttack == null) return;


        if (other.gameObject == TerritoryToAttack.gameObject)
        {
            Debug.Log("Attacked");
            TerritoryToAttack.Attacked(AttackDmg, WarriorType, WarriorMat);
            Destroy(gameObject);
        }
    }
}
