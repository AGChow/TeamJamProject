using System.Collections;
using UnityEngine;

public class StepSwitch : MonoBehaviour
{
    private Player _player;
    private Transform _transform;
    private Vector3 _currentPosition;
    private Vector3 _newPosition;

    void Start() {
        _player = GameObject.FindObjectOfType<Player>();
        _transform = transform;
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")) {
            StartCoroutine(LowerSwitch());
        }
    }

    IEnumerator LowerSwitch() {
        float timer = 0f;
        float timeToPress = 0.2f;
        _currentPosition = _transform.position;
        _newPosition = new Vector3(_currentPosition.x, .02f, _currentPosition.z);

        while(timer < timeToPress) {
            _transform.position = Vector3.Lerp(_currentPosition, _newPosition, (timer / timeToPress));
            timer += Time.deltaTime;

            yield return null;
        }

        _transform.position = _newPosition;
    }
}
