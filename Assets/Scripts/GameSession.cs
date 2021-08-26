using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // ENCAPSULATION
    public static GameSession instance { get; private set; }

    private string _playerName = "UnknownPlayer";

    // ENCAPSULATION
    public string playerName {
        get => _playerName;
        set 
        {
            if (value.Length > 0)
            {
                _playerName = value;
            }
            else
            {
                Debug.LogError("Invalid player name");
            }
        }
    }
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
