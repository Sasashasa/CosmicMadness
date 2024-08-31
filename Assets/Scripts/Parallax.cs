using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _parallaxEffect;
    
    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float distX = _target.transform.position.x * _parallaxEffect;
        float distY = _target.transform.position.y * _parallaxEffect;

        transform.position = new Vector3(startPos.x + distX, startPos.y + distY, transform.position.z);
    }
}
