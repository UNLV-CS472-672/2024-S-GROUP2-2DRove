using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float ghostDelay;
    public float ghostDuration;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false;

    private void Start() {
        ghostDelaySeconds = ghostDelay;

    }

    private void Update() {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.rotation = transform.rotation;
                currentGhost.transform.localScale = transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, ghostDuration);
            }
        }
    }
}

