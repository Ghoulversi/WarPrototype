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

    private Material _warriorQuadMat;
    public Material WarriorQuadMat
    {
        get => _warriorQuadMat;
        set => _warriorQuadMat = value;
    }



    [SerializeField]
    private Territory _territoryToAttack;
    public Territory TerritoryToAttack
    {
        get => _territoryToAttack;
        set => _territoryToAttack = value;
    }

    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector3 _currentPos;

    private float _distance;
    private float _startTime;
    private float _speed = .01f;
    private float _delayForStart;
    private float _currentTimeToStart;

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        if (_currentTimeToStart < _delayForStart)
        {
            _currentTimeToStart += Time.deltaTime;
        }
        else
        {
            float distCovered = (Time.time - _startTime) * _speed;
            float fractionOfJourney = distCovered / _distance;

            _currentPos = Vector3.Lerp(_currentPos, _endPos, fractionOfJourney);

            transform.position = _currentPos;
        }
    }

    public void SetPos(Vector3 startPos, Vector3 endPos, float distance, float delay)
    {
        _startPos = startPos;
        _endPos = endPos;
        _currentPos = startPos;
        _distance = distance;
        _delayForStart = delay;
    }

    public void SetWarrior(int attackingDmg, TerritoryType attackingSide, Material attackingMat, Material attackingQuadMat, Territory territoryToAttack)
    {
        AttackDmg = attackingDmg; 
        WarriorType = attackingSide;
        WarriorMat = attackingMat;
        TerritoryToAttack = territoryToAttack;
        WarriorQuadMat = attackingQuadMat;

        GetComponent<Renderer>().material = WarriorMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TerritoryToAttack == null) return;


        if (other.gameObject == TerritoryToAttack.gameObject)
        {
            TerritoryToAttack.Attacked(AttackDmg, WarriorType, WarriorMat, WarriorQuadMat);
            Destroy(gameObject);
        }
    }
}
