using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SaveLoadManager : MonoBehaviour
{
    public static bool isSaving = true;

    public SaveDatas[] saveDatas { get; set; }

    public static SaveLoadManager Instance;

    void Awake()
    {
        Instance = this;
        saveDatas = SerializationManager.Load<SaveDatas[]>("saveData");
    }

    void Start()
    {
        // reset
        //SerializationManager.Save("saveData", new SaveDatas[9] {new SaveDatas(), new SaveDatas(), new SaveDatas(), new SaveDatas(), new SaveDatas(), new SaveDatas(), new SaveDatas(), new SaveDatas(), new SaveDatas()});

    }

    public void UnloadSaveLoadScene(bool isSavedLoaded)
    {
        SceneManager.UnloadSceneAsync("SaveLoadScene");

        if (isSavedLoaded)
        { // is actual saving/loading not just cancelling
            SerializationManager.Save("saveData", saveDatas);

            if (SceneManager.GetActiveScene().name == "MainGameScene" && !isSaving)
            {
                MainGameManager.Instance.SquareClick(-1);
                CameraManager.Instance.FocusCamera(Vector2.zero);
                MainGameManager.Instance.GenerateSquares();
                MainGameManager.Instance.GenerateEdges();
            }
            if (SceneManager.GetActiveScene().name == "MenuScene")
            {
                SceneManager.LoadSceneAsync("MainGameScene");
            }
        }
    }
}
