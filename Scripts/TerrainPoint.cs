using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class TerrainPoint : MonoBehaviour
{
    void Update()
    {
        // çƒê∂íÜÇÕâΩÇ‡ÇµÇ»Ç¢
        if (Application.isPlaying) return;

        Terrain t = Terrain.activeTerrain;
        if (t == null) return;

        Vector3 p = transform.position;
        float h = t.SampleHeight(p);
        transform.position = new Vector3(p.x, h, p.z);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
