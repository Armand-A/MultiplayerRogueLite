using EntityDataEnums;
using System;
using System.Collections.Generic;

public class UpgradeRange
{
    public static readonly Dictionary<StatsEnum, UpgradeValues> Common = new Dictionary<StatsEnum, UpgradeValues>
    {
        { StatsEnum.HP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.AP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.HPRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.APRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.CDReduction, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 0.1f, 0.5f), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.Accuracy, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.Evasiveness, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.Crit_Dmg, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.Crit_Rate, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.Speed, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { StatsEnum.Luck, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
    };

    public static readonly Dictionary<StatsEnum, UpgradeValues> Rare = new Dictionary<StatsEnum, UpgradeValues>
    {
        { StatsEnum.HP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.AP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.HPRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.APRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.CDReduction, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 0.5f, 1f), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.Accuracy, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.Evasiveness, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.Crit_Dmg, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.Crit_Rate, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.Speed, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { StatsEnum.Luck, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
    };

    public static readonly Dictionary<StatsEnum, UpgradeValues> Elite = new Dictionary<StatsEnum, UpgradeValues>
    {
        { StatsEnum.HP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.AP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.HPRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.APRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.CDReduction, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1f, 2f), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.Accuracy, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.Evasiveness, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.Crit_Dmg, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.Crit_Rate, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.Speed, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { StatsEnum.Luck, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
    };

    public static readonly Dictionary<StatsEnum, UpgradeValues> Legendary = new Dictionary<StatsEnum, UpgradeValues>
    {
        { StatsEnum.HP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.AP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.HPRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.APRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.CDReduction, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 2f, 5f), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.Accuracy, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.Evasiveness, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.Crit_Dmg, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.Crit_Rate, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.Speed, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { StatsEnum.Luck, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
    };

    public class UpgradeValues
    {
        public UpgradeValue[] Values = new UpgradeValue[Enum.GetNames(typeof(ValueTypeEnum)).Length];

        public UpgradeValues(UpgradeValue flat, UpgradeValue percentage)
        {
            Values[(int) ValueTypeEnum.Flat] = flat;
            Values[(int) ValueTypeEnum.Percentage] = percentage;
        }
    }

    public class UpgradeValue
    {
        public ValueTypeEnum ValueType;
        public float Min;
        public float Max;

        public UpgradeValue(ValueTypeEnum type, float min, float max)
        {
            ValueType = type;
            Min = min;
            Max = max;
        }
    }
}