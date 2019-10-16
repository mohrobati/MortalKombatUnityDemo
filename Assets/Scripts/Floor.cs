using UnityEngine;

public class Floor : MonoBehaviour
{
    private Player[] _players;
    
    private void Start()
    {
        _players = FindObjectsOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach (var player in _players)
        {
            if (player.IsFirstPlayer && other.gameObject.name == "Player1")
            {
                player.PlayerStatus.IsOnTheFloor = true;
            }
            
            if (!player.IsFirstPlayer && other.gameObject.name == "Player2")
            {
                player.PlayerStatus.IsOnTheFloor = true;
            }
        }
    }
}
