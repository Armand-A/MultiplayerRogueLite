using EntityDataEnums;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfoDisplay : MonoBehaviour
{
    public Image RarityColor;

    public void DisplayNewUpgrade(Upgrade upgrade)
    {
        SetRarityColor(upgrade.GivenRarity);
    }

    private void SetRarityColor(Rarity rarity)
    {
        switch(rarity)
        {
            case Rarity.Common:
                RarityColor.color = new Color(0, 0, 0, 0);
                break;
            case Rarity.Rare:
                RarityColor.color = new Color(0, 0, 0, 0);
                break;
            case Rarity.Epic:
                RarityColor.color = new Color(0, 0, 0, 0);
                break;
            case Rarity.Legendary:
                RarityColor.color = new Color(0, 0, 0, 0);
                break;
            default:
                break;
        }
    }
}