using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Outline)), RequireComponent(typeof(Image))]
public class AbilityImage : MonoBehaviour
{
    [SerializeField] private AttackSlot slot = AttackSlot.None;
    [SerializeField] private AttackScriptableObject attack;
    private Outline _outline;
    private Image _image;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;

        _image = GetComponent<Image>();
        _image.sprite = attack.Sprite;
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
}
