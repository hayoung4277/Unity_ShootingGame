using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed = 10f;

    GameManager gm;

    private void Awake()
    {
        var findGo = GameObject.FindWithTag("GameController");
        gm = findGo.GetComponent<GameManager>();

        gm.onGameOver += StopScrolling;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void StopScrolling()
    {
        enabled = false;
    }
}
