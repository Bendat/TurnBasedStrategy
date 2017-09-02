using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.TurnBasedStrategy.Scripts.Common
{
    [Serializable]
    public struct DirectionInformation
    {
        public MoveDirection Horizontal;
        public MoveDirection Verical;

        public bool IsStationary =>
            Verical == MoveDirection.Stationary && Horizontal == MoveDirection.Stationary;

        public DirectionInformation(float horizontal, float verical)
        {
            Horizontal = horizontal > 0 ? MoveDirection.Right: 
                horizontal < 0 ? 
                    MoveDirection.Left: 
                    MoveDirection.Stationary;

            Verical = verical > 0 ? MoveDirection.Forward: 
                 verical < 0 ? 
                        MoveDirection.Backward: 
                        MoveDirection.Stationary;
        }
    }

    public enum MoveDirection { Stationary, Forward, Backward, Left, Right }
}
