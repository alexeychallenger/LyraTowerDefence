using UnityEngine;
using LTD.Map.LevelDesing;

namespace LTD.PlayerControls
{
    public class PlayerControl : MonoBehaviour
    {
        private static PlayerControl _instance;
        public static PlayerControl Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PlayerControl>();
                    if(_instance != null)
                    {
                        _instance.Init();
                    }
                }
                return _instance;
            }
        }

        private CameraMove cameraMove;
        private bool buildingMode = false;
        private string buildingName;

        public void Init()
        {
            cameraMove = FindObjectOfType<CameraMove>();
            cameraMove.Init(FindObjectOfType<Camera>());
            cameraMove.OnLevelClick += PlaceBuilding;
        }

        public void SetPause(bool pause)
        {
            cameraMove.SetActive(!pause);
        }

        public void Destroy()
        {
            cameraMove.Destroy();
        }

        public void StartBuilding(string key)
        {
            buildingMode = true;
            buildingName = key;
        }
        public void PlaceBuilding(Vector2Int position)
        {
            if (!buildingMode)
                return;

            StopBuilding();
            var obj = BuildingFactory(buildingName);
            Level.Instance.PlaceBuilding(obj, position);
        }

        public void StopBuilding()
        {
            buildingMode = false;
        }

        private void Update()
        {
            if (buildingMode)
            {
                if (Input.GetKey(KeyCode.Escape))
                {

                }
            }
        }

        private GameObject BuildingFactory(string key)
        {
            switch (key) {
                case "clock":
                    {
                        return (GameObject)Resources.Load("Prefabs/Buildings/ClockTower/ClockTower");
                    }
                case "windmill":
                    {
                        return (GameObject)Resources.Load("Prefabs/Buildings/Windmill/Windmill");
                    }
            }
            throw new System.Exception($"cant find build named [{key}]");
        }
    }
}