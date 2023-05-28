using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            camera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camera.gameObject.SetActive(false);
        }
    }
}
