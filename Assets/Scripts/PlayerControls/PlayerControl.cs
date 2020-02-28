using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public void Init()
        {
            cameraMove = FindObjectOfType<CameraMove>();
            cameraMove.Init(FindObjectOfType<Camera>());
        }

        public void SetPause(bool pause)
        {
            cameraMove.SetActive(!pause);
        }

        public void Destroy()
        {
            cameraMove.Destroy();
        }


    }
}