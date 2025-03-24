using LazySquirrelLabs.MinMaxRangeAttribute;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum MovementType
    {
        LeftToRight_XY,
        TopDown_XZ
    }

    [SerializeField] private MovementType movementType;
    [SerializeField] private float movementSpeed;

    [MinMaxRange(0f,1f)]
    [SerializeField] private Vector2 activeRegionWidth;
    [MinMaxRange(0f,1f)]
    [SerializeField] private Vector2 activeRegionHeight;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        switch (movementType)
        {
            case MovementType.LeftToRight_XY:
                MoveLeftToRight();
                break;
            case MovementType.TopDown_XZ:
                MoveTopDown();
                break;
        }

        ClampMovementToScreen();
    }

    private void ClampMovementToScreen()
    {
        Camera camera = Camera.main;
        Vector3 viewPos = camera.WorldToViewportPoint(transform.position);
        viewPos.x = Mathf.Clamp(viewPos.x, 0.1f, 0.3f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.1f, 0.9f);
        transform.position = camera.ViewportToWorldPoint(viewPos);
    }

    private void MoveLeftToRight()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        rb.linearVelocity = movement * movementSpeed;
    }

    private void MoveTopDown()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        rb.linearVelocity = movement * movementSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 1, 0.7f);
        Camera camera = Camera.main;
        Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(activeRegionWidth.x, activeRegionHeight.x, Vector3.Distance(transform.position, camera.transform.position)));
        Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(activeRegionWidth.y, activeRegionHeight.y, Vector3.Distance(transform.position, camera.transform.position)));
        Gizmos.DrawCube(bottomLeft, Vector3.one * 0.3f);
        Gizmos.DrawCube(topRight, Vector3.one * 0.3f);

    }
}
