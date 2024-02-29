// ONLY TO BE USED FOR TESTING PURPOSES
// using System;
// using System.Collections.Generic;
// using UnityEngine;

// /// <summary>
// /// Class responsible for creating a border line in a 2D space.
// /// </summary>
// namespace CreateBorder
// {
//     public class DrawBorder : MonoBehaviour
//     {
//         [SerializeField] float lineWidth = 20f;
//         /// <summary>
//         /// Draws a border line using a LineRenderer component.
//         /// </summary>
//         /// <param name="minX">The minimum X coordinate of the border.</param>
//         /// <param name="minY">The minimum Y coordinate of the border.</param>
//         /// <param name="maxX">The maximum X coordinate of the border.</param>
//         /// <param name="maxY">The maximum Y coordinate of the border.</param>
//         public void DrawBorderLine(int minX, int minY, int maxX, int maxY)
//         {
//             //Create a new GameObject and add a LineRenderer component to it
//             GameObject borderLine = new GameObject("BorderLine");
//             //Add a LineRenderer component to the GameObject
//             LineRenderer lineRenderer = borderLine.AddComponent<LineRenderer>();

//             //Assign the default material for linerender
//             lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            
//             //Set the number of positions in the LineRenderer to 5
//             lineRenderer.positionCount = 5;
//             //Set the positions of the LineRenderer to form a rectangle
//             lineRenderer.SetPosition(0, new Vector3(minX, minY, 0));
//             lineRenderer.SetPosition(1, new Vector3(maxX, minY, 0));
//             lineRenderer.SetPosition(2, new Vector3(maxX, maxY, 0));
//             lineRenderer.SetPosition(3, new Vector3(minX, maxY, 0));
//             lineRenderer.SetPosition(4, new Vector3(minX, minY, 0));

//             //Set the color and width of the LineRenderer
//             lineRenderer.startColor = Color.black;
//             lineRenderer.endColor = Color.black;
//             //Set the width of the LineRenderer
//             lineRenderer.startWidth = lineWidth;
//             lineRenderer.endWidth = lineWidth;
//         }
//     }
// }
