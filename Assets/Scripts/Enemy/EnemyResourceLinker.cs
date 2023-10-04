using UI;
using UnityEngine;

namespace Enemy
{
    public class EnemyResourceLinker : MonoBehaviour
    {
        [field:SerializeField] public ResourseSlider ResourseSlider { get; private set; }
        [field:SerializeField] public EnemyPositionTracker EnemyPositionTracker { get; private set; }
    }
}