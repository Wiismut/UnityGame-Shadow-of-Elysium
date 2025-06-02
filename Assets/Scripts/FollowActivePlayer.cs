using UnityEngine;

public class FollowActivePlayer : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;

    private GameObject activePlayer;

    void Start()
    {
        
        if (player1.activeInHierarchy)
        {
            activePlayer = player1;
        }
        else if (player2.activeInHierarchy)
        {
            activePlayer = player2;
        }
    }

    void Update()
    {
        
        if (activePlayer == null || !activePlayer.activeInHierarchy)
        {
            if (player1.activeInHierarchy)
            {
                activePlayer = player1;
            }
            else if (player2.activeInHierarchy)
            {
                activePlayer = player2;
            }
        }

        if (activePlayer != null)
        {
            transform.position = new Vector3(activePlayer.transform.position.x, activePlayer.transform.position.y, transform.position.z);
        }
    }
}
