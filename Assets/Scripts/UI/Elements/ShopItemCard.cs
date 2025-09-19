using Items;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCard : ItemCard
{
    [SerializeField] protected TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Button _buyButton;
    
    private ShopManager _shopManager;
    
    public void Initialize(ItemSO itemSO, ShopManager shopManager)
    {
        base.Initialize(itemSO);
        
        _shopManager = shopManager;
        
        _descriptionText.text = itemSO.Description;
        _priceText.text = "$" + itemSO.Price.ToString();
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
        if (_shopManager.TryPurchaseItem(_itemSO) && _itemSO.IsInfinite)
        {
            Destroy(gameObject);
        }
    }
}
