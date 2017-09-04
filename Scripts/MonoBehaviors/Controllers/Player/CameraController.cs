using System;
using System.Collections;
using System.Collections.Generic;
using Assets.TurnBasedStrategy.Scripts.Common;
using Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map;
using Assets.TurnBasedStrategy.Scripts.Utils;
using UnityEngine;
//using static UnityEngine.Debug;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Controllers.Player
{
    /// <inheritdoc />
    /// <summary>
    /// Controller for the Strategy view camera.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <summary>
        /// The Current level of zoom the camera is at.
        /// </summary>
        public float Zoom => _zoom;
        /// <summary>
        /// The speed the camera will pan at when Zoom is 0
        /// </summary>
        public float MoveSpeedMinZoom;
        /// <summary>
        /// The speed the camera will pan at when Zoom is 1
        /// </summary>
        public float MoveSpeedMaxZoom;
        public float RotationSpeed;
        [Range(0f,360f)]public float MaxRotationAngle;
        public float MoveSpeed;
        /// <summary>
        /// The minimum angle of the cameras swivel when Zoom is 0.
        /// </summary>
        public float SwivelMinZoom;
        /// <summary>
        /// The Maximum angle of the cameras swivel when Zoom is 1.
        /// </summary>
        public float SwivelMaxZoom;
        /// <summary>
        /// The distance the camera sticks out from this object at when Zoom is 0.
        /// </summary>
        public float StickMinZoom;
        /// <summary>
        /// The distance the camera sticks out from this object at when Zoom is 1.
        /// </summary>
        public float StickMaxZoom;

        private float XDelta => Input.GetAxis("Horizontal");
        private float ZDelta => Input.GetAxis("Vertical");
        private float ZoomDelta => Input.GetAxis("Mouse ScrollWheel");
        private float RotationDelta => Input.GetAxis("Rotation");

        [ReadOnly] [SerializeField] private Quaternion _originalRotation;
        [ReadOnly] [SerializeField] private Transform _swivel;
        [ReadOnly] [SerializeField] private Transform _stick;

        [ReadOnly][SerializeField]private float _lastDelta;
        [ReadOnly][SerializeField]private float _rotationAngle;
        [ReadOnly][SerializeField]private float _zoom;

        private void Awake()
        {
            _originalRotation = transform.localRotation;
            _swivel = transform.GetChild(0);
            _stick = _swivel.GetChild(0);
        }

        private void Start()
        {
            AdjustZoom(0);
        }

        private void Update()
        {
            if (ZoomDelta != 0f)
            {
                AdjustZoom(ZoomDelta);
            }
            if (RotationDelta != 0f && CanRotate())
            {
                _lastDelta = RotationDelta;
                AdjustRotation(_lastDelta);
            }else if (Math.Abs(_rotationAngle - _originalRotation.z) > 0.5f)
            {
                Snap();
            }
            if (XDelta != 0f || ZDelta != 0f)
            {
                AdjustPosition(XDelta, ZDelta);
            }
        }

        private bool CanRotate()
        {
            return (_rotationAngle < MaxRotationAngle) 
                || (_rotationAngle > (360 - MaxRotationAngle));
        }

        private void AdjustZoom(float delta)
        {        
            _zoom = Mathf.Clamp01(Zoom + delta);

            float distance = Mathf.Lerp(StickMinZoom, StickMaxZoom, Zoom);
            _stick.localPosition = new Vector3(0f, 0f, distance);

            float angle = Mathf.Lerp(SwivelMinZoom, SwivelMaxZoom, Zoom);
            _swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
        }

        private void Snap()
        {      
            AdjustRotation(_lastDelta>0?-1:1);
        }

        private void AdjustRotation(float delta)
        {
            _rotationAngle += delta * RotationSpeed * Time.deltaTime;
            if (_rotationAngle < 0f)
            {
                _rotationAngle += 360f;
            }
            else if (_rotationAngle >= 360f)
            {
                _rotationAngle -= 360f;
            }
            transform.localRotation = Quaternion.Euler(0f, _rotationAngle, 0f);
        }

        private void AdjustPosition(float xDelta, float zDelta)
        {
            Vector3 direction = transform.localRotation * new Vector3(xDelta, 0f, zDelta).normalized;
            float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
            float distance =
                Mathf.Lerp(MoveSpeedMinZoom, MoveSpeedMaxZoom, Zoom) *
                damping * Time.deltaTime;

            Vector3 position = transform.localPosition;
            position += direction * distance;
            transform.localPosition = position;
        }
    }
}
