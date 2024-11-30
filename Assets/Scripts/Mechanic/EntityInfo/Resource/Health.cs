using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : ResourceTemplate
{
    public override bool Add(float value)
    {
        if (_currentValue + value <= _totalValue)
        {
            _currentValue += value;
            return true;
        } else
        {
            _currentValue = _totalValue;
            return false;
        }
    }

    public override bool Remove(float value)
    {
        if (_currentValue - value >= 0)
        {
            _currentValue -= value;
            return true;
        } else
        {
            _currentValue = 0;
            return false;
        }
    }
}
