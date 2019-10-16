using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UserInterface : MonoBehaviour
{
    [FormerlySerializedAs("player1HealthBar")] [SerializeField] private GameObject _player1HealthBar;
    [FormerlySerializedAs("player2HealthBar")] [SerializeField] private GameObject _player2HealthBar;
    [FormerlySerializedAs("finishHim")] [SerializeField] private GameObject _finishHim;
    [FormerlySerializedAs("playerWins")] [SerializeField] private GameObject _playerWins;
    [FormerlySerializedAs("flawlessVictory")] [SerializeField] private GameObject _flawlessVictory;
    [FormerlySerializedAs("clips")] [SerializeField] private AudioClip[] _clips;
    [FormerlySerializedAs("fillBarOffset")] [SerializeField] private float _fillBarOffset = 750;
    
    
    private GameStatus _gameStatus;
    
    private Player _player1;
    
    private Player _player2;
    
    private bool _isFinishHim;
    
    private bool _gameOver;
    
    private bool _gameOverFlawless;
    

    private void Start()
    {
        _gameStatus = FindObjectOfType<GameStatus>();
        _player1 = _gameStatus.Player1;
        _player2 = _gameStatus.Player2;
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        _player1HealthBar.GetComponent<RectTransform>().offsetMax = 
            new Vector2(_player1.PlayerStatus.Health*(_fillBarOffset/100)-_fillBarOffset,0);
        _player2HealthBar.GetComponent<RectTransform>().offsetMax =
            new Vector2(_player2.PlayerStatus.Health*(_fillBarOffset/100)-_fillBarOffset,0);
        DisplayFinishHim();
        DisplayGameOver();
    }

    private void DisplayFinishHim()
    {
        if (_player1.PlayerStatus.IsFeelingDizzy || _player2.PlayerStatus.IsFeelingDizzy)
        {
            if (!_isFinishHim)
            {
                GetComponents<AudioSource>()[0].Stop();
                GetComponents<AudioSource>()[1].Stop();
                GetComponents<AudioSource>()[0].clip = _clips[1];
                GetComponents<AudioSource>()[1].clip = _clips[2];
                GetComponents<AudioSource>()[0].Play();
                GetComponents<AudioSource>()[1].Play();
            }
            _finishHim.GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,255);
            _isFinishHim = true;
        }
    }

    private void DisplayGameOver()
    {
        if (_player1.PlayerStatus.IsDead)
        {
            _finishHim.GetComponent<TextMeshProUGUI>().color = new Color32(0,0,0,0);
            _playerWins.GetComponent<TextMeshProUGUI>().text = "Ermac  Wins";
            _playerWins.GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,255);
            if (_player2.PlayerStatus.isHealthComplete())
            {
                _flawlessVictory.GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,255);
                if (!_gameOverFlawless)
                {
                    GetComponents<AudioSource>()[1].clip = _clips[4];
                    GetComponents<AudioSource>()[1].Play();
                }
                _gameOverFlawless = true;
            }
            
            if (!_gameOverFlawless && !_gameOver)
            {
                GetComponents<AudioSource>()[1].clip = _clips[3];
                GetComponents<AudioSource>()[1].Play();
            }

            _gameOver = true;
            
        }
        
        if (_player2.PlayerStatus.IsDead)
        {
            _finishHim.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 0);
            _playerWins.GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,255);
            if (_player1.PlayerStatus.isHealthComplete())
            {
                _flawlessVictory.GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,255);
                if (!_gameOverFlawless)
                {
                    GetComponents<AudioSource>()[1].clip = _clips[4];
                    GetComponents<AudioSource>()[1].Play();
                }
                _gameOverFlawless = true;
            }

            if (!_gameOverFlawless && !_gameOver)
            {
                GetComponents<AudioSource>()[1].clip = _clips[3];
                GetComponents<AudioSource>()[1].Play();
            }

            _gameOver = true;
        }
    }
}
