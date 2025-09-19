using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] protected Image _iconImage;
    [SerializeField] protected TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _descriptionText;

    public virtual void Initialize(ItemSO itemSo)
    {
        _iconImage.sprite = itemSo.Icon;
        _nameText.text = itemSo.Name;
        _descriptionText.text = itemSo.Description;
    }
}
