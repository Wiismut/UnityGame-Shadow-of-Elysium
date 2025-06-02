using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpiritArr : MonoBehaviour {
    public GameObject Spirit;
    private Items items;
    public GameObject player1;
    public GameObject player2;
    private GameObject activePlayer;
    private void Start()
    {
        Spirit.SetActive(false);
        SetActivePlayer();
    }

    private void Update()
    {
        if ((player1.activeInHierarchy && activePlayer != player1) ||
            (player2.activeInHierarchy && activePlayer != player2))
        {
            SetActivePlayer();
        }

        UpdateVisibility();
    }

    private void SetActivePlayer()
    {
        if (player1.activeInHierarchy)
        {
            activePlayer = player1;
        }
        else if (player2.activeInHierarchy)
        {
            activePlayer = player2;
        }
        else
        {
            activePlayer = null;
        }

        if (activePlayer != null)
        {
            items = activePlayer.GetComponent<Items>();
        }
        else
        {
            items = null;
        }
    }

    private void UpdateVisibility()
    {
        if (items == null) return;

        int itemCount = 0;
        foreach (bool hasItem in items.hasItems)
        {
            if (hasItem) itemCount++;
        }

        Spirit.SetActive(itemCount == 3);
    }
}
