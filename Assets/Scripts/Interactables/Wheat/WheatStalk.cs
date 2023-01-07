using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WheatStalk : MonoBehaviour
{
    [SerializeField]
    private GameSettingsSO _settings;

    [Header("Listening on: ")]
    [SerializeField]
    private Vector3EventChannel _onClickEvent;

    [Header("Broadcasting on: ")]
    [SerializeField]
    private VoidEventChannelSO _onWheatCutEvent;

    public UnityEvent<WheatStalk> RequestSetActive;
    public UnityEvent<WheatStalk> RequestDeactivate;

    private Bounds _bounds;
    private Transform _transform;
    private SpriteRenderer _renderer;
    private WheatHighlight _highlight;
    private float _currentHeight;

    void Awake()
    {
        _transform = gameObject.GetComponent<Transform>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        _highlight = gameObject.GetComponentInChildren<WheatHighlight>();
    }

    void OnEnable()
    {
        _onClickEvent.OnEventRaised += OnClick;
    }

    void OnDisable()
    {
        _onClickEvent.OnEventRaised -= OnClick;
    }

    void Start()
    {
        OnReset();
    }

    public void OnReset()
    {
        _currentHeight = _settings.maxWheatHeight;
        _renderer.sortingOrder = 1;
        _highlight.Reset();
        UpdateSprite(_currentHeight);
        CalculateBounds();
    }

    public void SetActive(bool status)
    {
        if (status)
        {
            _renderer.sortingOrder = 3;
            _highlight.TurnOn();
        }
        else
        {
            _renderer.sortingOrder = 1;
            _highlight.TurnOff();
        }
    }

    public void OnClick(Vector3 position)
    {
        if (_bounds.Contains(position))
        {
            if (CheckCursorHeight(position.y, out float yPosition))
            {
                _currentHeight = yPosition;
                UpdateSprite(yPosition);
                _onWheatCutEvent.RaiseEvent();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<ReticleMover>(out ReticleMover reticle))
        {
            OnHover(reticle.GetWorldPosition());
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<ReticleMover>(out ReticleMover reticle))
        {
            OnHover(reticle.GetWorldPosition());
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<ReticleMover>(out ReticleMover reticle))
        {
            OnHoverRelease();
        } 
    }
    
    void OnHover(Vector3 position)
    {
        if (CheckCursorHeight(position.y, out float y))
        {
            RequestSetActive?.Invoke(this);
        }
        else
        {
            RequestDeactivate?.Invoke(this);
        }
    }

    void OnHoverRelease()
    {
        RequestDeactivate?.Invoke(this);
    }

    bool CheckCursorHeight(float position, out float yRelativePosition)
    {
        yRelativePosition = position - _bounds.min.y;
        return yRelativePosition < _currentHeight;
    }


    void CalculateBounds()
    {
        _bounds = _renderer.bounds;
    }

    void UpdateSprite(float height)
    {
        _renderer.size = new Vector2(2f, height);
        _highlight.UpdateSprite(height);
    }
}
