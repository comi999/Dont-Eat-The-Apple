using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Player Attributes")]
    public  string playerName = "";
    public  int playerMoney = 4;
    public  bool ownsWagon = false;

    [Header("Camera Controls")]
    public  float mouseSensitivity = 10;
    private float yaw;
    private float pitch;

    [Header ("Movement Controls")]
    public  float moveSpeed = 0.1f;

    private CharacterController characterController;
    private Camera playerCamera;
    
    public  Inventory bag = new Inventory("Bag", 100);

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;

        //Initialise Bag
        InitialiseBag();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !HUDController.Instance.IsUIShowing())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            HUDController.Instance.ShowCurrentUI();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            HUDController.Instance.CloseUI();
        }

        //Move Player body
        if (!HUDController.Instance.IsUIShowing())
        {
            float hMov = Input.GetAxis("Horizontal");
            float vMov = Input.GetAxis("Vertical");
            Vector3 move = transform.forward * vMov + transform.right * hMov;
            move.Normalize();
            characterController.SimpleMove(move * moveSpeed);

            float hRot = Input.GetAxis("Mouse X") * mouseSensitivity;
            float vRot = -Input.GetAxis("Mouse Y") * mouseSensitivity;

            yaw  += hRot;
            pitch = Mathf.Clamp(pitch += vRot, -90, 90);

            playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
            transform.rotation =                   Quaternion.Euler(0, yaw, 0);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }

    void InitialiseBag()
    {
        bag.Insert(new ItemStack(0, 5));
        bag.Insert(new ItemStack(1, 9));
        bag.Insert(new ItemStack(2, 3));
        bag.Insert(new ItemStack(3, 2));
        bag.Insert(new ItemStack(4, 2));
        bag.Insert(new ItemStack(5, 1));
    }
}
