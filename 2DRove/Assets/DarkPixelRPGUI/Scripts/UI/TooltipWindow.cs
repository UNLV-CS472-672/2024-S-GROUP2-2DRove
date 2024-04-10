using TMPro;
using UnityEngine;

namespace DarkPixelRPGUI.Scripts.UI
{
    public class TooltipWindow : MonoBehaviour
    {
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private RectTransform windowTransform;

        private void Start()
        {
            HideTooltip();
        }

        public void ShowTooltip(string header, string description, float positionX)
        {
            headerText.text = header;
            descriptionText.text = description;
            var position = windowTransform.position;
            windowTransform.position = new Vector3(positionX, position.y, position.z);
            gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }
    }
}