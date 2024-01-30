
public class StatTypes
{
    public enum ValueType
    {
        Flat,
        Percentage
    }

    public enum CalculationOrder
    {
        Flat = 100,
        FlatMulti = 200,
        PercentMulti = 300
    }

    public enum Stat
    {
        HP,
        AP,
        Speed,
        Attack,
        Atk_Spd,
        Defence,
        Hit_Rate,
        Dodge_Rate,
        Crit_Rate,
        Crit_Atk
    }

    public enum PlayerStat
    {
        Luck,
        Currency,
        Invincibility_Time_Frame
    }

    public enum ElementTypes
    {
        Fire,
        Water,
        Earth,
        Air
    }
}