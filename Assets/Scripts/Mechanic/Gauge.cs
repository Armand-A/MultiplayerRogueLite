using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gauge : MonoBehaviour
{
    protected Slider _gauge;

    [Header("Bar UI")]
    [Tooltip("Base Bar to show cost")]
    public Image Fill;
    [Tooltip("Rect Transform of Fill")]
    public RectTransform RectFill;
    // [Tooltip("Color of fill gauge")]
    // public Image Cost;
    [Tooltip("Rect Transform of Cost")]
    public RectTransform RectCost;
    // [Tooltip("Bar color")]
    // public Color FillColor;
    // [Tooltip("Cost preview color")]
    // public Color CostColor;
    [Tooltip("Color for when cost is beyond available points")]
    public Color DeniedColor;
    [Tooltip("Value text")]
    public TMP_Text ValueText;
    [Tooltip("Notifying text")]
    public GameObject NotiText;
    [Tooltip("Not enough points notification duration")]
    public float NotiDuration = 2.0f;
    
    //protected CooldownTimer _notiTimer;

    protected float _currentValue = 0;
    
    protected bool _isPreviewing = false;
    protected float _previewCost = 0;

    protected Color _fillColor;
    //protected Color _costColor;

    private void Awake()
    {
        _gauge = GetComponent<Slider>();
        _fillColor = Fill.color;
        //_costColor = Cost.color;
    }

    public void UpdateValue(float newValue, float totalValue)
    {
        _currentValue = newValue;
        _gauge.maxValue = totalValue;
        UpdateBar();
    }

    /// <summary>
    /// Preview of cost of action
    /// </summary>
    /// <param name="previewing">true if preview is desired, else no</param>
    /// <param name="value">preview cost value</param>
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

    protected void UpdateBar()
    {
        float previewTotal = _currentValue - _previewCost;

        _gauge.fillRect = RectCost;
        _gauge.value = _isPreviewing ? previewTotal : _currentValue;
        
        _gauge.fillRect = RectFill;
        _gauge.value = _currentValue;

        if (previewTotal < 0)
        {
            //DisplayNoti(true);
            Fill.color = DeniedColor;
        }
        else
        {
            //DisplayNoti(false);
            Fill.color = _fillColor;
        }
        if (ValueText != null)
            ValueText.text = _currentValue + " / " + _gauge.maxValue;
    }
}
