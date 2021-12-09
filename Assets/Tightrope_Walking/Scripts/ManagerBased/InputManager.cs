using amemo.balanceUnicycle.structurals.Singleton;
using amemo.balanceUnicycle.utils;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace amemo.balanceUnicycle.structurals.inputs
{
    /// <summary>
    /// InputManager was designed to handle Unity New Input System.  
    ///     
    ///  created by: Ahmet Þentürk
    /// </summary>
    /// 
    [DefaultExecutionOrder(-2)]
    public class InputManager : Singleton<InputManager>
    {
        private PlayerControls playerControls;
        private Camera mainCamera;

        public Action<Vector2, float> onStart;
        public Action<Vector2, float> onEnd;

        private void Awake()
        {
            playerControls = new PlayerControls();
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Start()
        {
            playerControls.Touch.PrimaryContact.started += PrimaryPositionStarted;
            playerControls.Touch.PrimaryContact.canceled += PrimaryContactEnded;

        }

        private void PrimaryPositionStarted(InputAction.CallbackContext obj)
        {
            if (onStart != null) onStart(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)obj.startTime);
        }


        private void PrimaryContactEnded(InputAction.CallbackContext obj)
        {
            if (onEnd != null) onEnd(Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)obj.time);
        }

        public Vector2 PrimaryPosition()
        {
            return Utils.ScreenToWorld(mainCamera, playerControls.Touch.PrimaryPosition.ReadValue<Vector2>());
        }

    }


}

namespace amemo.balanceUnicycle.utils
{
    public class Utils : MonoBehaviour
    {
        public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }
    }
}

