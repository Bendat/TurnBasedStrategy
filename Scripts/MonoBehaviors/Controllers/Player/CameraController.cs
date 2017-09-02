using System.Collections;
using Assets.TurnBasedStrategy.Scripts.Common;

using UnityEngine;


namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Controllers.Player
{

    public class CameraController : MonoBehaviour
    {
        public float PanAccelerationTime = 1f;
        public float MaxPanSpeed = 20f;
        public float MinPanSpeed = 0f;
        public float PanSpeed;

        public float ZoomSpeed = 20f;
        public float MaxZoomHeight = 70f;
        public float MinZoomHeight = 10f;



        public DirectionInformation DirectionInfo;

        [SerializeField] private Vector3 _velocity;
        private Vector3 _previousPosition;

        private void Start()
        {
            _previousPosition = transform.position;
            PanSpeed = MinPanSpeed;
        }

        private void Update()
        {
            _velocity = (_previousPosition - transform.position) / Time.deltaTime;
            _previousPosition = transform.position;
            var wasStationaryLastFrame = DirectionInfo.IsStationary;

            var pos = transform.position;
            DirectionInfo = GetAxis();
            HandleAcceleration(ref DirectionInfo, wasStationaryLastFrame);

            HandleMovement(ref pos);
           
            Zoom(ref pos);

            transform.position = pos;
        }

        private void Zoom(ref Vector3 pos)
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0 && pos.y > MinZoomHeight)
            {
                pos += transform.forward * Time.deltaTime * ZoomSpeed;
            }else if (scroll < 0 && pos.y < MaxZoomHeight)
            {
                pos -= transform.forward * Time.deltaTime * ZoomSpeed;
            }
        }

        private void HandleAcceleration(ref DirectionInformation directionInfo, bool wasStationaryLastFrame)
        {
            if (wasStationaryLastFrame && !directionInfo.IsStationary)
            {
                StartCoroutine(PanAcceleration());
            }
            else if (!wasStationaryLastFrame && directionInfo.IsStationary)
            {
                StopCoroutine(PanAcceleration());
            }
        }

        private void HandleMovement(ref Vector3 pos)
        {
            HandleVerticalMovement(ref pos);
            HandleHorizontalMovement(ref pos);
        }

        private void HandleVerticalMovement(ref Vector3 pos)
        {
            switch (DirectionInfo.Verical)
            {
                case MoveDirection.Forward:
                    pos.z += PanSpeed * Time.deltaTime;
                    break;
                case MoveDirection.Backward:
                    pos.z -= PanSpeed * Time.deltaTime;
                    break;
                default:
                    break;
            }
        }

        private void HandleHorizontalMovement(ref Vector3 pos)
        {
            switch (DirectionInfo.Horizontal)
            {
                case MoveDirection.Right:
                    pos.x += PanSpeed * Time.deltaTime;
                    break;
                case MoveDirection.Left:
                    pos.x -= PanSpeed * Time.deltaTime;
                    break;
                default:
                    break;
            }
        }
        private DirectionInformation GetAxis()
        {
            return new DirectionInformation(Input.GetAxis("PanLeftRight"), Input.GetAxis("PanUpDown"));
        }

        /// <summary>
        /// Accelerates the rate at which the camera pans when going from stationary
        /// to moving.
        /// </summary>
        /// <returns></returns>
        IEnumerator PanAcceleration()
        {
            var timer = 0f;
            while (timer < PanAccelerationTime 
                && !DirectionInfo.IsStationary)
            {
                timer += Time.deltaTime;
                PanSpeed = Mathf.Lerp(MinPanSpeed, MaxPanSpeed, timer / PanAccelerationTime);
                yield return null;
            }
        }
    }
}
