using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera MainCamera;
    private Collider BladeCollider;
    private bool Slicing;
    private TrailRenderer Trail;


    public Vector3 direction { get; private set; }
    public float MinSliceVelocity = 0.01f;
    public float SlicedForce = 5f;

    private void Awake()
    {
        MainCamera = Camera.main;
        BladeCollider = GetComponent<Collider>();
        Trail = GetComponentInChildren<TrailRenderer>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (Slicing)
        {
            ContinueSlicing();
        }
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void StartSlicing()
    {
        Vector3 NewPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        NewPosition.z = 0f;
        transform.position = NewPosition;
        Slicing = true;
        BladeCollider.enabled = true;
        Trail.enabled = true;
        Trail.Clear();
    }

    private void StopSlicing()
    {
        Slicing = false;
        BladeCollider.enabled = false;
        Trail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 NewPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        NewPosition.z = 0f;

        direction = NewPosition - transform.position;

        float Velocity = direction.magnitude / Time.deltaTime;

        BladeCollider.enabled = Velocity > MinSliceVelocity;

        transform.position = NewPosition;
    }
}
