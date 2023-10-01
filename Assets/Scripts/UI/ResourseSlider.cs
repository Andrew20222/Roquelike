using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourseSlider : MonoBehaviour
    {
        [SerializeField] private Slider resourseSlider;

        public void SetValue(float currentValue, float maxValue)
        {
            if(resourseSlider == null) return;

            resourseSlider.maxValue = maxValue;
            resourseSlider.value = currentValue;
        }
    
    }
}

