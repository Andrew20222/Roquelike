using UnityEngine;

namespace Unit.Behaviors
{
    public class CircleDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private int segments = 360;

        private void Start()
        {
            lineRenderer.useWorldSpace = false;
            DrawCircle();
        }

        private void DrawCircle()
        {
            lineRenderer.positionCount = segments + 1;

            var deltaAngle = 2.0f * Mathf.PI / segments;
            float angle = 0;

            for (int i = 0; i < segments + 1; i++)
            {
                float x = Mathf.Cos(angle) * sphereCollider.radius;
                float y = Mathf.Sin(angle) * sphereCollider.radius;

                lineRenderer.SetPosition(i, new Vector3(x, y, 0));

                angle += deltaAngle;
            }
        }
    }
}