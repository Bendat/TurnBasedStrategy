using System;
using System.Security.Policy;
using Assets.TurnBasedStrategy.Scripts.MonoBehaviors.Map;
using Assets.TurnBasedStrategy.Scripts.Utils;
using UnityEngine;

namespace Assets.TurnBasedStrategy.Scripts.Common
{
    /// <summary>
    /// Represents the coordinates on a map grid for a hexagonal cell.
    /// </summary>
    [Serializable]
    public struct MapCoordinates
    {
       
        public int X => x;
        public int Z => z;
        public int Y => -x - z;

        [ReadOnly][SerializeField] private int x, z, y;

        public MapCoordinates(int x, int z)
        {
            this.x = x;
            this.z = z;
            this.y = -x - z;
        }

        // Not sure if this documentation is right.

        /// <summary>
        /// Converts Offset Coordinate to grid coordinates for a hexagonally tiles map
        /// and returns them as a new MapCoordinates object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static MapCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new MapCoordinates(x, z);
        }

        // Not sure if this documentation is right.

        /// <summary>
        /// Converts Offset Coordinate to grid coordinates for a hexagonally tiles map
        /// and returns them as a new MapCoordinates object.
        /// </summary>
        /// <param name="coords">The Coordinates to use to calculate on a grid.</param>
        /// <returns>A new MapCoordinates object without offsets.</returns>
        public static MapCoordinates FromOffsetCoordinates(MapCoordinates coords)
        {
            return new MapCoordinates(coords.x - coords.z / 2, coords.z);
        }

        /// <summary>
        /// Converts this object to a string of format [x, y, z]
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{x}, {y}, {z}]";
        }
        /// <summary>
        /// Converts this object to a string of format
        /// x
        /// y
        /// z
        /// </summary>
        /// <returns></returns>
        public string ToFlatString()
        {
            return $"{x}\n{y}\n{z}";
        }

        /// <summary>
        /// Converts a world space position to a cell on the map, if one exists at that location.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static MapCoordinates FromPosition(Vector3 position)
        {
            float x = position.x / (HexCell.InnerRadius * 2f);
            float y = -x;
            float offset = position.z / (HexCell.OuterRadius * 3f);
            x -= offset;
            y -= offset;
            int iX = Mathf.RoundToInt(x);
            int iY = Mathf.RoundToInt(y);
            int iZ = Mathf.RoundToInt(-x - y);
            if (iX + iY + iZ != 0)
            {
                float dX = Mathf.Abs(x - iX);
                float dY = Mathf.Abs(y - iY);
                float dZ = Mathf.Abs(-x - y - iZ);

                if (dX > dY && dX > dZ)
                {
                    iX = -iY - iZ;
                }
                else if (dZ > dY)
                {
                    iZ = -iX - iY;
                }
            }
            return new MapCoordinates(iX, iZ);
        }
    }
}
