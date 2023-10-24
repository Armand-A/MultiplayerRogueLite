using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionGauge : MonoBehaviour
{
    private Slider _actionGauge;

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
    
    private CooldownTimer _notiTimer;

    private float _currentActionValue = 0;
    
    private bool _isPreviewing = false;
    private float _previewCost = 0;

    /// <summary>
    /// Initialize values
    /// Create timer for action recharge
    /// </summary>
    private void Awake()
    {
        _actionGauge = GetComponent<Slider>();
        //_notiTimer = new CooldownTimer(NotiDuration);
    }

    /*
    private void OnEnable()
    {
        _notiTimer.TimerCompleteEvent += DisableNoti;
    }

    private void OnDisable()
    {
        _notiTimer.TimerCompleteEvent -= DisableNoti;
    }
    */

    public void UpdateValue(float newActionValue, float totalActionValue)
    {
        _currentActionValue = newActionValue;
        _actionGauge.maxValue = totalActionValue;
    }

    private void Update()
    {
        //_notiTimer.Update(Time.deltaTime);
        UpdateBar();
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
    
    /// <summary>
    /// Disable lacking point notification
    /// </summary>
    private void DisplayNoti(bool status)
    {
        if (NotiText.activeSelf == status)
            return;
        NotiText.SetActive(status);
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
            DisplayNoti(true);
            Fill.color = DeniedColor;
        }
        else
        {
            DisplayNoti(false);
            Fill.color = FillColor;
        }

        ActionText.text = _currentActionValue + " / " + _actionGauge.maxValue;
    }
}
