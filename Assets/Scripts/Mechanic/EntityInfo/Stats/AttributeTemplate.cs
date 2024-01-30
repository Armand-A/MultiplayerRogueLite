using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTemplate : MonoBehaviour
{

    // Health, Mana, Action values
    public float Value;

    [Header("Attribute values")]
    [Tooltip("Default base value")]
    [SerializeField] protected float _baseValue = 20;
    [Tooltip("Current total value")]
    [SerializeField] protected float _totalValue = 20;
    [Tooltip("Minimum value")]
    [SerializeField] protected float _minValue = 100;
    [Tooltip("Max value")]
    [SerializeField] protected float _maxValue = 100;

    [Header("Attribute Recharge values")]
    [SerializeField] protected bool RechargeIsActive = false;
    [Tooltip("How much value is recharged each interval")]
    [SerializeField] protected float _baseRechargeValue = 2.0f;
    [Tooltip("Recharge interval durations")]
    [SerializeField] protected float RechargeInterval = 1.0f;
    [Tooltip("Recharge quantity per interval")]
    [SerializeField] protected float RechargeRate = 2.0f;
    [Tooltip("Recharge buff/debuff")]
    [SerializeField] private float RechargeModifier = 0;

    public float TotalValue {get{return _totalValue;}}
    //Positive or negative addition to total value (Buff/debuff)
    private float _addOnValue;

    private CooldownTimer _rechargeTimer;
    private bool _inCombat = false;
    

    private void Awake()
    {
        UpdateTotalValue();
        if (Value == 0)
        {
            Value = _totalValue;
        }

        _rechargeTimer = new CooldownTimer(RechargeInterval, true);
    }

    private void OnEnable()
    {
        _rechargeTimer.TimerCompleteEvent += Recharge;
    }

    private void OnDisable()
    {
        _rechargeTimer.TimerCompleteEvent -= Recharge;
    }
    
    private void Update()
    {
        if (RechargeIsActive)
        {
            _rechargeTimer.Update(Time.deltaTime);
            RechargeCheck();
        }

        //ActionGaugeObject.UpdateValue(Value, _totalValue);
    }

    /// <summary>
    /// Adjusts value quantity
    /// </summary>
    /// <param name="value"> Must use negative numbers to subtract value </param>
    /// <returns>Tells result if value was used or not</returns>
    public virtual bool UpdateValue(float value)
    {
        if (value < 0 && Value + value < 0)
        {
            return false;
        }
        else
        {
            if (Value + value > _totalValue)
                Value = _totalValue;
            else
                Value += value;
        }
        return true;
    }

    /// <summary>
    /// Activates whenever the recharge timer completes a turn
    /// </summary>
    protected virtual void Recharge()
    {
        if (Value < _totalValue)
        {
            UpdateValue(RechargeRate + RechargeModifier);
        }
    }

    /// <summary>
    /// Decide if value should continue recharging
    /// </summary>
    protected virtual void RechargeCheck()
    {
        if (Value >= _totalValue && _rechargeTimer.IsActive)
        {
            _rechargeTimer.Pause();
        }
        else if (Value < _totalValue && !_rechargeTimer.IsActive)
        {
            _rechargeTimer.Start();
        }
    }

    /// <summary>
    /// Changes the maximum value of the attribute
    /// </summary>
    /// <param name="value"></param>
    public void UpdateTotalValue(float value = 0)
    {
        float tempAddOnValue = _addOnValue + value;
        if (tempAddOnValue + _baseValue < 0)
            _addOnValue = 0;

        _totalValue = _baseValue + _addOnValue;
    }

    /// <summary>
    /// setter for _incombat variable
    /// </summary>
    /// <param name="inCombat"></param>
    public virtual void InCombat(bool inCombat)
    {
        _inCombat = inCombat;
    }

    /// <summary>
    /// Changes the recharge rate modifier
    /// </summary>
    /// <param name="rate"></param>
    public virtual bool UpdateRechargeRate(float rate)
    {
        float tempRechargeRate = RechargeRate + RechargeModifier + rate;
        if (0 > tempRechargeRate)
        {
            return false;
        }
        else
        {
            RechargeRate = tempRechargeRate;
        }
        return true;
    }

    /// <summary>
    /// Returns the recharge rate back to normal
    /// </summary>
    public void ResetRechargeRate()
    {
        RechargeModifier = 0;
    }
}