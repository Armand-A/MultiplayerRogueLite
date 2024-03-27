using EntityDataEnums;
using System;
using System.Collections.Generic;

public class UpgradeRange
{
    public static readonly Dictionary<UpgradeableStatsEnum, UpgradeValues> Common = new Dictionary<UpgradeableStatsEnum, UpgradeValues>
    {
        { UpgradeableStatsEnum.HP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.AP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.HPRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.APRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.Attack, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.Defence, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.CDReduction, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 0.1f, 0.5f), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.Accuracy, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.Evasiveness, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.Crit_Dmg, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.Crit_Rate, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.Speed, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
        { UpgradeableStatsEnum.Luck, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1, 5), new UpgradeValue(ValueTypeEnum.Percentage, 2, 5)) },
    };

    public static readonly Dictionary<UpgradeableStatsEnum, UpgradeValues> Rare = new Dictionary<UpgradeableStatsEnum, UpgradeValues>
    {
        { UpgradeableStatsEnum.HP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.AP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.HPRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.APRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.Attack, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.Defence, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.CDReduction, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 0.5f, 1f), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.Accuracy, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.Evasiveness, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.Crit_Dmg, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.Crit_Rate, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.Speed, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
        { UpgradeableStatsEnum.Luck, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 5, 10), new UpgradeValue(ValueTypeEnum.Percentage, 5, 10)) },
    };

    public static readonly Dictionary<UpgradeableStatsEnum, UpgradeValues> Epic = new Dictionary<UpgradeableStatsEnum, UpgradeValues>
    {
        { UpgradeableStatsEnum.HP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.AP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.HPRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.APRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.Attack, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.Defence, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.CDReduction, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 1f, 2f), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.Accuracy, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.Evasiveness, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.Crit_Dmg, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.Crit_Rate, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.Speed, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 50, 100), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
        { UpgradeableStatsEnum.Luck, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 10, 20), new UpgradeValue(ValueTypeEnum.Percentage, 10, 20)) },
    };

    public static readonly Dictionary<UpgradeableStatsEnum, UpgradeValues> Legendary = new Dictionary<UpgradeableStatsEnum, UpgradeValues>
    {
        { UpgradeableStatsEnum.HP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.AP, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.HPRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.APRegen, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.Attack, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.Defence, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.CDReduction, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 2f, 5f), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.Accuracy, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.Evasiveness, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.Crit_Dmg, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.Crit_Rate, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.Speed, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 100, 200), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
        { UpgradeableStatsEnum.Luck, new UpgradeValues(new UpgradeValue(ValueTypeEnum.Flat, 20, 50), new UpgradeValue(ValueTypeEnum.Percentage, 20, 50)) },
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