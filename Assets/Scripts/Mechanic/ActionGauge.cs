using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionGauge : MonoBehaviour
{
    private Slider _actionGauge;

    [Header("Action Variables")]
    [SerializeField]
    private float _currentActionValue = 0;
    [Tooltip("Base total action value")]
    public float BaseActionValue = 20;
    [Tooltip("Positive or negative action value boost")]
    public float BonusActionValue = 0;
    [Tooltip("Max action value")]
    public float TotalActionValue = 100;
    [Tooltip("Action recharge value per interval")]
    public float ActionRechargeInterval = 10.0f;
    [Tooltip("Action recharge value per interval")]
    public float ActionRechargeRate = 1.0f;

    [Header("Bar UI")]
    [Tooltip("Base Bar to show cost")]
    public Image Fill;
    [Tooltip("Rect Transform of Fill")]
    public RectTransform RectFill;
    [Tooltip("Color of fill gauge")]
    public Image Cost;
    [Tooltip("Rect Transform of Cost")]
    public RectTransform RectCost;
    [Tooltip("Parent bar ")]
    public Color FillColor;
    [Tooltip("Action cost preview color")]
    public Color CostColor;
    [Tooltip("Color for when cost is beyond available points")]
    public Color DeniedColor;
    // [Tooltip("Gradient for cost of action")]
    // public Gradient ActionGradient;
    [Tooltip("Action value text")]
    public TMP_Text ActionText;
    [Tooltip("Notifying text")]
    public GameObject NotiText;
    [Tooltip("Not enough action point notification duration")]
    public float NotiDuration = 2.0f;
    
    private CooldownTimer _actionRechargeTimer;
    private CooldownTimer _notiTimer;

    private bool _isPreviewing = false;
    private float _previewCost = 0;

    /// <summary>
    /// Initialize values
    /// Create timer for action recharge
    /// </summary>
    private void Awake()
    {
        _actionGauge = GetComponent<Slider>();
        UpdateTotalActionValue();
        TotalActionValue = BaseActionValue + BonusActionValue;

        // In case this is adjusted from save file
        if (_currentActionValue == 0)
        {
            _currentActionValue = TotalActionValue;
        }

        _actionRechargeTimer = new CooldownTimer(ActionRechargeInterval, true);
        _notiTimer = new CooldownTimer(NotiDuration);
    }

    private void OnEnable()
    {
        _actionRechargeTimer.TimerCompleteEvent += RechargeActionPoint;
        _notiTimer.TimerCompleteEvent += DisableNoti;
    }

    private void OnDisable()
    {
        _actionRechargeTimer.TimerCompleteEvent -= RechargeActionPoint;
        _notiTimer.TimerCompleteEvent -= DisableNoti;
    }

    private void Update()
    {
        _actionRechargeTimer.Update(Time.deltaTime);
        _notiTimer.Update(Time.deltaTime);

        ActionRechargeCheck();
        UpdateBar();
    }

    /// <summary>
    /// Adjusts Action value remaining
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Tells result if action was used or not</returns>
    public bool UpdateActionValue(float value)
    {
        if (value < 0 && _currentActionValue + value < 0)
        {
            _notiTimer.Start();
            NotiText.SetActive(true);
            return false;
        }
        else
        {
            if (_currentActionValue + value > TotalActionValue)
                _currentActionValue = TotalActionValue;
            else
                _currentActionValue += value;
        }
        return true;
    }

    /// <summary>
    /// Makes the gauge show the estimated cost of action
    /// </summary>
    /// <param name="value">Cost of action</param>
    public void CostPreview(bool previewing, float value = 0)
    {
        _previewCost = value;
        _isPreviewing = previewing;
    }

    public void UpdateTotalActionValue(float value = 0)
    {
        BonusActionValue += value;
        if (BonusActionValue < 0)
            BonusActionValue = 0;

        TotalActionValue = BaseActionValue + BonusActionValue;
        _actionGauge.maxValue = TotalActionValue;
    }

    /// <summary>
    /// Activates whenever the recharge timer completes a turn
    /// </summary>
    private void RechargeActionPoint()
    {
        if (_currentActionValue < TotalActionValue)
        {
            UpdateActionValue(ActionRechargeRate);
        }
    }

    /// <summary>
    /// Decide if action points should continue recharging
    /// </summary>
    private void ActionRechargeCheck()
    {
        if (_currentActionValue >= TotalActionValue && _actionRechargeTimer.IsActive)
        {
            _actionRechargeTimer.Pause();
        }
        else if (_currentActionValue < TotalActionValue && !_actionRechargeTimer.IsActive)
        {
            _actionRechargeTimer.Start();
            
        }
    }
    /// <summary>
    /// Disable lacking point notification
    /// </summary>
    private void DisableNoti()
    {
        NotiText.SetActive(false);
    }

    private void UpdateBar()
    {
        float previewTotal = _currentActionValue + _previewCost;

        _actionGauge.fillRect = RectCost;
        _actionGauge.value = _isPreviewing ? previewTotal : _currentActionValue;
        
        _actionGauge.fillRect = RectFill;
        _actionGauge.value = _currentActionValue;

        if (previewTotal < 0)
        {
            Fill.color = DeniedColor;
        }
        else
        {
            Fill.color = FillColor;
        }

        ActionText.text = _currentActionValue + " / " + TotalActionValue;
    }
}
