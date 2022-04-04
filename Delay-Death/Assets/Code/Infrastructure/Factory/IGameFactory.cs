using Assets.Scripts.Infrastructure.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject position);
        GameObject CreatePlayerBodyManager();

        void CreateHud();
        void CreateSpawners();
        void CreateGameStateManager();
    }
}