using Inventory;
using Items.Actions;
using UnityEngine;

namespace Items.ItemActions
{
    [CreateAssetMenu(fileName = "HealActionSO", menuName = "Scriptable Objects/ItemActions/HealActionSO")]
    public class HealActionSO : ItemActionSO
    {
        [SerializeField] private float _healAmount;
        
        public override bool CanExecute(
            UseContext useContext,
            InventoryItem invetoryItem,
            out string reason)
        {
            if (invetoryItem.ItemSO != null && invetoryItem.OnCooldown(Time.time))
            {
                reason = "Cooldown";
                return false;
            }

            reason = null;
            return true;
        }

        public override void Execute(UseContext useContext, InventoryItem invetoryItem)
        {
            Debug.LogFormat("{0} healed!", _healAmount);
        }
    }
}
