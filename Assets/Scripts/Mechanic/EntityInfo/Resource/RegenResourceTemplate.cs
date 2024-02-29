
public class RegenResourceTemplate : ResourceTemplate
{
    private float regenValue = 1;

    public void UpdateResource(float value, float regen = 1)
    {
        value = TotalValue;
        if (Value > TotalValue)
            Value = TotalValue;
        regenValue = regen;
    }
    public void Regen(float _rechargeInterval)
    {
        if (0 != _rechargeInterval)
            Add(regenValue * _rechargeInterval);
        else
            Add(regenValue);
    }
}

public class Health : RegenResourceTemplate
{
    public override bool Remove(float value)
    {
        if (_currentValue - value > 0)
        {
            _currentValue -= value;
            return true;
        }
        else
        {
            _currentValue = 0;
            return false;
        }
    }
}

public class Action : RegenResourceTemplate
{
}