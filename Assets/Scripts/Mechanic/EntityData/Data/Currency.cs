using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int Value = 100;

    /// <summary>
    /// Transaction succeeded or not
    /// different from UpdateValue() because it doesn't have a maximum value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual bool Transaction(int value)
    {
        if (value < 0 && Value + value < 0)
        {
            return false;
        }
        else
        {
            Value += value;
        }
        return true;
    }
}
