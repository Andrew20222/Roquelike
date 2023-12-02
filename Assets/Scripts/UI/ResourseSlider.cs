using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ResourseSlider : MonoBehaviour
    {
        [SerializeField] private Image imageSlider;

        public void SetValue(float currentValue, float maxValue)
        {
            if (imageSlider == null) return;
            var result = Mathf.Lerp(0, 1, currentValue);
            var fillAmount = Mathf.Clamp(currentValue / maxValue, 0, 1);
            imageSlider.fillAmount = fillAmount;
        }
    }
}