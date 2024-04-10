using System;
using UnityEngine;

namespace DarkPixelRPGUI.Scripts.UI.Equipment
{
    [Serializable]
    public class Item
    {
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}