using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;

    public void Initialize(ItemSO itemSo)
    {
        _iconImage.sprite = itemSo.Icon;
        _nameText.text = itemSo.Name;
        _descriptionText.text = itemSo.Description;
        _priceText.text = itemSo.Price.ToString();
    }
}
