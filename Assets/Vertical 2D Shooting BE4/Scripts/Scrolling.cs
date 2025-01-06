using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float speed = 10f;]
    
    GameManager gm;

    // Start is called before the first frame update
    private void Awake()
    {
        var findGo = GameObject.FindWithTag("GameController");
        gm = findGo.GetComponent<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void StopScrolling()
    {
        enabled = false;
    }
}
