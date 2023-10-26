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
    private AttackScriptableObject _attack;
    private Outline _outline;
    private Image _image;

    private void OnEnable()
    {
        _abilities = FindObjectOfType<PlayerAbilities>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _image = GetComponent<Image>();

        UpdateAttack();
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

    public void OnPlayerEquipAttack(AttackSlot eventSlot)
    {
        if (slot == AttackSlot.None) return;
        if (slot != eventSlot) return;

        _outline.enabled = true;
    }

    public void OnPlayerUnequipAttack(AttackSlot eventSlot)
    {
        _outline.enabled = false;
    }

    public void OnChangeEquippedAbility()
    {
        UpdateAttack();
    }
}
