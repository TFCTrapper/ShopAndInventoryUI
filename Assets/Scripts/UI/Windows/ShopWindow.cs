using System;
using Shop;
using UI.Windows.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ShopWindow : Window
    {
        [SerializeField] private Button _mainWindowButton;
        [SerializeField] private GameObject _itemCardPrefab;
        [SerializeField] private Transform _itemsCardsParent;
        
        private ShopWindowData _shopWindowData;
        
        public override void Show(UIElementData data = null, bool immediately = false)
        {
            base.Show(data, immediately);

            if (data != null)
            {
                _shopWindowData = data as ShopWindowData;
            }

            foreach (Transform child in _itemsCardsParent)
            {
                Destroy(child.gameObject);
            }
            foreach (var itemSO in _shopWindowData.ShopManager.Items)
            {
                var itemCard = Instantiate(_itemCardPrefab, _itemsCardsParent).GetComponent<ItemCard>();
                itemCard.Initialize(itemSO);
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
    }
}
