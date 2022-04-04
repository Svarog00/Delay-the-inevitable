using System;
using UnityEngine;

namespace Assets.Code.Global
{
    public class PlayerBodyManager : MonoBehaviour
    {
        public event EventHandler OnBodySwapped;

        private GameObject _currentBody;

        public void SetStartBody(GameObject body)
        {
            _currentBody = body;
        }

        public void SwapBodies(GameObject newBody)
        {
            _currentBody.SetActive(false);
            _currentBody = newBody;
            ChangeCameraTarget(newBody);
            OnBodySwapped?.Invoke(this, EventArgs.Empty);
        }

        public void ChangeCameraTarget(GameObject target)
        {
            Camera.main.
                GetComponent<CameraFollow>().
                Follow(target);
        }
    }
}
