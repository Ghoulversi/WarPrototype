using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Territory : MonoBehaviour
{
    public GameObject TerritoryQuad;
    public GameObject WarriorPrefab;
    public TextMeshPro PointsTxtPro;
    //Min and Max range for start points to be set
    public int MinRangePoints;
    public int MaxRangePoints;

    public int PointsIncreaseDelay = 1;
    public Material TerritoryMat;
    public Material TerritoryQuadMat;
    public Renderer TerritoryRenderer;
    public TerritoryType TerritoryType;

    public bool IsActivated;

    private float _timer;
    private Vector3 _startScale;
    private Sequence _sequence;

    private int _currentPoints;
    public int CurrentPoints
    {
        get => _currentPoints;
        set => _currentPoints = value;
    }

   

    private void Start()
    {
        _startScale = gameObject.transform.localScale;
        CurrentPoints = Random.Range(MinRangePoints, MaxRangePoints);

        TerritoryQuad.GetComponent<Renderer>().material = TerritoryQuadMat;

    }

    protected virtual void Update()
    {
        UpdatePoints();

        SetTextMeshPro();
    }

    public virtual void UpdatePoints()
    {
        _timer += Time.deltaTime;

        if (_timer >= PointsIncreaseDelay)
        {
            _timer = 0f;
            CurrentPoints++;
        }
    }

    public virtual void SetTextMeshPro()
    {

    }

    public int GetPoints()
    {
        return CurrentPoints;
    }

    public void MinusWarriors(int minusWarriors)
    {
        CurrentPoints -= minusWarriors;
        if (CurrentPoints < 0) CurrentPoints = 0;
    }

    public void Attacked(int attackingPoints, TerritoryType attackingSide, Material attackingMat, Material attackingQuadMat)
    {
        if (attackingSide != TerritoryType)
        {
            CurrentPoints -= attackingPoints;
        }
        else
        {
            CurrentPoints += attackingPoints;
        }

        if (CurrentPoints < 0)
        {
            CurrentPoints = 0;
            Defeated(attackingSide, attackingMat, attackingQuadMat);
        }
    }

    /// <summary>
    /// Activates if not activated and vice versa. Also animating if activated.
    /// </summary>
    public void ActivateElement()
    {
        AnimateElement();
    }

    public void StopAnimationElement()
    {
        _sequence.Pause();
        TerritoryRenderer.gameObject.transform.localScale = _startScale;
    }

    private void AnimateElement()
    {
        _sequence = DOTween.Sequence();
        Tween bigger = TerritoryRenderer.gameObject.transform.DOScale(_startScale * 1.5f, 1f);
        Tween smaller = TerritoryRenderer.gameObject.transform.DOScale(_startScale, 1f);
        _sequence.Append(bigger);
        _sequence.Append(smaller);
        _sequence.SetLoops(-1);
        _sequence.Play();
    }

    private void Defeated(TerritoryType attackingSide, Material attackingMat, Material attackingQuadMat)
    {
        TerritoryMat = attackingMat;
        TerritoryType = attackingSide;
        TerritoryRenderer.material = attackingMat;
        TerritoryQuad.GetComponent<Renderer>().material = attackingQuadMat;
        StopAnimationElement();
        GameEvents.Instance.DefeatTriggerTerritory();
    }
}
