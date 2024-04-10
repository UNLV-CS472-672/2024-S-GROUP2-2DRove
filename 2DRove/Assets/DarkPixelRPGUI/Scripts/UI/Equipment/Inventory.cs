using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DarkPixelRPGUI.Scripts.UI.Equipment
{
    public class Inventory : MonoBehaviour
    {
        private List<Slot> _slots;

        private void Start()
        {
            _slots = GetComponentsInChildren<Slot>().ToList();
        }

        public bool HasFreeSpace()
        {
            return _slots.Any(slot => slot.IsEmpty());
        }

        public Slot GetNextEmptySlotForItem()
        {
            return _slots.Find(s => s.IsEmpty());
        }

        public void RemoveBlanks()
        {
            var items = _slots.Where(s => !s.IsEmpty()).Select(s => s.Item).ToList();
            for (var i = 0; i < _slots.Count; i++)
            {
                if (i < items.Count)
                {
                    _slots[i].PlaceItem(items[i]);
                }
                else
                {
                    _slots[i].ClearSlot();
                }
            }
        }
    }
}