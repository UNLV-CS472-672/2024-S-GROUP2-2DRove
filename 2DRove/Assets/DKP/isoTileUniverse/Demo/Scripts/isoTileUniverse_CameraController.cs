using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace isoTile_Universe
{

    public class isoTileUniverse_CameraController : MonoBehaviour
    {

        public static isoTileUniverse_CameraController GlobalAccess;
        void Awake()
        {
            GlobalAccess = this;
            myTransform = gameObject.transform;
            CameraScript = gameObject.GetComponent<Camera>();
            CameraScript.backgroundColor = Normal_Camera_BackgroundColor;
        }

        private Transform myTransform;
        public Camera CameraScript;
        public Color Normal_Camera_BackgroundColor = Color.white;
        public Color Normal_Camera_TilesetDisplayColor = Color.white;

        public void MoveCameraToPositionInstant(Vector3 worldPositionToMoveTo)
        {
            worldPositionToMoveTo.z = -10f;
            CameraTransform.position = worldPositionToMoveTo;
        }

        public float MoveSpeed = 5.0f;
        public Transform CameraTransform;
        public Transform CameraBounds_BL_Transform;
        public Transform CameraBounds_BR_Transform;
        public Transform CameraBounds_TL_Transform;
        public Transform CameraBounds_TR_Transform;
        public void DisconnectMapBoundGOs()
        {
            if (CameraBounds_BL_Transform != null)
                CameraBounds_BL_Transform.SetParent(null);
            if (CameraBounds_BR_Transform != null)
                CameraBounds_BR_Transform.SetParent(null);
            if (CameraBounds_TL_Transform != null)
                CameraBounds_TL_Transform.SetParent(null);
            if (CameraBounds_TR_Transform != null)
                CameraBounds_TR_Transform.SetParent(null);
        }

        public void SetupCameraBounds(Vector3 startingTilePosition)
        {
            Vector3 startingPosition = startingTilePosition;
            startingPosition.x -= 0.5f;
            startingPosition.y -= 0.5f;
            CameraBounds_BL_Transform.position = startingPosition;

            startingPosition = startingTilePosition;
            startingPosition.x += 0.5f;
            startingPosition.y -= 0.5f;
            CameraBounds_BR_Transform.position = startingPosition;

            startingPosition = startingTilePosition;
            startingPosition.x -= 0.5f;
            startingPosition.y += 0.5f;
            CameraBounds_TL_Transform.position = startingPosition;

            startingPosition = startingTilePosition;
            startingPosition.x += 0.5f;
            startingPosition.y += 0.5f;
            CameraBounds_TR_Transform.position = startingPosition;
        }

        public void Update_CameraBounds(Vector3 newTileWorldPosition)
        {

            // Expand Camera Bounds With Tiles
            // BL
            if (CameraBounds_BL_Transform.position.x > newTileWorldPosition.x - 0.5f)
            {
                Vector3 newPosition = CameraBounds_BL_Transform.position;
                newPosition.x = newTileWorldPosition.x - 0.5f;
                CameraBounds_BL_Transform.position = newPosition;
            }
            if (CameraBounds_BL_Transform.position.y > newTileWorldPosition.y - 0.5f)
            {
                Vector3 newPosition = CameraBounds_BL_Transform.position;
                newPosition.y = newTileWorldPosition.y - 0.5f;
                CameraBounds_BL_Transform.position = newPosition;
            }
            // BR
            if (CameraBounds_BR_Transform.position.x < newTileWorldPosition.x + 0.5f)
            {
                Vector3 newPosition = CameraBounds_BR_Transform.position;
                newPosition.x = newTileWorldPosition.x + 0.5f;
                CameraBounds_BR_Transform.position = newPosition;
            }
            if (CameraBounds_BR_Transform.position.y > newTileWorldPosition.y - 0.5f)
            {
                Vector3 newPosition = CameraBounds_BR_Transform.position;
                newPosition.y = newTileWorldPosition.y - 0.5f;
                CameraBounds_BR_Transform.position = newPosition;
            }
            // TL
            if (CameraBounds_TL_Transform.position.x > newTileWorldPosition.x - 0.5f)
            {
                Vector3 newPosition = CameraBounds_TL_Transform.position;
                newPosition.x = newTileWorldPosition.x - 0.5f;
                CameraBounds_TL_Transform.position = newPosition;
            }
            if (CameraBounds_TL_Transform.position.y < newTileWorldPosition.y + 0.5f)
            {
                Vector3 newPosition = CameraBounds_TL_Transform.position;
                newPosition.y = newTileWorldPosition.y + 0.5f;
                CameraBounds_TL_Transform.position = newPosition;
            }
            // TR
            if (CameraBounds_TR_Transform.position.x < newTileWorldPosition.x + 0.5f)
            {
                Vector3 newPosition = CameraBounds_TR_Transform.position;
                newPosition.x = newTileWorldPosition.x + 0.5f;
                CameraBounds_TR_Transform.position = newPosition;
            }
            if (CameraBounds_TR_Transform.position.y < newTileWorldPosition.y + 0.5f)
            {
                Vector3 newPosition = CameraBounds_TR_Transform.position;
                newPosition.y = newTileWorldPosition.y + 0.5f;
                CameraBounds_TR_Transform.position = newPosition;
            }

        }

        public void Set_CameraZoom_Level1()
        {
            CameraScript.orthographicSize = 2;
        }
        public void Set_CameraZoom_Level2()
        {
            CameraScript.orthographicSize = 3;
        }
        public void Set_CameraZoom_Level3()
        {
            CameraScript.orthographicSize = 6;
        }
        public void Set_CameraZoom_Level4()
        {
            CameraScript.orthographicSize = 12;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKey(KeyCode.W))
            {
                // Move Camera Up
                CameraTransform.Translate(new Vector3(0, MoveSpeed, 0) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                // Move Camera Down
                CameraTransform.Translate(new Vector3(0, -MoveSpeed, 0) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                // Move Camera Left
                CameraTransform.Translate(new Vector3(-MoveSpeed, 0, 0) * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                // Move Camera Right
                CameraTransform.Translate(new Vector3(MoveSpeed, 0, 0) * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                // Set Camera Zoom Level 1
                Set_CameraZoom_Level1();
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                // Set Camera Zoom Level 2
                Set_CameraZoom_Level2();
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                // Set Camera Zoom Level 3
                Set_CameraZoom_Level3();
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                // Set Camera Zoom Level 4
                Set_CameraZoom_Level4();
            }
        }

        void LateUpdate()
        {
            // Constrain Camera Position
            if (CameraTransform.position.x < CameraBounds_TL_Transform.position.x)
                CameraTransform.position = new Vector3(CameraBounds_TL_Transform.position.x, CameraTransform.position.y, -10);
            if (CameraTransform.position.x > CameraBounds_BR_Transform.position.x)
                CameraTransform.position = new Vector3(CameraBounds_BR_Transform.position.x, CameraTransform.position.y, -10);
            if (CameraTransform.position.y > CameraBounds_TR_Transform.position.y)
                CameraTransform.position = new Vector3(CameraTransform.position.x, CameraBounds_TR_Transform.position.y, -10);
            if (CameraTransform.position.y < CameraBounds_BL_Transform.position.y)
                CameraTransform.position = new Vector3(CameraTransform.position.x, CameraBounds_BL_Transform.position.y, -10);
        }

    }

}
