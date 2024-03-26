// // ONLY TO BE USED FOR TESTING PURPOSES
// using System;
// using System.Collections.Generic;
// using UnityEngine;

// // /// <summary>
// // /// Class responsible for creating a border line in a 2D space.
// // /// </summary>
// namespace CreateBorder
// {
//     public class DrawBorder : MonoBehaviour
//     {
//         [SerializeField] float lineWidth = 1f;

//         /// <summary>
//         /// Draws a border line using a LineRenderer component.
//         /// </summary>
//         /// <param name="minX">The minimum X coordinate of the border.</param>
//         /// <param name="minY">The minimum Y coordinate of the border.</param>
//         /// <param name="maxX">The maximum X coordinate of the border.</param>
//         /// <param name="maxY">The maximum Y coordinate of the border.</param>
//         public void DrawBorderLine(int minX, int minY, int maxX, int maxY)
//         {
//             // Create a new GameObject and add a LineRenderer component to it
//             GameObject borderLine = new GameObject("BorderLine");
//             LineRenderer lineRenderer = borderLine.AddComponent<LineRenderer>();

//             // Assign the default material for line renderer
//             lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            
//             // Set the number of positions in the LineRenderer to 5 (4 corners + close the loop)
//             lineRenderer.positionCount = 5;

//             // Calculate the new positions based on isometric projection
//             Vector3[] isometricPositions = new Vector3[5];
//             float cos = Mathf.Cos(45 * Mathf.Deg2Rad);
//             float sin = Mathf.Sin(45 * Mathf.Deg2Rad);

//             // Bottom left
//             isometricPositions[0] = PointToIso(new Vector2(minX, minY), cos, sin);
//             // Bottom right
//             isometricPositions[1] = PointToIso(new Vector2(maxX, minY), cos, sin);
//             // Top right
//             isometricPositions[2] = PointToIso(new Vector2(maxX, maxY), cos, sin);
//             // Top left
//             isometricPositions[3] = PointToIso(new Vector2(minX, maxY), cos, sin);
//             // Close the loop
//             isometricPositions[4] = isometricPositions[0];

//             // Set the positions of the LineRenderer to form the isometric rectangle
//             for (int i = 0 ; i < isometricPositions.Length ; i++)
//             {
//                 lineRenderer.SetPosition(i, isometricPositions[i]);
//             }

//             // Set the color and width of the LineRenderer
//             lineRenderer.startColor = Color.black;
//             lineRenderer.endColor = Color.black;
//             lineRenderer.startWidth = lineWidth;
//             lineRenderer.endWidth = lineWidth;
//         }

//         private Vector3 PointToIso(Vector2 point, float cos, float sin)
//         {
//             // Convert point to isometric
//             return new Vector3(
//                 point.x * cos - point.y * sin,
//                 (point.x * sin + point.y * cos) / 2, // Dividing Y by 2 to flatten the isometric view
//                 0
//             );
//         }
//     }
// }