using UnityEngine;
using UnityEngine.Serialization;

public class Acid : MonoBehaviour
{

    [FormerlySerializedAs("acidDamage")] [SerializeField] private float _acidDamage;
    [FormerlySerializedAs("getAcid")] [SerializeField] private AudioClip _getAcid;
    
    private GameStatus _gameStatus;
    
    private void Start()
    {
        _gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player1" && !CompareTag("Player1"))
        {
            if (!_gameStatus.Player1.PlayerStatus.IsDefending)
                _gameStatus.Player1.PlayerStatus.Health -= _gameStatus.Damage1*_acidDamage;
            _gameStatus.Player1.GetHurt();
            _gameStatus.Player1.GetComponent<AudioSource>().clip = _getAcid;
            _gameStatus.Player1.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        } else if (other.name == "Player2" && !CompareTag("Player2"))
        {
            if (!_gameStatus.Player2.PlayerStatus.IsDefending)
                _gameStatus.Player2.PlayerStatus.Health -= _gameStatus.Damage1*_acidDamage;
            _gameStatus.Player2.GetHurt();
            _gameStatus.Player2.GetComponent<AudioSource>().clip = _getAcid;
            _gameStatus.Player2.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        else if(other.name == "RightWall" || other.name == "LeftWall")
        {
            Destroy(gameObject);
        }
    }
}
