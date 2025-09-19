using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    public ItemSO ItemSO => _itemSO;
    
    [SerializeField] protected ItemSO _itemSO;
    [SerializeField] protected Image _iconImage;
    [SerializeField] protected TextMeshProUGUI _nameText;

    public virtual void Initialize(ItemSO itemSO)
    {
        _itemSO = itemSO;
        _iconImage.sprite = itemSO.Icon;
        _nameText.text = itemSO.Name;
    }
}
