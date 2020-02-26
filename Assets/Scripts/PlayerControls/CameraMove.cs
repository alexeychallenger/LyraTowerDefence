using UnityEngine;
using LTD.Map.LevelDesing;

namespace LTD.PlayerControls
{
    public class CameraMove : MonoBehaviour
    {
        private const float PANNING_WIDTH = 0.96f;
        private Camera currentCamera;
        private bool isActive = false;

        [SerializeField] private float turnSpeed = 0.8f;

        public void Init(Camera camera)
        {
            currentCamera = camera;
        }

        public void Destroy()
        {
            currentCamera = null;
            SetActive(false);
        }

        public void SetActive(bool active)
        {
            isActive = active;
        }

        void LateUpdate()
        {
            if (currentCamera == null)
                return;

            if (!isActive)
                return;

            CameraMoving();
            MovementHandler();
        }

        private void MovementHandler()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GameObject collider;
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2000, 1 << LayerMask.NameToLayer("Map")))
            {
                collider = hit.collider.gameObject;

                if (Input.GetMouseButtonDown(0))
                {
                    Debug.DrawLine(transform.position, hit.point);
                    Level.Instance.SelectTile(hit.point);
                }


                //floatedHexagon.position = collider.transform.position;
                //playerui.UpdateFloated(collider.GetComponent<Hex>());

                //if (Input.GetMouseButtonDown(0))
                //{
                //    Hex h = collider.GetComponent<Hex>();
                //    try
                //    {
                //        playerui.LeftClick(h.point);
                //    }
                //    catch
                //    {
                //        print(h.name + " " + h.point.ToString());
                //    }
                //}
            }
        }

        void CameraMoving()
        {

            Vector3 pos = Input.mousePosition;
            pos.x = (pos.x - Screen.width / 2) * 2f / Screen.width;
            pos.y = (pos.y - Screen.height / 2) * 2f / Screen.height;

            Vector3 move = new Vector3();
            int vertical = 0;
            int horizontal = 0;

            if (pos.x < -PANNING_WIDTH) // Left
            {
                horizontal = -1;
            }
            else if (PANNING_WIDTH < pos.x) // Right
            {
                horizontal = 1;
            }

            if (pos.y < -PANNING_WIDTH) // Down
            {
                vertical = -1;
            }
            else if (PANNING_WIDTH < pos.y) // Up
            {
                vertical = 1;
            }

            move = new Vector3(horizontal * turnSpeed, vertical * turnSpeed, 0);
            move = Quaternion.Euler(-currentCamera.transform.eulerAngles.x, 0, 0) * move;
            currentCamera.transform.Translate(move, Space.Self);
        }
    }
}
