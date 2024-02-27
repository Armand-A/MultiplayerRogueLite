using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareUpgradeRollUI : AbilityUI
{
    [Header("Main fields")]
    [SerializeField] RandomPrefabPoolSO upgradePool;
    [SerializeField, Range(1, 6)] int cardsPerRoll;

    [Header("References")]
    [SerializeField] GameObject abilityCardPrefab;
    [SerializeField] RectTransform cardsContainer;

    List<GameObject> upgrades;

    new void Start()
    {
        base.Start(); 

        // roll upgrades from pool
        upgrades = upgradePool.Get(cardsPerRoll);
        if (upgrades == null || upgrades.Count == 0)
        {
            uiManager.CloseUI();
            return;
        }

        // precalculate formatting parameters
        float containerX = cardsContainer.anchoredPosition.x;
        float containerY = cardsContainer.anchoredPosition.y;
        float containerWidth = cardsContainer.rect.width;
        float cardWidth = abilityCardPrefab.GetComponent<RectTransform>().rect.width;
        int numCards = upgrades.Count;
        float totalGap = Mathf.Max(containerWidth - cardWidth * numCards, 0);
        float gap = totalGap / (numCards + 1);
        float offset = (cardWidth + gap) / 2;

        // update UI
        for (int i = 0; i < upgrades.Count; i++)
        {
            Ability ability = upgrades[i].GetComponent<Ability>();
            GameObject abilityCardObject = Instantiate(abilityCardPrefab);
            abilityCardObject.transform.SetParent(cardsContainer);

            // procedurally position cards
            // space-around
            RectTransform abilityCardRectTransform = abilityCardObject.GetComponent<RectTransform>();
            Vector2 generatedPosition;
            if (numCards % 2 == 0)
            {
                // case even
                if (i < numCards / 2)
                {
                    // left side
                    float x = containerX - offset - (numCards / 2 - 1 - i) * 2 * offset;
                    generatedPosition = new Vector2(x, containerY);
                }
                else
                {
                    // right side
                    float x = containerX + offset + (i - numCards / 2) * 2 * offset;
                    generatedPosition = new Vector2(x, containerY);
                }
            }
            else
            {
                // case odd
                if (i == numCards / 2)
                {
                    // middle
                    generatedPosition = new Vector2(containerX, containerY);
                }
                else if (i < numCards / 2)
                {
                    // left
                    float x = containerX - (numCards / 2 - i) * 2 * offset;
                    generatedPosition = new Vector2(x, containerY);
                }
                else
                {
                    // right
                    float x = containerX + (i - numCards / 2) * 2 * offset;
                    generatedPosition = new Vector2(x, containerY);
                }
            }
            abilityCardRectTransform.anchoredPosition = generatedPosition;

            // initialize ability card references
            AbilityCard abilityCard = abilityCardObject.GetComponent<AbilityCard>();
            abilityCard.SetAbility(ability);
            abilityCard.RegisterOnClickListener(OnCardClicked);
        }
    }

    void OnCardClicked(Ability ability)
    {
        PlayerAbilities playerAbilities = FindObjectOfType<PlayerAbilities>();
        if (playerAbilities == null) return;

        upgradePool.Ban(ability.gameObject);
        playerAbilities.UnlockAbility(ability);
        uiManager.CloseUI();
    }
}
