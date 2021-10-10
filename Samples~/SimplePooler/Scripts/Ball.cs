using System;
using ExpressoBits.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour, IPooler
{

    private Rigidbody m_Rigidbody;
    private Renderer m_Renderer;

    public void OnPoolerDisable()
    {
        
    }

    public void OnPoolerEnable()
    {
        m_Renderer.material.color = Random.ColorHSV();
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
        m_Rigidbody.angularDrag = 0f;
    }

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Renderer = GetComponent<Renderer>();
    }

}