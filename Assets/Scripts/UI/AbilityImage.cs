using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Outline)), RequireComponent(typeof(Image))]
public class AbilityImage : MonoBehaviour
{
    [SerializeField] private AttackSlot slot = AttackSlot.None;
    private PlayerAbilities _abilities;
    private PlayerAttack _playerAttack;
    private AttackScriptableObject _attack;
    private Outline _outline;
    private Image _image;

    private UnityAction<AttackSlot> _equipAction;
    private UnityAction _unequipAction;

    private void OnEnable()
    {
        _equipAction = (AttackSlot attackSlot) => OnPlayerEquipAttack(attackSlot);
        _unequipAction = () => OnPlayerUnequipAttack();

        _playerAttack = FindObjectOfType<PlayerAttack>();
        _playerAttack.AddEquipListener(_equipAction);
        _playerAttack.AddUnequipListener(_unequipAction);

        _abilities = FindObjectOfType<PlayerAbilities>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _image = GetComponent<Image>();

        UpdateAttack();
    }

    private void OnDisable()
    {
        _playerAttack.RemoveEquipListener(_equipAction);
        _playerAttack.RemoveUnequipListener(_unequipAction);
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

    private void OnPlayerEquipAttack(AttackSlot eventSlot)
    {
        if (slot == AttackSlot.None) return;
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
