using UnityEngine;

public class Section : MonoBehaviour
{
    [SerializeField] private Vector3 sectionSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.right * sectionSize.x * 0.5f + Vector3.up * sectionSize.y * 0.5f, sectionSize);
    }

    public Vector3 GetSize()
    {
        return sectionSize;
    }
}
