using UnityEngine;
using UnityEngine.UI;

namespace DarkPixelRPGUI.Scripts.UI.Equipment
{
    public class DragItemHolder : MonoBehaviour
    {
        public static DragItemHolder Instance;

        [SerializeField] private Image displayItemImage;
        [SerializeField] private Transform transformToMove;
        [HideInInspector]
        public bool dragging;
        [HideInInspector]
        public Item dragItem;

        private Slot _targetSlotToDrop;
        private Slot _sourceSlot;

        public void StartDrag(Slot sourceSlot)
        {
            if (dragging) return;
            dragging = true;

            _sourceSlot = sourceSlot;
            dragItem = _sourceSlot.Item;
            _sourceSlot.ClearSlot();
            TargetSlotToDrop(_sourceSlot);
            
            transformToMove.position = Input.mousePosition;
            displayItemImage.sprite = dragItem.Sprite;
            displayItemImage.enabled = dragging;
        }

        public void DropItem()
        {
            if (!dragging) return;
            dragging = false;
            _targetSlotToDrop.PlaceItem(dragItem);
            dragItem = null;
            displayItemImage.enabled = dragging;
            _targetSlotToDrop = null;
            _sourceSlot = null;
        }

        public void TargetSlotToDrop(Slot slot)
        {
            if (_targetSlotToDrop != null)
            {
                _targetSlotToDrop.ClearSlot();
                
            }
            _targetSlotToDrop = slot;
            _targetSlotToDrop.PlaceholdItem(dragItem);
        }

        public void RemoveTarget(Slot slot)
        {
            if (slot != _targetSlotToDrop) return;
            TargetSlotToDrop(_sourceSlot);
        }

        private void Update()
        {
            if (!dragging) return;
            transformToMove.position = Input.mousePosition;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
    }
}