using UnityEngine;
using UnityEngine.EventSystems;

namespace DarkPixelRPGUI.Scripts.UI.Equipment
{
    public class SlotItemDropTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Slot slot;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            var dragItemHolder = DragItemHolder.Instance;
            if (dragItemHolder.dragging && slot.IsEmpty())
            {
                dragItemHolder.TargetSlotToDrop(slot);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DragItemHolder.Instance.RemoveTarget(slot);
        }
    }
}