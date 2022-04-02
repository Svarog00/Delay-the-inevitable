using Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public void Follow(GameObject hero)
    {
        GetComponentInChildren<CinemachineVirtualCamera>().Follow = hero.transform;
    }
}
