using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _moveDistance = 4f;
    [SerializeField] private float _speed = 2f;
    private Vector3 startPos;
    private Vector3 targetPos;
    private Coroutine moveCoroutine;
    private bool movingDown = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos;
        Player.Instance.OnInteract += ToggleMove;
    }

    public void ToggleMove()
    {
        movingDown = !movingDown;
        targetPos = movingDown ? startPos + Vector3.down * _moveDistance : startPos;

        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MoveRoutine());
        }
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
                break;

            yield return null;
        }

        moveCoroutine = null;
    }
}
