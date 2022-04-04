using Assets.Code.Enitites;
using Assets.Code.Enitites.Enemies;
using Assets.Code.Enitites.Enemies.StateMachine;
using Assets.Code.Global;
using Assets.Code.UI;
using Assets.Code.Utility;
using Assets.Scripts.Infrastructure.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamagable, IPoolable
{
    public float AgressionDistance { get; internal set; }

    private const string PlayerTag = "Player";

    [SerializeField] private UiInformerScript _uiInformer;
    [SerializeField] private int _healthPoints;
    [SerializeField] private float _timeForKill;
    [SerializeField] private float _decayTime;

    private GameStateManager _gameStateManager;
    private PlayerBodyManager _bodyManager;
    private ObjectPool _objectPool;
    private EntityStateMachine _stateMachine;
    private EntityHealth _enemyHealth;
    private GameObject _player;
    private IInputService _gameInput;

    // Start is called before the first frame update
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
        _gameStateManager = FindObjectOfType<GameStateManager>();

        _bodyManager = FindObjectOfType<PlayerBodyManager>();
        _bodyManager.OnBodySwapped += BodyManager_OnBodySwapped;

        _enemyHealth = new EntityHealth(_healthPoints);
        _enemyHealth.OnDieEventHandler += _enemyHealth_OnDieEventHandler;

        _stateMachine = new EntityStateMachine();
        _stateMachine.States = new Dictionary<Type, IEntityState>
        {
            [typeof(ControlledEntityState)] = new ControlledEntityState(_stateMachine, this, ServiceLocator.Container.Single<IInputService>()),
            [typeof(DecayState)] = new DecayState(this, _stateMachine, _decayTime),
            [typeof(ChaseState)] = new ChaseState(this, _stateMachine),
            [typeof(EngageState)] = new EngageState(this, _stateMachine),
        };

        _gameInput = ServiceLocator.Container.Single<IInputService>();
    }

    private void BodyManager_OnBodySwapped(object sender, EventArgs e)
    {
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
    }

    private void OnEnable()
    {
        StartWith<ChaseState>();
        _enemyHealth.Reset();
    }
    
    protected void StartWith<TState>() where TState : class, IEntityState
    {
        _stateMachine.Enter<TState>();
    }

    public float GetDistanceToPlayer()
    {
        Vector2 vectorToPlayer = _player.transform.position - transform.position;
        return vectorToPlayer.magnitude;
    }

    public Vector2 GetDirectionToPlayer()
    {
        Vector2 vectorToPlayer = _player.transform.position - transform.position;
        return vectorToPlayer / vectorToPlayer.magnitude;
    }

    public void Hurt(int damage)
    {
        _enemyHealth.Hurt(damage);
    }

    public void SetPool(ObjectPool objectPool)
    {
        _objectPool = objectPool;
    }

    private void Update()
    {
        _stateMachine.Work();
    }

    private void _enemyHealth_OnDieEventHandler(object sender, EventArgs e)
    {
        if (!(_stateMachine.CurrentState is ControlledEntityState))
        {
            _gameStateManager.CountKill();
            _uiInformer.Appear(0f);
            _stateMachine.Enter<DecayState>();
        }
    }

    private void OnMouseOver()
    {
        if(_gameInput.IsInteractButtonDown())
        {
            if(_stateMachine.CurrentState is DecayState)
            {
                AssumeControl();
            }
        }
    }

    private void AssumeControl()
    {
        _uiInformer.Disappear(.1f);
        _stateMachine.Enter<ControlledEntityState>();
        _gameStateManager.ResetTimer(_timeForKill);
        _bodyManager.SwapBodies(gameObject);
    }

    private void OnDisable()
    {
        _objectPool.AddToPool(gameObject);
    }

}
