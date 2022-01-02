using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data")]

public class EnemyData : ScriptableObject
{

    #region Field

    [SerializeField] private string enemyName;
    [SerializeField] private float maxHp;
    [SerializeField] private int points;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float followRange;
    [SerializeField] private float moveCD;

    #endregion

    #region Properties

    public string EnemyName
    {
        get { return enemyName; }
    }
    public float MaxHP
    {
        get { return maxHp; }
    }
    public int Points
    {
        get { return points; }
    }
    public float Speed
    {
        get { return speed; }
    }
    public float AttackRange
    {
        get { return attackRange; }
    }
    public float FollowRange
    {
        get { return followRange; }
    }
    public float MovementCD
    {
        get { return moveCD; }
    }

    #endregion

}
