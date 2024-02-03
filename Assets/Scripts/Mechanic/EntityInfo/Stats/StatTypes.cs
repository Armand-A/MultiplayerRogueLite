
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

    public enum Stats
    {
        HP = 0,
        AP = 1,
        Attack = 200,
        Defence = 300,
        Atk_Spd = 4,
        Accuracy = 5,
        Evasiveness = 6,
        Crit_Dmg = 7,
        Crit_Rate = 8,
        Speed = 9,
    }

    public enum PlayerStat
    {
        Luck,
        Currency,
        Invincibility_Time_Frame
    }

    public enum ElementTypes
    {
        Normal = 0,
        Fire,
        Water,
        Earth,
        Air
    }
}