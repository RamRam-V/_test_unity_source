using System.Collections;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float autoDestoryTime = 3f;

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(autoDestoryTime);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("#### onCollisionEnter : "+collision.gameObject.tag);
        if (collision.gameObject.tag.Equals("Ground"))
        {
            StopCoroutine(AutoDestroy());
            Destroy(gameObject);
        }
    }
}
