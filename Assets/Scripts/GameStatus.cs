using UnityEngine;
using UnityEngine.Serialization;

public class GameStatus : MonoBehaviour
{
    
    [FormerlySerializedAs("player1Health")] [SerializeField] private float _player1Health;
    [FormerlySerializedAs("player2Health")] [SerializeField] private float _player2Health;
    [FormerlySerializedAs("damage")] [SerializeField] private float _damage;
    [FormerlySerializedAs("FinishingTime")] [SerializeField] private float _finishingTime;
    
    private Player _player1;
    
    private Player _player2;

    private void Start()
    {
        Player[] players = FindObjectsOfType<Player>();
        
        if (players[0].IsFirstPlayer)
        {
            _player1 = players[0];
            _player2 = players[1];
        }
        else
        {
            _player1 = players[1];
            _player2 = players[0];
        }

        _player1.PlayerStatus.Health = _player1Health;
        _player2.PlayerStatus.Health = _player2Health;
    }

    private void Update()
    {
        Debug.Log(_player2.PlayerStatus.Health);
        Damage();
        FinishHim();
    }

    private void Damage()
    {
        if (_player1.PlayerStatus.HasCollisionWithOpponent &&
            _player1.PlayerStatus.IsAttacking &&
            !_player2.PlayerStatus.IsDefending)
        {
            if (_player2.PlayerStatus.Health > 0)
            {
                _player2.GetHurt();
                _player2.PlayerStatus.Health -= _damage;
            }
            else
            {
                FeelDizzy(_player2);
            }
        } 
        
        if (_player2.PlayerStatus.HasCollisionWithOpponent &&
            _player2.PlayerStatus.IsAttacking &&
            !_player1.PlayerStatus.IsDefending)
        {
            if (_player1.PlayerStatus.Health > 0)
            {
                _player1.GetHurt();
                _player1.PlayerStatus.Health -= _damage;
            }
            else
            {
                FeelDizzy(_player1);
            }
        } 
        
        if (_player1.PlayerStatus.Health <= 0)
        {
            FeelDizzy(_player1);
        }
        
        if (_player2.PlayerStatus.Health <= 0)
        {
            FeelDizzy(_player2);
        }
        
        
    }
    
    private void FinishHim()
    {
        if (_player1.PlayerStatus.IsFeelingDizzy && _player2.PlayerStatus.IsAttacking && 
            _player1.PlayerStatus.Health < (-1)*_finishingTime)
        {
            Die(_player1,_player2);
        } else if (_player1.PlayerStatus.IsFeelingDizzy && _player2.PlayerStatus.IsAttacking)
        {
            _player1.PlayerStatus.Health -= _damage;
        }
        
        if (_player2.PlayerStatus.IsFeelingDizzy && _player1.PlayerStatus.IsAttacking && 
            _player2.PlayerStatus.Health < (-1)*_finishingTime)
        {
            Die(_player2,_player1);
        } else if (_player2.PlayerStatus.IsFeelingDizzy && _player1.PlayerStatus.IsAttacking)
        {
            _player2.PlayerStatus.Health -= _damage;
        } 
        
        if (_player1.PlayerStatus.Health < (-1)*_finishingTime)
        {
            Die(_player1,_player2);
        }
        
        if (_player2.PlayerStatus.Health < (-1)*_finishingTime)
        {
            Die(_player2,_player1);
        }
        
    }

    private void FeelDizzy(Player player)
    {
        player.SetAllAnimationsOff();
        player.PlayerStatus.IsFeelingDizzy = true;
        player.GetComponent<Animator>().SetTrigger("FeelingDizzy");
    }

    private void Die(Player loser,Player winner)
    {
        loser.PlayerStatus.IsFeelingDizzy = false;
        loser.PlayerStatus.IsDead = true;
        loser.SetAllAnimationsOff();
        loser.GetComponent<Animator>().SetTrigger("Dying");
        HasWon(winner);
    }

    private void HasWon(Player player)
    {
        player.SetAllAnimationsOff();
        player.HasWon();
    }

    public Player Player1 => _player1;

    public Player Player2 => _player2;

    public float Damage1 => _damage;
}
