using System;
using System.Collections;
using Assets.TurnBasedStrategy.Scripts.Common;
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
        /// The time taken for the cameras panning speed to go from minimum to maximum.
        /// </summary>
        public float PanAccelerationTime = 1f;

        /// <summary>
        /// The highest speed the camera can pan at.
        /// </summary>
        public float MaxPanSpeed = 20f;

        /// <summary>
        /// The resting speed of the camera.
        /// </summary>
        public float MinPanSpeed = 0f;

        /// <summary>
        /// The current speed at which the camera is panning.
        /// </summary>
        public float PanSpeed => _panSpeed;

        /// <summary>
        /// The speed at which the camera zooms in and out of the map.
        /// </summary>
        public float ZoomSpeed = 20f;
        /// <summary>
        /// The highest distance above the ground the camera can zoom to.
        /// </summary>
        public float MaxZoomHeight = 70f;

        /// <summary>
        /// The lowest distance above the ground the camera can zoom to.
        /// </summary>
        public float MinZoomHeight = 10f;

        /// <summary>
        /// The multiplier to apply to the cameras speed when shift is held.
        /// </summary>
        public float PanSprintMultiplier = 3;

        /// <summary>
        /// The DirectionInformation of the camera during the current frame.
        /// </summary>
        public DirectionInformation DirectionInfo;

        private float _panSpeedBase = 0; // the point at which to start lerping if the ActualMaxSpeed is changed. 
        private bool _crIsRunning; // Indicates if the AccelerationRoutine is running.
        [ReadOnly][SerializeField] private float _panSpeed;
        [ReadOnly][SerializeField] private Vector3 _velocity;
        private Vector3 _previousPosition;

        // Used to calculate whether the panning speed of the camera is modified due to shift being held.

        private float ActualMaxPanSpeed => Input.GetKey(KeyCode.LeftShift) ? MaxPanSpeed * PanSprintMultiplier: MaxPanSpeed;

        private float ActualMinPanSpeed => Input.GetKey(KeyCode.LeftShift) ? _panSpeedBase : MinPanSpeed;

        // Determines if the max speed sould be increased due to shift being held.
        private bool CanDoubleAccelerate => Mathf.Abs(PanSpeed - MaxPanSpeed) < 0.5 && Input.GetKey(KeyCode.LeftShift);


        private void Start()
        {
            _previousPosition = transform.position;
            _panSpeed = MinPanSpeed;
        }

        private void Update()
        {
            _velocity = (_previousPosition - transform.position) / Time.deltaTime;
            _previousPosition = transform.position;

            var wasStationaryLastFrame = DirectionInfo.IsStationary;

            var pos = transform.position;
            DirectionInfo = GetAxis();

            /*
             * ref parameters allow structs to be modified as is without the need to assign a returned copy.
             * In other words treats structs as if they were an object which was initialized from a Class.
             */

            HandleAcceleration(ref DirectionInfo, wasStationaryLastFrame);

            HandleMovement(ref pos);
           
            Zoom(ref pos);

            transform.position = pos;
        }

        // handles zooming in and out of the map.
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
            // A variable containing an invokable method which takes a parameter of type float (the current phase in the lerp timer)
            // Doing this allows the same routine to lerp in multiple directions (or perform some other action every loop of the routine)
            Action<float> accelerate = (timer) => {
                _panSpeed = Mathf.Lerp(ActualMinPanSpeed, ActualMaxPanSpeed, timer / PanAccelerationTime);
            };
            if (!directionInfo.IsStationary)
            {
                // We only want to run the coroutine if the player requests movement and either we weren't moving last frame,
                // or the shift key has been pressed (modifying the max speed)
                if (!_crIsRunning && (CanDoubleAccelerate || wasStationaryLastFrame))
                {
                    StartCoroutine(AccelerationRoutine(accelerate));
                }
                else if (PanSpeed > ActualMaxPanSpeed)
                {
                    // Smoothly ramp up or down MaxSpeed if shift is held.
                    // Might be able to remove this else-if, haven't checked.
                    if (_panSpeedBase != 0)
                    {
                        _panSpeedBase = PanSpeed;
                    }
                    _panSpeedBase = _panSpeedBase != 0? _panSpeed: -1;
                    StartCoroutine(AccelerationRoutine((timer) => {
                        _panSpeed = Mathf.Lerp(ActualMinPanSpeed, ActualMaxPanSpeed, timer / PanAccelerationTime);
                        _panSpeedBase = -1;
                    }));
                }
            }
            else if ((!wasStationaryLastFrame && directionInfo.IsStationary))
            {
                StopCoroutine(AccelerationRoutine(accelerate));
            }
        }

        /// <summary>
        /// A coroutine which takes a number of seconds to execute equal to the PanAccelerationTime property. It will repeatedly perform
        /// an Action until the timer runs out. Exits prematurely if the camera is stationary or the timer exceeds its limit.
        /// </summary>
        /// <param name="action">An Action or Method delegate which takes a float, "timer" which represents the amount of time in seconds that this routine has been active for,
        /// as a parameter. Called repeatedly until timer is equal to PanAccelerationTime.
        /// </param>
        /// <returns>Enumerator of nulls</returns>
        protected IEnumerator AccelerationRoutine(Action<float> action)
        {
            _crIsRunning = true;
            var timer = 0f;
            while (timer < PanAccelerationTime
                   && !DirectionInfo.IsStationary)
            {
                timer += Time.deltaTime;
                action(timer);
                yield return null;
            }
            _crIsRunning = false;
        }

        private void HandleMovement(ref Vector3 pos)
        {
            HandleVerticalMovement(ref pos);
            HandleHorizontalMovement(ref pos);
            if (DirectionInfo.IsStationary)
            {
                _panSpeed = 0f;
            }
        }

        private void HandleVerticalMovement(ref Vector3 pos)
        {
            switch (DirectionInfo.Vertical)
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

    }
}
