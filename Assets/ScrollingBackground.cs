using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private enum Direction
    {
        LeftToRight,
        DownToUp
    }

    [SerializeField] private Direction direction;

    public List<Section> sections;
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float resetPosition = 0f;

    private void Start()
    {
        PreplaceSections();
    }

    [ContextMenu("Preplace Sections")]
    public void PreplaceSections()
    {
        switch (direction)
        {
            case Direction.LeftToRight:
                sections[0].transform.position = new Vector3(resetPosition, transform.position.y, transform.position.z);
                for (int i = 1; i < sections.Count; i++)
                {
                    sections[i].transform.position = sections[i - 1].transform.position + Vector3.right * sections[i - 1].GetSize().x;
                }
                break;
            case Direction.DownToUp:
                sections[0].transform.position = new Vector3(transform.position.x, transform.position.y, resetPosition);
                for (int i = 1; i < sections.Count; i++)
                {
                    sections[i].transform.position = sections[i - 1].transform.position + Vector3.forward * sections[i - 1].GetSize().z;
                }
                break;
        }
        
    }

    private void Update()
    {
        ScrollBackground();
        UpdatePosition();
    }

    void ScrollBackground()
    {
        switch (direction)
        {
            case Direction.LeftToRight:
                foreach (Section section in sections)
                {
                    section.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
                }
                break;
            case Direction.DownToUp:
                foreach (Section section in sections)
                {
                    section.transform.position += Vector3.back * scrollSpeed * Time.deltaTime; 
                }
                break;
        }
        
    }

    void UpdatePosition()
    {
        Vector3 sectionPosition = sections[0].transform.position;
        switch (direction)
        {
            case Direction.LeftToRight:
                if (sectionPosition.x < resetPosition)
                {
                    sectionPosition.x = sections[sections.Count - 1].transform.position.x + sections[sections.Count - 1].GetSize().x;
                    sections[0].transform.position = sectionPosition;
                    sections.Add(sections[0]);
                    sections.RemoveAt(0);
                }
                break;
            case Direction.DownToUp:
                if (sectionPosition.z < resetPosition)
                {
                    sectionPosition.z = sections[sections.Count - 1].transform.position.z + sections[sections.Count - 1].GetSize().z;
                    sections[0].transform.position = sectionPosition;
                    sections.Add(sections[0]);
                    sections.RemoveAt(0);
                }
                break;
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        switch (direction)
        {
            case Direction.LeftToRight:
                Gizmos.DrawCube(new Vector3(resetPosition, 0, 0), new Vector3(0.1f, 10, 0.1f));
                break;
            case Direction.DownToUp:
                Gizmos.DrawCube(new Vector3(0, 0, resetPosition), new Vector3(10, 0.1f, 0.1f));
                break;
        }

    }
}
