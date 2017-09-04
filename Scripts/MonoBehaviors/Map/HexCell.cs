using Assets.TurnBasedStrategy.Scripts.Common;
using Assets.TurnBasedStrategy.Scripts.Enums;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a Hexagonal Cell on the map, and metrics associated with them.
    /// </summary>
    public class HexCell : MonoBehaviour {

        public const float SolidFactor = 0.75f;

        public const float BlendFactor = 1f - SolidFactor;

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
        public static readonly Vector3[] Corners = {
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

        [SerializeField] private HexCell[] _neighbors;

        public static Vector3 GetFirstCorner(HexDirection direction)
        {
            return Corners[(int)direction];
        }

        public static Vector3 GetSecondCorner(HexDirection direction)
        {
            return Corners[(int)direction + 1];
        }

        public HexCell GetNeighbor(HexDirection direction)
        {
            return _neighbors[(int)direction];
        }

        public void SetNeighbor(HexDirection direction, HexCell cell)
        {
            _neighbors[(int) direction] = cell;
            cell._neighbors[(int)direction] = this;
            cell._neighbors[(int)direction.Opposite()] = this;
        }

        public static Vector3 GetFirstSolidCorner(HexDirection direction)
        {
            return Corners[(int)direction] * SolidFactor;
        }

        public static Vector3 GetSecondSolidCorner(HexDirection direction)
        {
            return Corners[(int)direction + 1] * SolidFactor;
        }

        public static Vector3 GetBridge(HexDirection direction)
        {
            return (Corners[(int)direction] + Corners[(int)direction + 1]) *
                   0.5f * BlendFactor;
        }
    }
}
