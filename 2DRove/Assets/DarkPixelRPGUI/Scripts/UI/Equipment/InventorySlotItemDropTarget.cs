using UnityEngine;
using UnityEngine.EventSystems;

namespace DarkPixelRPGUI.Scripts.UI.Equipment
{
    public class InventorySlotItemDropTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Inventory inventory;
        private Slot _placeholdedSlot;

        public void OnPointerEnter(PointerEventData eventData)
        {
            var dragItemHolder = DragItemHolder.Instance;
            if (!dragItemHolder.dragging || !inventory.HasFreeSpace()) return;
            _placeholdedSlot = inventory.GetNextEmptySlotForItem();
            dragItemHolder.TargetSlotToDrop(_placeholdedSlot);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_placeholdedSlot == null) return;
            DragItemHolder.Instance.RemoveTarget(_placeholdedSlot);
            _placeholdedSlot = null;
        }
    }
}