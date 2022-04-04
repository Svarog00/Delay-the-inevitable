using Assets.Code.Global;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrustructure;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class LoadLevelState : IPayloadState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded);
        }

        private void OnLoaded()
        {
            _gameFactory.CreateGameStateManager();
            GameObject bodyManager = _gameFactory.CreatePlayerBodyManager();

            _gameFactory.CreateHud();

            GameObject hero = _gameFactory.CreateHero(GameObject.FindGameObjectWithTag(InitialPointTag));
            bodyManager.GetComponent<PlayerBodyManager>().SetStartBody(hero);

            _gameFactory.CreateSpawners();
     
            CameraFollow(hero);

            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main.
                GetComponent<CameraFollow>().
                Follow(hero);
        }


        public void Exit()
        {
            
        }
    }
}