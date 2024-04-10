using UnityEngine;
using UnityEngine.UI;

namespace DarkPixelRPGUI.Scripts.UI.Equipment
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private NullableSerializableObjectField<Item> initialItem;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image placedItemImage;
        private Item _item;
        public Item Item => _item;

        public bool IsEmpty()
        {
            return _item == null;
        }

        public virtual void PlaceItem(Item itemToPlace)
        {
            _item = itemToPlace;
            ShowItem();
        }

        public void PlaceholdItem(Item itemToPlacehold)
        {
            itemIcon.sprite = itemToPlacehold.Sprite;
            itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
            itemIcon.enabled = true;
        }

        public virtual void ClearSlot()
        {
            itemIcon.sprite = null;
            itemIcon.color = Color.white;
            itemIcon.enabled = false;
            _item = null;
            if (placedItemImage)
            {
                placedItemImage.enabled = false;
            }
        }

        private void ShowItem()
        {
            itemIcon.sprite = _item.Sprite;
            itemIcon.color = Color.white;
            itemIcon.enabled = true;
            if (placedItemImage)
            {
                placedItemImage.enabled = true;
            }
        }

        private void OnValidate()
        {
            if (itemIcon == null) return;
            if (initialItem.Value == null)
            {
                ClearSlot();
                return;
            }

            if (initialItem.Value != null)
            {
                _item = initialItem.Value;
            }

            ShowItem();
        }
    }
}