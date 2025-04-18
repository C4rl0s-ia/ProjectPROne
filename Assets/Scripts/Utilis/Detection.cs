using UnityEngine;
using System.Collections;
using System.Collections.Generic;   
public class Detection : MonoBehaviour
{
    [Header("Detections")]
    [SerializeField] private LayerMask layerGround;
    [Range(0, 10)]
    [SerializeField] private float GroundRadius = 0.2f;
    [SerializeField] private Transform groundPoint;

    public Collider2D ground;
    void Start()
    {
        // Initialize or set up any necessary components here
    }

    void Update()
    {
        HandleGround();
    }

    private void HandleGround()
    {
        ground = Physics2D.OverlapCircle(groundPoint.position, GroundRadius, layerGround);
    }

    private void OnDrawGizmos()
    {
        if (ground != null) 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundPoint.position, GroundRadius);
        }
        
    }
}
