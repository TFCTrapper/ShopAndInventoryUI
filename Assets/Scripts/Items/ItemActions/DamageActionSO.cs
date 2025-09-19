using Inventory;
using Items.Actions;
using UnityEngine;

namespace Items.ItemActions
{
    [CreateAssetMenu(fileName = "DamageActionSO", menuName = "Scriptable Objects/ItemActions/DamageActionSO")]
    public class DamageActionSO : ItemActionSO
    {
        [SerializeField] private float _damage;
        
        public override bool CanExecute(
            UseContext useContext,
            InventoryManager.InventoryItem invetoryItem,
            out string reason)
        {
            if (invetoryItem.ItemSO != null && invetoryItem.OnCooldown(useContext.LastUseTime))
            {
                reason = "Cooldown";
                return false;
            }

            reason = null;
            return true;
        }

        public override void Execute(UseContext useContext, InventoryManager.InventoryItem invetoryItem)
        {
            //Damage
        }
    }
}
