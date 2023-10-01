using DefaultNamespace;
using UI;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] private EnemyResourceLinker enemyResourceLinker;

    public void SpawnSlider(EnemyContainer enemyContainer)
    {
        var instance = Instantiate(enemyResourceLinker, transform);
        instance.EnemyPositionTracker.Init(enemyContainer.HeadUp);
        enemyContainer.HealView.OnHealthChangeEvent +=  instance.ResourseSlider.SetValue;
    }
}