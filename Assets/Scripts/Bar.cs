using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Image filler;

        public void OnValueChanged(float valueInParts)
            => filler.fillAmount = valueInParts;
    }
}