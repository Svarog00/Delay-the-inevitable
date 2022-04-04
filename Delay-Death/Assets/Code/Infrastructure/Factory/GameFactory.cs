using Assets.Scripts.Infrastructure.AssetManagment;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assets = assetProvider;
        }

        public void CreateGameStateManager()
        {
            _assets.Instantiate(AssetPaths.GameStateManager);
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            return _assets.Instantiate(AssetPaths.PlayerPath, position: initialPoint.transform.position);
        }

        public void CreateHud()
        {
            _assets.Instantiate(AssetPaths.UIPath);
        }

        public GameObject CreatePlayerBodyManager()
        {
            return _assets.Instantiate(AssetPaths.PlayerBodyManager);
        }

        public void CreateSpawners()
        {
            _assets.Instantiate(AssetPaths.Setup);
        }
    }
}
