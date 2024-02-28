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