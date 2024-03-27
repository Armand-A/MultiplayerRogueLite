using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityDataEnums
{
    public class DataEnumNames
    {
        public static List<StatsEnum> StatNames = Enum.GetValues(typeof(StatsEnum)).Cast<StatsEnum>().ToList();
        public static List<ElementTypesEnum> ElementNames = Enum.GetValues(typeof(ElementTypesEnum)).Cast<ElementTypesEnum>().ToList();
    }
    public enum ValueTypeEnum
    {
        Flat,
        Percentage
    }

    public enum CalculationOrderEnum
    {
        Flat = 100,
        FlatMulti = 200,
        PercentMulti = 300
    }

    public enum StatsEnum
    {
        HP = 0,
        AP = 1,
        HPRegen = 2,
        APRegen = 3,
        Attack = 200,
        Defence = 300,
        CDReduction = 4,
        Accuracy = 5,
        Evasiveness = 6,
        Crit_Dmg = 7,
        Crit_Rate = 8,
        Speed = 9,
        Luck = 10, //luck for upgrade draws
    }

    public enum UpgradeableStatsEnum
    {
        HP,
        AP,
        HPRegen,
        APRegen,
        Attack,
        Defence,
        CDReduction,
        Accuracy,
        Evasiveness,
        Crit_Dmg,
        Crit_Rate,
        Speed,
        Luck
    }

    public enum ResourceEnum
    {
        HP,
        AP,
        Currency,
    }

    public enum ElementTypesEnum
    {
        Normal = 0,
        Fire,
        Water,
        Earth,
        Air
    }

    public enum StatStatusEnum
    {
        Min,
        Normal,
        Max
    }
}