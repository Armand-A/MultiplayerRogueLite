
public class RegenResourceTemplate : ResourceTemplate
{
    private float regenValue = 1;

    public RegenResourceTemplate(float value) : base(value)
    {
        Value = value;
        TotalValue = value;
    }

    public void UpdateResource(float value, float regen)
    {
        TotalValue = value;
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
    public Health(float value) : base(value)
    {
        Value = value;
        TotalValue = value;
    }

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
    public Action(float value) : base(value)
    {
        Value = value;
        TotalValue = value;
    }
}