using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] private Image _filler;

        protected void OnValueChanged(float valueInParts)
            => _filler.fillAmount = valueInParts;
    }
}