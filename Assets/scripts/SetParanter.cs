using UnityEngine;

public class SetParanter : MonoBehaviour
{
    private Transform _myTransform;

    private void Start()
    {
        _myTransform = GetComponent<Transform>();   
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject enteredObject = other.gameObject;
        enteredObject.transform.SetParent(_myTransform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        GameObject enteredObject = other.gameObject;
        enteredObject.transform.parent = null;
    }
  
}
