using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    public const string key = "33";

    public static Action OnFirebaseInitialized;
    public static FirebaseApp app;


    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError($"Failed to initialize Firebase with {task.Exception}");
                return;
            }

            Debug.Log("Firebase initialized");
            app = FirebaseApp.Create();

            Save();
            OnFirebaseInitialized?.Invoke();
        });
    }

    private void Save()
    {
        var p = new Player()
        {
            name = "AHMET",
            score = 100
        };

        UploadScore(p);
    }

    public async Task UploadScore(Player player)
    {
        await FirebaseDatabase.DefaultInstance.GetReference(key).SetRawJsonValueAsync(JsonUtility.ToJson(player));
        Debug.Log("Saved");
    }
}

[Serializable]
public class Player
{
    public string name;
    public int score;
}