using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private WheatManager _wheat;

    [SerializeField]
    private SkylineManager _skyline;
    
    [SerializeField]
    private ComparisonMover _mover;

    [SerializeField]
    private TextureReader _reader;
    [SerializeField]
    private float _proceedLag;

    [SerializeField]
    private Button _finishButton;

    private IEnumerator _coroutine;
    // private int _day = 0;
    
    private bool _isWaitingToProceed;

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        _mover.Initialize(_wheat, _skyline);
        _wheat.GameReset();
        NextDay(0);
    }

    public void OnProceed()
    {
        if (_isWaitingToProceed)
        {
            _isWaitingToProceed = false;
        }
    }

    public void Compare()
    {
        PauseGameplay();
        _coroutine = ComparisonRoutine();
        StartCoroutine(_coroutine);
    }

    void PauseGameplay()
    {
        _wheat.Pause();
        _finishButton.gameObject.SetActive(false);
    }

    void NextDay(int day)
    {
        _wheat.Reset();
        _skyline.LoadSkyline(day);
    }

    void CalculateExposure(float percent)
    {
        float exposure = percent / _skyline.activeSkyline.percentShadedArea;
        Debug.Log("Percent shaded in view: " + percent + " Exposure: " + exposure);
    }

    void ResumeGameplay()
    {
        _wheat.Resume();
        _finishButton.gameObject.SetActive(true);
    }

    IEnumerator ComparisonRoutine()
    {
        _mover.MoveToComparisonPosition();
        while (_mover.isMoving)
        {
            yield return null;
        }
        _reader.ReadTexture();
        while (_reader.isProcessing)
        {
            yield return null;
        }
        CalculateExposure(_reader.shadedPercent);
        yield return new WaitForSeconds(_proceedLag);
        _mover.MoveToGameplayPosition();
        while (_mover.isMoving)
        {
            yield return null;
        }
        NextDay(0);
        ResumeGameplay();
    }  
}
