using Assets.TurnBasedStrategy.Scripts.Common;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a Hexagonal Cell on the map, and metrics associated with them.
    /// </summary>
    public class HexCell : MonoBehaviour {

        /// <summary>
        /// The diameter outer circle touching all 6 points of the hexagon.
        /// </summary>
        public const float OuterRadius = 10f;

        /// <summary>
        /// The diameter of the inner circle touching all 6 sides of the hexagon.
        /// </summary>
        public const float InnerRadius = OuterRadius * 0.866025404f;

        /// <summary>
        /// The corners of a hexagon.
        /// </summary>
        public static Vector3[] Corners = {
            new Vector3(0f, 0f, OuterRadius),

            new Vector3(InnerRadius, 0f, 0.5f * OuterRadius),
            new Vector3(InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(0f, 0f, -OuterRadius),
            new Vector3(-InnerRadius, 0f, -0.5f * OuterRadius),
            new Vector3(-InnerRadius, 0f, 0.5f * OuterRadius),

            new Vector3(0f, 0f, OuterRadius)
        };

        /// <summary>
        /// The Coordinates of this cell on the grid.
        /// </summary>
        public MapCoordinates Coordinates;

        /// <summary>
        /// The color of this cells material.
        /// </summary>
        public Color Color;
    }
}
