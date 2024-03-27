using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    
    private Transform _playerTransform;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        _playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, _playerTransform.position.x + offset.x, Time.deltaTime),
                                         Mathf.Lerp(transform.position.y, _playerTransform.position.y + offset.y, Time.deltaTime),
                                         transform.position.z);
    }
}
