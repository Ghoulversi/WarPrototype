using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickableElement : MonoBehaviour
{
    private bool _isActivated;

    private Vector3 _startScale;
    private Sequence _sequence;

    private void Start()
    {
        _startScale = gameObject.transform.localScale;
    }

    /// <summary>
    /// Activates if not activated and vice versa. Also animating if activated.
    /// </summary>
    public void ActivatedElement()
    {
        _isActivated = !_isActivated;

        AnimateElement();
    }

    private void AnimateElement()
    {
        if (_isActivated)
        {
            _sequence = DOTween.Sequence();
            Tween bigger = gameObject.transform.DOScale(_startScale * 1.5f, 1f);
            Tween smaller = gameObject.transform.DOScale(_startScale, 1f);
            _sequence.Append(bigger);
            _sequence.Append(smaller);
            _sequence.SetLoops(-1);
            _sequence.Play();
        }
        else
        {
            _sequence.Pause();
            gameObject.transform.localScale = _startScale;
        }

    }
}
