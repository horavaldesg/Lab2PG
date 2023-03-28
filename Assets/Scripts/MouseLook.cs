using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public FloatVal sens;
    public Transform camTransform;
    private float _camRotation;

    private void Awake()
    {
        //Sets sens to the sens val
        sens = Resources.Load<FloatVal>("ScriptableObjects/sensVal");
        if (!PlayerPrefs.HasKey("FirstTimePlaying"))
        {
            sens.val = 15;
            PlayerPrefs.SetInt("FirstTimePlaying", 1);
        }
    }

    private void Start()
    {
        //Locks Cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //Rotation
        var mouseX = Input.GetAxis("Mouse X") * sens.val* Time.deltaTime * 10;
        transform.Rotate(new Vector3(transform.rotation.x, mouseX, transform.rotation.z));
        var mouseY = -Input.GetAxis("Mouse Y") * sens.val * Time.deltaTime * 10;
        _camRotation += mouseY;
        _camRotation = Mathf.Clamp(_camRotation, -80f, 90f);
        camTransform.localRotation =
            Quaternion.Euler(new Vector3(_camRotation, transform.rotation.y, transform.rotation.z));
    }
}
