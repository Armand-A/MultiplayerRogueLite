using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : AttributeTemplate
{
    /// <summary>
    /// Changes value of health
    /// different from parent method by allowing change in value 
    /// that can go below 0 by forcing the overall value to become 0
    /// </summary>
    /// <param name="value">must use negative value to remove health</param>
    /// <returns></returns>
    /// 
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
