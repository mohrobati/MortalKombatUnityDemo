using UnityEngine;
using UnityEngine.Serialization;

public class CinemachineScript : MonoBehaviour
{
    [FormerlySerializedAs("player1")] [SerializeField] private GameObject _player1;
    [FormerlySerializedAs("player2")] [SerializeField] private GameObject _player2;

    // Update is called once per frame
    void Update()
    {
        transform.position = (_player1.transform.position + _player2.transform.position) / 2;
    }
}
