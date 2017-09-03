using System;
using Assets.TurnBasedStrategy.Scripts.Utils;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.Common
{
    /// <summary>
    /// Holds information about what direction, if any, an objects has registered as moving in based
    /// on the velocity of it's x and z axis.
    /// </summary>
    [Serializable]
    public struct DirectionInformation
    {
        /// <summary>
        /// The left/right directionality, or neither, that this struct represents.
        /// </summary>
        public MoveDirection Horizontal => _horizontal;

        /// <summary>
        /// The forward/backward directionality, or neither, thatthis struct represents.
        /// </summary>
        public MoveDirection Vertical => _vertical;

        /// <summary>
        /// Determines if this struct is stationary in all axis.
        /// </summary>
        public bool IsStationary =>
            Vertical == MoveDirection.Stationary && Horizontal == MoveDirection.Stationary;

        [ReadOnly][SerializeField] private MoveDirection _horizontal;
        [ReadOnly][SerializeField] private MoveDirection _vertical;

        /// <summary>
        /// Creates a bew DirectionInformation object.
        /// </summary>
        /// <param name="horizontal">The left/right or horizontal velocity to be converted to a direction.</param>
        /// <param name="verical">The forward/backward or vertical velocity to be converted to a direction.</param>
        public DirectionInformation(float horizontal, float verical)
        {
            _horizontal = horizontal > 0 ? MoveDirection.Right: 
                horizontal < 0 ? 
                    MoveDirection.Left: 
                    MoveDirection.Stationary;

            _vertical = verical > 0 ? MoveDirection.Forward: 
                 verical < 0 ? 
                        MoveDirection.Backward: 
                        MoveDirection.Stationary;
        }
    }

    public enum MoveDirection { Stationary, Forward, Backward, Left, Right }
}
