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
    private StaminaManager _stamina;

    [SerializeField]
    private FeedbackUI _feedback;

    [SerializeField]
    private float _proceedLag;

    [SerializeField]
    private float _refundStaminaLag;

    [SerializeField]
    private float _feedbackLag;

    [SerializeField]
    private float _feedbackDisplayTime;

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
        _stamina.OnGameReset();
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

    float CalculateCoverage(float percent)
    {
        float exposure = percent / _skyline.activeSkyline.percentShadedArea;
        return 1 - exposure;
    }

    void PlayCoverageText(float coverage)
    {
        _feedback.PlayText(coverage);
    }

    void StopCoverageText()
    {
        _feedback.StopText();
    }

    void PlayCoverageFeedback(float coverage)
    {
        _feedback.PlayFeedbackText(coverage);
    }

    void StopCoverageFeedback()
    {
        _feedback.StopFeedbackText();
    }


    void RefundStamina(float coverage)
    {
        coverage = Mathf.Clamp(coverage, 0f, 1f);
        _stamina.RefundStamina(coverage);
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
        float coverage = CalculateCoverage(_reader.shadedPercent);
        yield return new WaitForSeconds(_proceedLag);
        PlayCoverageText(coverage);
        yield return new WaitForSeconds(_feedbackLag);
        StopCoverageText();
        PlayCoverageFeedback(coverage);
        yield return new WaitForSeconds(_refundStaminaLag);
        RefundStamina(coverage);
        yield return new WaitForSeconds(_feedbackDisplayTime);
        StopCoverageFeedback();
        _mover.MoveToGameplayPosition();
        while (_mover.isMoving)
        {
            yield return null;
        }
        NextDay(0);
        ResumeGameplay();
    }  
}
