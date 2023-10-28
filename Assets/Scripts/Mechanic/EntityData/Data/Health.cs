using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : AttributeTemplate
{
    public override bool UpdateValue(float value)
    {
        if (value < 0 && Value + value < 0)
        {
            Value = 0;
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
}
