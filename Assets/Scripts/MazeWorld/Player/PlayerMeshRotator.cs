using System;
using UnityEngine;

namespace MazeWorld.Player
{
    public class PlayerMeshRotator : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            // var screenCenter = new Vector3(Screen.width * 0.5f, 0f, 10f);
            // transform.LookAt(_camera.ScreenToWorldPoint(screenCenter));

            var camTransform = _camera.transform;
            transform.rotation = Quaternion.LookRotation(camTransform.forward, camTransform.up);
        }
    }
}
