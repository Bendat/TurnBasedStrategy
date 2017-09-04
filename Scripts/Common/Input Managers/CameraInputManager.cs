using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.TurnBasedStrategy.Scripts.Utils;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.Common.Input_Managers
{
    public class CameraInputManager: AbstraceSingleton<CameraInputManager>
    {
        private float Horizontal => Input.GetAxis("Horizontal");
        private float Vertical => Input.GetAxis("Vertical");
        private float Scroll => Input.GetAxis("Mouse ScrollWheel");
        private float Rotation => Input.GetAxis("Rotation");
    }
}
