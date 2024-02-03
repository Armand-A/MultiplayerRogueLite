using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Outline)), RequireComponent(typeof(Image))]
public class AbilityImage : MonoBehaviour
{
    [SerializeField] private HotbarAbilitySlot slot = HotbarAbilitySlot.None;
    [SerializeField] private GameObject cooldownOverlay;
    private PlayerAbilities _abilities;
    private PlayerAttack _playerAttack;
    private Ability _attack;
    private Outline _outline;
    private Image _image;

    private UnityAction<HotbarAbilitySlot> _equipAction;
    private UnityAction _unequipAction;

    private void Start()
    {
        _equipAction = (HotbarAbilitySlot attackSlot) => OnPlayerEquipAttack(attackSlot);
        _unequipAction = () => OnPlayerUnequipAttack();

        _playerAttack = FindObjectOfType<PlayerAttack>();
        _playerAttack.AddStartPreviewAbilityListener(_equipAction);
        _playerAttack.AddEndPreviewAbilityListener(_unequipAction);

        _abilities = FindObjectOfType<PlayerAbilities>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _image = GetComponent<Image>();

        UpdateAttack();
    }

    private void OnDisable()
    {
        if (_playerAttack != null)
        {
            _playerAttack.RemoveStartPreviewAbilityListener(_equipAction);
            _playerAttack.RemoveEndPreviewAbilityListener(_unequipAction);
        }
    }

    private void Update()
    {
        if (slot != HotbarAbilitySlot.None)
        {
            Vector3 overlayScale = cooldownOverlay.transform.localScale;
            overlayScale.y = 1 - _abilities.GetAbilityCooldownPercentage((int)slot);
            cooldownOverlay.transform.localScale = overlayScale;
        }
    }

    private void UpdateAttack()
    {
        if (_abilities.EquippedAbilities.Count > (int)slot)
        {
            _attack = _abilities.EquippedAbilities[(int)slot];
        }

        if (_attack != null)
        {
            _image.sprite = _attack.Sprite;
        }
    }

    private void OnPlayerEquipAttack(HotbarAbilitySlot eventSlot)
    {
        if (slot == HotbarAbilitySlot.None) return;
        if (slot != eventSlot) return;

        _outline.enabled = true;
    }

    private void OnPlayerUnequipAttack()
    {
        _outline.enabled = false;
    }

    public void OnChangeEquippedAbility()
    {
        UpdateAttack();
    }
}
