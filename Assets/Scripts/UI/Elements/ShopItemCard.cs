using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCard : ItemCard
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TextMeshProUGUI _priceText;
    
    public override void Initialize(ItemSO itemSo)
    {
        base.Initialize(itemSo);
        
        _priceText.text = "$" + itemSo.Price.ToString();
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyClick);
    }
    
    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyClick);
    }

    private void OnBuyClick()
    {
        
    }
}
