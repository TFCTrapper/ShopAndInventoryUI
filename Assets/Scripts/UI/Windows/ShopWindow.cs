using Inventory;
using TMPro;
using UI.Windows.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ShopWindow : Window
    {
        [SerializeField] private Button _mainWindowButton;
        [SerializeField] private TextMeshProUGUI _currencyCountText;
        [SerializeField] private GameObject _itemCardPrefab;
        [SerializeField] private Transform _itemsCardsParent;
        
        private ShopWindowData _shopWindowData;
        
        public override void Show(UIElementData data = null, bool immediately = false)
        {
            base.Show(data, immediately);

            if (data != null && _shopWindowData != data)
            {
                _shopWindowData = data as ShopWindowData;
                _shopWindowData.ShopManager.CurrencyCountChangedAction += OnCurrencyCountChanged;
            }

            _currencyCountText.text = "$" + _shopWindowData.ShopManager.CurrencyCount.ToString();

            foreach (Transform child in _itemsCardsParent)
            {
                Destroy(child.gameObject);
            }
            foreach (var itemSO in _shopWindowData.ShopManager.Items)
            {
                if (itemSO.IsInfinite && _shopWindowData.InventoryManager.GetInventoryItem(itemSO) != null)
                {
                    continue;
                }
                
                var itemCard = Instantiate(_itemCardPrefab, _itemsCardsParent).GetComponent<ShopItemCard>();
                itemCard.Initialize(itemSO, _shopWindowData.ShopManager);
            }
            
            OnShowComplete();
        }

        private void OnEnable()
        {
            _mainWindowButton.onClick.AddListener(OnMainWindowButtonClick);
        }
        
        private void OnDisable()
        {
            _mainWindowButton.onClick.RemoveListener(OnMainWindowButtonClick);
        }

        private void OnMainWindowButtonClick()
        {
            _shopWindowData.UIManager.ShowElement<MainWindow>();
        }

        private void OnCurrencyCountChanged(float currencyCount)
        {
            _currencyCountText.text = "$" + currencyCount.ToString();
        }
    }
}
