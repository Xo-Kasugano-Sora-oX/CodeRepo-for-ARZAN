using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class bullet : MonoBehaviour
{
    [SerializeField] float Movespeed = 10f;
    [SerializeField] Vector2 movebullet;

    void OnEnable()
    {
        StartCoroutine(Movebullet());
    }
    IEnumerator Movebullet()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(movebullet * Movespeed * Time.deltaTime);

            yield return null;
        }
    }
}
