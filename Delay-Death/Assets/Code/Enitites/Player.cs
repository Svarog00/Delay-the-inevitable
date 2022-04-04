using Assets.Code.Enitites.Enemies.StateMachine;
using UnityEngine;

namespace Assets.Code.Enitites
{
    public class Player : Entity
    {
        private void OnEnable()
        {
            StartWith<ControlledEntityState>();
        }
    }
}
