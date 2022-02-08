using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerController : MonoBehaviour
{
    public float sensitivity = 1f;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        Vector3 newPos = transform.position + new Vector3(deltaX * sensitivity, 0, deltaY * sensitivity);
        Vector3 clamped = new Vector3(Mathf.Clamp(newPos.x, minX, maxX), newPos.y, Mathf.Clamp(newPos.z, minZ, maxZ));

        transform.position = clamped;
    }
}
