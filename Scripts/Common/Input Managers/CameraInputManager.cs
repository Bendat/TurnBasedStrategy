using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.TurnBasedStrategy.Scripts.Utils;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.Common.Input_Managers
{
    /// <summary>
    /// Singleton class which returns the values of its assigned axis for the frame in which they're called.
    /// </summary>
    public class CameraInputManager: AbstraceSingleton<CameraInputManager>
    {
        /// <summary>
        /// Yhe value on the "Horizontal Axis"
        /// </summary>
        public float Horizontal => Input.GetAxis("Horizontal");
        /// <summary>
        /// The value on the "Vertical Axis"
        /// </summary>
        public float Vertical => Input.GetAxis("Vertical");
        /// <summary>
        /// The Value on the "Mouse ScrollWheel" axis.
        /// </summary>
        public float Scroll => Input.GetAxis("Mouse ScrollWheel");
        /// <summary>
        /// The Value on the "Rotation" axis.
        /// </summary>
        public float Rotation => Input.GetAxis("Rotation");
    }
}
