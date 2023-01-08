using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private NightScreen _nightScreen;

    [SerializeField]
    private SpriteRenderer _nightLight;

    [SerializeField]
    private float _nightScreenFadeInTime;

    [SerializeField]
    private float _refundStaminaLag;

    [SerializeField]
    private float _feedbackLag;

    [SerializeField]
    private float _feedbackDisplayTime;

    [SerializeField]
    private float _harvestFeedbackLag;

    [SerializeField]
    private float _harvestFeedbackDisplayTime;

    [SerializeField]
    private float _lightTurnOnLag;

    [SerializeField]
    private ScreenFader _dayCard;

    [SerializeField]
    private float _dayCardFadeInTime;

    [SerializeField]
    private TextMeshProUGUI _dayText;

    [SerializeField]
    private float _dayTextDelay;

    [SerializeField]
    private float _dayTextHold;

    [SerializeField]
    private float _dayCardFadeOutTime;

    [SerializeField]
    private Button _finishButton;

    private IEnumerator _coroutine;
    private int _day = 0;
    
    private bool _isWaitingToProceed;

    [SerializeField]
    public WheatMeter _wheatMeter;

    [SerializeField]
    public WheatTarget _target;

    [SerializeField]
    public TextMeshProUGUI _gameOverText;

    [SerializeField]
    private float _gameOverTextDelay;

    [SerializeField]
    private Button _resetButton;

    void Start()
    {
       ResetGame();
    }

    public void ResetGame()
    {
        _gameOverText.gameObject.SetActive(false);
        _resetButton.gameObject.SetActive(false);
        _nightLight.gameObject.SetActive(false);
        _nightScreen.gameObject.SetActive(false);
        _day++;
        _day = 0;
        _mover.Initialize(_wheat, _skyline);
        _wheat.GameReset();
        _stamina.OnGameReset();
        _dayCard.gameObject.SetActive(true);
        StartCoroutine(GameStart());
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
        SetTargetWheat(day);
    }

    void SetTargetWheat(int day)
    {
        float target = _skyline.GetDailyTarget(day);
        _target.SetTarget(target);
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

    void PlayHarvestFeedback()
    {
        float current = _wheatMeter.currentWheat;
        float expected = _target.targetWheat;
        float differential = current - expected;
        _feedback.PlayHarvestText(differential);
    }

    void StopharvestFeedback()
    {
        _feedback.StopHarvestText();
    }

    void SetNightLightColor(float coverage)
    {
        float exposure = 1 - coverage;
        exposure = Mathf.Clamp(exposure, 0f, 1f);
        float alpha = .7f * exposure + .05f;
        _nightLight.color = new Color(1f, 1f, 1f, alpha);
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

    IEnumerator GameStart()
    {
        PauseGameplay();
        NextDay(_day);
        yield return new WaitForSeconds(_dayTextDelay);
        _dayText.gameObject.SetActive(true);
        _dayText.text = "Night " + (_day + 1).ToString();
        yield return new WaitForSeconds(_dayTextHold);
        _dayText.gameObject.SetActive(false);
        _dayCard.FadeIn();
        while (!_dayCard.IsComplete)
        {
            yield return null;
        }
        _dayCard.gameObject.SetActive(false);
        ResumeGameplay();
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

        _nightScreen.gameObject.SetActive(true);
        _nightScreen.TurnOn(_nightScreenFadeInTime);
        while (!_nightScreen.isCompleted)
        {
            yield return null;
        }
        yield return new WaitForSeconds(_lightTurnOnLag);
        _nightLight.gameObject.SetActive(true);
        SetNightLightColor(coverage);

        PlayCoverageFeedback(coverage);

        if (_day == 6)
        {
            StartCoroutine(GameOver());
            yield break;
        }
        yield return new WaitForSeconds(_refundStaminaLag);
        RefundStamina(coverage);
        yield return new WaitForSeconds(_feedbackDisplayTime);
        StopCoverageFeedback();
        yield return new WaitForSeconds(_harvestFeedbackLag);
        PlayHarvestFeedback();
        yield return new WaitForSeconds(_harvestFeedbackDisplayTime);
        StopharvestFeedback();


        // remember, fader is swapped fade in/fade out bc i stupid
        _dayCard.gameObject.SetActive(true);
        _dayCard.FadeOut();
        while (!_dayCard.IsComplete)
        {
            yield return null;
        }
        _nightScreen.TurnOff();
        _nightLight.gameObject.SetActive(false);
        _nightScreen.gameObject.SetActive(false);
        _day++;
        NextDay(_day);
        yield return new WaitForSeconds(_dayTextDelay);
        _dayText.gameObject.SetActive(true);
        _dayText.text = "Night " + (_day + 1).ToString();
        _mover.MoveToGameplayPosition();
        yield return new WaitForSeconds(_dayTextHold);
        _dayText.gameObject.SetActive(false);
        _dayCard.FadeIn();
        while (!_dayCard.IsComplete && _mover.isMoving)
        {
            yield return null;
        }
        _dayCard.gameObject.SetActive(false);
        ResumeGameplay();
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(_feedbackDisplayTime);
        StopCoverageFeedback();
        yield return new WaitForSeconds(_gameOverTextDelay);
        _gameOverText.gameObject.SetActive(true);
        _resetButton.gameObject.SetActive(true);
    }
}
