using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlaySpaceBounds : MonoBehaviour
{
    [FormerlySerializedAs("rightWall")] [SerializeField] private GameObject _rightWall;
    [FormerlySerializedAs("leftWall")] [SerializeField] private GameObject _leftWall;
    [FormerlySerializedAs("lookPoint")] [SerializeField] private GameObject _lookPoint;
    [FormerlySerializedAs("lookPointOffset")] [SerializeField] private float _lookPointOffset = 7;
    
    
    private void Update()
    {
        _rightWall.transform.position = new Vector3(_lookPoint.transform.position.x + _lookPointOffset,
                                                    _rightWall.transform.position.y,0);
        _leftWall.transform.position = new Vector3(_lookPoint.transform.position.x - _lookPointOffset,
            _leftWall.transform.position.y,0);
    }
    
}
