using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
    public FauxBodyAttractor attractor;
    private Transform mytransform;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 0;
        mytransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        attractor.Attract(mytransform);
    }
}
