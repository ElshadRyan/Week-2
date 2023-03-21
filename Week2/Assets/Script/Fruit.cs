using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject Whole;
    public GameObject Sliced;

    private Rigidbody FruitRigidbody;
    private Collider FruitCollider;
    private ParticleSystem Juice;

    private void Awake()
    {
        FruitRigidbody = GetComponent<Rigidbody>();
        FruitCollider = GetComponent<Collider>();
        Juice = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 Direction, Vector3 Position, float Force)
    {
        FindAnyObjectByType<GameManager>().IncreaseScore();

        Whole.SetActive(false);
        Sliced.SetActive(true);

        FruitCollider.enabled = false;
        Juice.Play();

        float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Sliced.transform.rotation = Quaternion.Euler(0f, 0f, Angle);

        Rigidbody[] Slices = Sliced.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody Slice in Slices)
        {
            Slice.velocity = FruitRigidbody.velocity;
            Slice.AddForceAtPosition(Direction * Force, Position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Blade Blade = other.GetComponent<Blade>();
            Slice(Blade.direction, Blade.transform.position, Blade.SlicedForce);
        }
    }
}
