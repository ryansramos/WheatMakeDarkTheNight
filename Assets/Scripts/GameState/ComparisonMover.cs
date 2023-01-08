using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparisonMover : MonoBehaviour
{

    [SerializeField]
    private float _wheatGameplayPosition;

    [SerializeField]
    private float _wheatComparisonPosition;

    [SerializeField]
    private float _skylineGameplayPosition;

    [SerializeField]
    private float _skylineComparisonPosition;

    [SerializeField]
    private GameObject _compareCam;

    [SerializeField]
    private float _compareCamGameplayPosition;

    private bool _isMoving;
    public bool isMoving => _isMoving;

    [Range(0f, 10f)]
    [SerializeField]
    private float _duration;

    private Transform
        _wheat,
        _skyline;

    public void Initialize(WheatManager wheat, SkylineManager skyline)
    {
        _wheat = wheat.transform;
        _skyline = skyline.transform;
        _isMoving = false;
    }

    public void MoveToComparisonPosition()
    {
        _isMoving = true;
        _compareCam.transform.position = Vector3.zero + Vector3.forward * -1f;
        StartCoroutine(MoveToComparison());
    }

    public void MoveToGameplayPosition()
    {
        _isMoving = true;
        _compareCam.transform.position = new Vector3(0f, _compareCamGameplayPosition, -1f);
        StartCoroutine(MoveToGameplay());
    }

    IEnumerator MoveToComparison()
    {
        float timer = 0f;
        while (timer < _duration)
        {
            float timeRatio = timer / _duration;
            SetTransformHeight(_wheat, _wheatGameplayPosition, _wheatComparisonPosition, timeRatio);
            SetTransformHeight(_skyline, _skylineGameplayPosition, _skylineComparisonPosition, timeRatio);
            timer += Time.deltaTime;
            yield return null;
        }
        SetTransformHeight(_wheat, _wheatGameplayPosition, _wheatComparisonPosition, 1f);
        SetTransformHeight(_skyline, _skylineGameplayPosition, _skylineComparisonPosition, 1f);
        _isMoving = false;
    }

    IEnumerator MoveToGameplay()
    {
        float timer = 0f;
        while (timer < _duration)
        {
            float timeRatio = timer / _duration;
            SetTransformHeight(_wheat, _wheatComparisonPosition, _wheatGameplayPosition, timeRatio);
            SetTransformHeight(_skyline, _skylineComparisonPosition, _skylineGameplayPosition, timeRatio);
            timer += Time.deltaTime;
            yield return null;
        }
        SetTransformHeight(_wheat, _wheatComparisonPosition, _wheatGameplayPosition, 1f);
        SetTransformHeight(_skyline, _skylineComparisonPosition, _skylineGameplayPosition, 1f);
        _isMoving = false;
    }

    private void SetTransformHeight(Transform t, float startPosition, float targetPosition, float percentage)
    {
        float newTransformPosition = startPosition + (targetPosition - startPosition) * percentage;
        t.position = new Vector3(t.position.x, newTransformPosition, t.position.z);
    }
}
