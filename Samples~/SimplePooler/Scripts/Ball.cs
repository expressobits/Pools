using System;
using UnityEngine;
using ExpressoBits.Pools;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour {
    
    private Rigidbody m_Rigidbody;
    private Renderer m_Renderer;
    
    private void Awake () {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        m_Renderer.material.color = Random.ColorHSV();
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
        m_Rigidbody.angularDrag = 0f;
    }

}