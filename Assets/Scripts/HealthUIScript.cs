using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIScript : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerScript player;
    List<HeartTypeScript> hearts = new List<HeartTypeScript>();

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHeartList();
        int heartsToMake = player.GetPlayerMaxHealth();

        for(int i = 0; i < heartsToMake; i++) 
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatus = Mathf.Clamp(player.GetPlayerCurrentHealth() - i, 0, 1);
            hearts[i].SetHeartImage((HeartStatus)heartStatus);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab, transform);
        HeartTypeScript heartComponent = newHeart.GetComponent<HeartTypeScript>();

        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHeartList()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HeartTypeScript>();
    }
}
