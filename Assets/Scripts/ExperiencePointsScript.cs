using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePointsScript : MonoBehaviour
{
    private float experienceValue = 20;
    public GameObject player;
    public LevelScalingScript lvlScale;
    private Vector3 playerPos;
    public bool playerCollected;
    [SerializeField] private float speed = 8f;
    private Action<ExperiencePointsScript> pickedUpXP;

    // Start is called before the first frame update
    void OnEnable()
    {
        playerCollected = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        if (playerCollected)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, speed * Time.deltaTime);
            if(this.transform.position == playerPos)
            {
                lvlScale.AddExperience(experienceValue);
                pickedUpXP(this);
            }
        }
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == player.gameObject)
        {
            playerCollected = true;

        }
    }
    */

    public void Init(Action<ExperiencePointsScript> _pickedUpXP)
    {
        pickedUpXP = _pickedUpXP;
    }
}