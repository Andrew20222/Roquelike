using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image filler;

    public void OnValueChanged(float valueInParts)
        => filler.fillAmount = valueInParts;
}