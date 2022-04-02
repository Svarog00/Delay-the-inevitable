using Assets.Code.Enitites;
using Assets.Code.Enitites.Enemies;
using Assets.Code.Enitites.Enemies.StateMachine;
using Assets.Scripts.Infrastructure.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float DistanceToPlayer => _distanceToPlayer;

    public float AgressionDistance { get; internal set; }

    private const string PlayerTag = "Player";

    [SerializeField] private int _healthPoints;

    private EntityStateMachine _stateMachine;

    private EnemyHealth _enemyHealth;

    private GameObject _player;

    private float _distanceToPlayer;

    // Start is called before the first frame update
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(PlayerTag);

        _enemyHealth = new EnemyHealth(_healthPoints);
        _enemyHealth.OnDieEventHandler += _enemyHealth_OnDieEventHandler;

        _stateMachine = new EntityStateMachine();
        _stateMachine.States = new Dictionary<Type, IEntityState>
        {
            [typeof(ControlledEntityState)] = new ControlledEntityState(_stateMachine, this, ServiceLocator.Container.Single<IInputService>()),
            [typeof(ChaseState)] = new ChaseState(this, _stateMachine),
            [typeof(EngageState)] = new EngageState(this, _stateMachine),
        };
    }

    private void OnEnable()
    {
        _stateMachine.Enter<ChaseState>();
        _enemyHealth.Reset();
    }
    
    // Update is called once per frame
    private void Update()
    {
        _stateMachine.Work();
    }

    private void _enemyHealth_OnDieEventHandler(object sender, EventArgs e)
    {
        _stateMachine.ChangeState<ControlledEntityState>();
    }

    public Vector2 GetDirectionToPlayer()
    {
        Vector2 vectorToPlayer = _player.transform.position - transform.position;
        return vectorToPlayer / vectorToPlayer.magnitude;
    }
}
