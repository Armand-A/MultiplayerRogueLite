using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency : MonoBehaviour
{
    public int Value = 100;

    /// <summary>
    /// Transaction succeeded or not
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
