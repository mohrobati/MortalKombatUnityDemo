using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

public class Player : MonoBehaviour
{
    
    [FormerlySerializedAs("isFirstPlayer")] [SerializeField] private bool _isFirstPlayer;
    [FormerlySerializedAs("opponent")] [SerializeField] private GameObject _opponent;
    [FormerlySerializedAs("clips")] [SerializeField] private AudioClip[] _clips;
    [FormerlySerializedAs("acidPrefab")] [SerializeField] private GameObject _acidPrefab;
    [FormerlySerializedAs("playerSpeed")] [SerializeField] private float _playerSpeed;
    [FormerlySerializedAs("acidSpeed")] [SerializeField] private float _acidSpeed;
    [FormerlySerializedAs("acidOffset")] [SerializeField] private float _acidOffset;
    [FormerlySerializedAs("jumpSpeed")] [SerializeField] private float _jumpForce;
    
    [SerializeField] private string _up;
    [SerializeField] private string _down;
    [SerializeField] private string _right;
    [SerializeField] private string _left;
    [SerializeField] private string _defense;
    [SerializeField] private string _kick;
    [SerializeField] private string _punch;
    [SerializeField] private string _skill;
    
    private Controls _playerControls;

    private PlayerStatus _playerStatus;

    private void Awake()
    {
        PlayerStatus player1Status = new PlayerStatus();
        PlayerStatus player2Status = new PlayerStatus();
        if (_isFirstPlayer) _playerStatus = player1Status;
        else _playerStatus = player2Status;
        _playerControls = new Controls(_up,_down,_right,_left,_defense,_kick,_punch,_skill);
    }

    void Update()
    {
        UpdateVelocity();
        UpdatePlayerStatus();
        CheckForInputs();
        CheckIfFlipNeeded();
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void CheckForInputs()
    {
        if (!(_playerStatus.IsFeelingDizzy || _playerStatus.IsDead))
        {
            if (!_playerStatus.IsDefending)
            {
                Walk();
                Jump();
                Sit();
                Punch();
                Kick();
                ThrowAcid();
            }
            Defend();
        } 
    }

    private void UpdateVelocity()
    {
        PlayerStatus.Velocity = GetComponent<Rigidbody2D>().velocity;
    }

    private void UpdatePlayerStatus()
    {
        if (_playerStatus.IsDefending) PlayerStatus.IsDefending = true;
        else PlayerStatus.IsDefending = false;
        
        if (_playerStatus.IsKicking || _playerStatus.IsPunching) PlayerStatus.IsAttacking = true;
        else PlayerStatus.IsAttacking = false;
    }

    private void CheckIfFlipNeeded()
    {
            if (_opponent.transform.position.x - transform.position.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
   
       
    }

    private void Walk()
    {
        if (Input.GetKey(_playerControls.Right) && !_playerStatus.IsSitted)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(_playerSpeed,PlayerStatus.Velocity.y);
            GetComponent<Animator>().SetBool("Walking",true);
            _playerStatus.IsWalking = true;
        } else if (Input.GetKey(_playerControls.Left) && !_playerStatus.IsSitted)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1*_playerSpeed,PlayerStatus.Velocity.y);
            GetComponent<Animator>().SetBool("Walking",true);
            _playerStatus.IsWalking = true;
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking",false);
            _playerStatus.IsWalking = false;
        }
    }

    private void Jump()
    {
        if (!_playerStatus.IsSitted)
        {
            if (Input.GetKey(_playerControls.Up) && _playerStatus.IsOnTheFloor)
            {
                if (!_playerStatus.IsJumping)
                {
                    
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, _jumpForce));
                    GetComponent<Animator>().SetBool("Jumping", true);
                    _playerStatus.IsJumping = true;
                    GetComponent<AudioSource>().clip = _clips[2];
                    GetComponent<AudioSource>().Play();
                }    
                _playerStatus.IsOnTheFloor = false;
            }
            else
            {
                GetComponent<Animator>().SetBool("Jumping", false);
                _playerStatus.IsJumping = false;
            }
        }
}

    private void Sit()
    {
        if (Input.GetKey(_playerControls.Down))
        {
            if (!_playerStatus.IsSitted)
            {
                GetComponent<Animator>().SetBool("Sitting", true);
                _playerStatus.IsSitted = true;
                GetComponent<AudioSource>().clip = _clips[3];
                GetComponent<AudioSource>().Play();
            } 
        }
        else
        {
            GetComponent<Animator>().SetBool("Sitting",false);
            _playerStatus.IsSitted = false;
        }
    }

    private void Punch()
    {
        if (Input.GetKey(_playerControls.Punch))
        {
            if (!_playerStatus.IsPunching)
            {
                if(_playerStatus.IsSitted) GetComponent<Animator>().SetBool("UnderPunching", true);
                else GetComponent<Animator>().SetBool("Punching", true);
                _playerStatus.IsPunching = true;
                GetComponent<AudioSource>().clip = _clips[0];
                GetComponent<AudioSource>().Play();
            }
            else
            {
                if (_playerStatus.IsSitted) GetComponent<Animator>().SetBool("UnderPunching", false);
                else GetComponent<Animator>().SetBool("Punching", false);
            }           
        }
        else
        {
            if(_playerStatus.IsSitted) GetComponent<Animator>().SetBool("UnderPunching", false);
            else GetComponent<Animator>().SetBool("Punching", false);
            _playerStatus.IsPunching = false;
        }
    }

    private void Kick()
    {
        if (Input.GetKey(_playerControls.Kick))
        {
            if (!_playerStatus.IsKicking)
            {
                if(_playerStatus.IsSitted) GetComponent<Animator>().SetBool("UnderKicking", true);
                else GetComponent<Animator>().SetBool("Kicking", true);
                _playerStatus.IsKicking = true;
                GetComponent<AudioSource>().clip = _clips[1];
                GetComponent<AudioSource>().Play();
            }
            else
            {
                if (_playerStatus.IsSitted) GetComponent<Animator>().SetBool("UnderKicking", false);
                else GetComponent<Animator>().SetBool("Kicking", false);
            }           
        }
        else
        {
            if(_playerStatus.IsSitted) GetComponent<Animator>().SetBool("UnderKicking", false);
            else GetComponent<Animator>().SetBool("Kicking", false);
            _playerStatus.IsKicking = false;
        }
    }


    private void Defend()
    {
        if (Input.GetKey(_playerControls.Defense))
        {
            if (_playerStatus.IsWalking)
            {
                GetComponent<Animator>().SetBool("Walking", false);
                _playerStatus.IsWalking = false;
            }
            GetComponent<Animator>().SetBool("Defending", true);
            _playerStatus.IsDefending = true;
        }
        else
        {
            GetComponent<Animator>().SetBool("Defending", false);
            _playerStatus.IsDefending = false;
        }
    }

    private void ThrowAcid()
    {
        if (Input.GetKey(_playerControls.Skill))
        {
            if (!_playerStatus.DoingSkill)
            {
                GetComponent<Animator>().SetBool("Acid", true);
                GetComponent<Animator>().speed = 2;
                _playerStatus.DoingSkill = true;
                GetComponent<AudioSource>().clip = _clips[4];
                GetComponent<AudioSource>().Play();
                GameObject acid = Instantiate(_acidPrefab,
                    new Vector3(transform.position.x ,transform.position.y + transform.lossyScale.y*_acidOffset
                        ,transform.position.z), 
                    Quaternion.identity);

                if (transform.position.x - _opponent.transform.position.x < 0)
                {
                    acid.GetComponent<Rigidbody2D>().velocity = 
                        new Vector2(_acidSpeed,0);
                }
                else
                {
                    acid.transform.localScale = new Vector3(-1,1,1);
                    acid.GetComponent<Rigidbody2D>().velocity = 
                        new Vector2(-1*_acidSpeed,0);
                }
                
                if (_isFirstPlayer) acid.tag = "Player1";
                else acid.tag = "Player2";
            }
            else
            {
                GetComponent<Animator>().SetBool("Acid", false);
            }           
        }
        else
        {
            GetComponent<Animator>().SetBool("Acid", false);
            _playerStatus.DoingSkill = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((_isFirstPlayer && other.gameObject.name == "Player2")
            || (!_isFirstPlayer && other.gameObject.name == "Player1")) {PlayerStatus.HasCollisionWithOpponent = true;}
        else PlayerStatus.HasCollisionWithOpponent = false;        
    }

    public void SetAllAnimationsOff()
    {
        GetComponent<Animator>().SetBool("Walking", false);
        GetComponent<Animator>().SetBool("Jumping", false);
        GetComponent<Animator>().SetBool("Defending", false);
        GetComponent<Animator>().SetBool("FeelingDizzy", false);
        GetComponent<Animator>().SetBool("Dying", false);
        GetComponent<Animator>().SetBool("Freezing", false);
        GetComponent<Animator>().SetBool("GetOverHere", false);
        GetComponent<Animator>().SetBool("Kicking", false);
        GetComponent<Animator>().SetBool("Punching", false);
        GetComponent<Animator>().SetBool("Sitting", false);
        GetComponent<Animator>().SetBool("UnderPunching", false);
        GetComponent<Animator>().SetBool("UnderKicking", false);
        GetComponent<Animator>().SetBool("GettingHurt", false);
        GetComponent<Animator>().SetBool("Acid", false);
    }

    public void GetHurt()
    {
        StartCoroutine(SetHurtAnimation());
    }

    public void HasWon()
    {
        GetComponent<Animator>().SetBool("HasWon",true);
    }
    
    IEnumerator SetHurtAnimation()
    {
        GetComponent<Animator>().SetBool("GettingHurt",true);
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animator>().SetBool("GettingHurt",false);
    }
    
    public PlayerStatus PlayerStatus => _playerStatus;

    public bool IsFirstPlayer => _isFirstPlayer;
    
}
