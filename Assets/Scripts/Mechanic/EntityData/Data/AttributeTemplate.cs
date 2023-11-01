using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTemplate : MonoBehaviour
{
    // Health, Mana, Action values
    public float Value;

    [Header("Attribute values")]
    [Tooltip("Default base value")]
    public float BaseValue = 20;
    [Tooltip("Positive or negative addition to base value (Buff/debuff)")]
    private float AddOnValue;
    [Tooltip("Max allowed value for the value")]
    public float MaxValue = 100;
    [Tooltip("Final Calculated total")]
    public float TotalValue;
    [Tooltip("Recharge interval durations")]
    public float RechargeInterval = 1.0f;
    [Tooltip("Recharge quantity per interval")]
    public float RechargeRate = 2.0f;
    [Tooltip("In combat recharge rate modifier (Should be equal or above RechargeRate)")]
    public float RechargeModifier = 1;

    protected float _defaultRechargeValue = 2.0f;

    private CooldownTimer _rechargeTimer;
    private bool _inCombat = false;
    

    private void Awake()
    {
        UpdateTotalValue();
        if (Value == 0)
        {
            Value = TotalValue;
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
        _rechargeTimer.Update(Time.deltaTime);

        //ActionGaugeObject.UpdateValue(Value, TotalValue);
        RechargeCheck();
    }

    /// <summary>
    /// Adjusts value quantity
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Tells result if value was used or not</returns>
    public virtual bool UpdateValue(float value)
    {
        if (value < 0 && Value + value < 0)
        {
            return false;
        }
        else
        {
            if (Value + value > TotalValue)
                Value = TotalValue;
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
        if (Value < TotalValue)
        {
            UpdateValue(RechargeRate - RechargeModifier);
        }
    }

    /// <summary>
    /// Decide if value should continue recharging
    /// </summary>
    protected virtual void RechargeCheck()
    {
        if (Value >= TotalValue && _rechargeTimer.IsActive)
        {
            _rechargeTimer.Pause();
        }
        else if (Value < TotalValue && !_rechargeTimer.IsActive)
        {
            _rechargeTimer.Start();
        }
    }

    public void UpdateTotalValue(float value = 0)
    {
        AddOnValue += value;
        if (AddOnValue < 0)
            AddOnValue = 0;

        TotalValue = BaseValue + AddOnValue;
    }

    public virtual void InCombat(bool inCombat)
    {
        _inCombat = inCombat;
    }

    public void ResetRechargeRate()
    {
        RechargeRate = _defaultRechargeValue;
    }
}
