using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public float dampTime = 0.15f;
    private Vector3 v = Vector3.zero;

    //target should be set to player. Transform on player axis
    private Transform target;
    Camera camera;

    void Start()
    {
        //find the objects needed
        camera = GetComponent<Camera>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }
	void Update () {
        //keeps camera following player, with general smooth value to decrease jitter
        
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            var pos = Vector3.SmoothDamp(transform.position, destination, ref v, dampTime);
            pos.x = Mathf.Clamp(pos.x, -24, 24);
            pos.y = Mathf.Clamp(pos.y, -24, 10);
            transform.position = pos;
        }
    }
}
