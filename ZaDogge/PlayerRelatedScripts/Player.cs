using System;
using UnityEngine;
using Sadge;
public class Player : PersistentSingleton<Player>
{
    public event Action OnInteract;
    [SerializeField] private LayerMask _InteractableLayer;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 origin = new(transform.position.x, transform.position.y);
            Collider2D hit = Physics2D.OverlapBox(origin + Vector2.up, Vector2.one, 0f, _InteractableLayer);

            Debug.Log("fired");
            OnInteract?.Invoke();
        }
    }
    // public void Oninteract(InputAction.CallbackContext context)
    // {
    //Collider2D collider = Physics2D.OverlapBox(transform.position, new(1, 1), LayerMask.NameToLayer("Interactable"));
    //     var collider = Physics.OverlapSphere(transform.position, 1.23f, LayerMask.NameToLayer("Interactable")).FirstOrDefault();
    //     Debug.Log(collider.name);
    //     timedMover = collider.gameObject.GetComponent<TimedMover>();


    //     if (timedMover != null)
    //     {
    //         timedMover.Begin();
    //     }


    // }
}