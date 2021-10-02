using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private Dictionary<string, int> ItemsCollected = new Dictionary<string, int>();
    private enum Fruit
    {
        Banana,
        Kiwi,
        Pineapple,
        Apple,
        Cherrie,
        Melon,
        Orange,
        Strawberry
    }

    #pragma warning disable 0649

    [SerializeField]
    private Text countDisplay;
    [SerializeField]
    private AudioSource CollectAudio;

    #pragma warning restore 0649

    private void Start()
    {
        // TODO: count all collectables 
        // TODO: do something when all is collected
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Enum.TryParse(collision.gameObject.tag.Replace("Collectable", ""), out Fruit fruitCollided))
        {
            if (!ItemsCollected.ContainsKey(fruitCollided.ToString()))
                ItemsCollected[fruitCollided.ToString()] = 0;

            ItemsCollected[fruitCollided.ToString()]++;
            
            Destroy(collision.gameObject);
            countDisplay.text = string.Join("\n", ItemsCollected.Select(x => $"{x.Key}: {x.Value}"));

            CollectAudio.Play();
        }
        else
            Debug.LogWarning("Collide trigerred for object not handled: " + collision.name);
    }
}
