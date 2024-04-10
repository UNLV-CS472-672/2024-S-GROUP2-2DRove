using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DarkPixelRPGUI.Scripts.UI
{
    [ExecuteInEditMode]
    public class SliderTextValue : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            if (slider)
            {
                slider.onValueChanged.AddListener(UpdateText);
            }
        }

        private void UpdateText(float value)
        {
            if (!text) return;
            text.text = $"{value:0}/{slider.maxValue:0}";
        }

        private void OnDestroy()
        {
            if (slider)
            {
                slider.onValueChanged.RemoveListener(UpdateText);
            }
        }
    }
}