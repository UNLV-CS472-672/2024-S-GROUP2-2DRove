using UnityEngine;
using UnityEngine.UI;

namespace DarkPixelRPGUI.Scripts.UI
{
    public class DropdownHideCaptionBorder : MonoBehaviour
    {
        [SerializeField] private Image image;

        private void OnEnable()
        {
            image.enabled = false;
        }

        private void OnDestroy()
        {
            image.enabled = true;
        }
    }
}