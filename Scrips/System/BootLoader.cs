using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    public DataAPIController dataAPIController;
    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(gameObject);
        ConfigManager.instance.InitConfig(LoadConfigDone);
    }
    private void LoadConfigDone()
    {
        Debug.Log(" load config done!");
        // int data 
        // load scene buffer
        dataAPIController.InitData(() =>
        {
            LoadScenceManager.instance.LoadSceneByIndex(1, () =>
            {
                Debug.Log(" load Buffer done!");
                ViewManager.instance.SwitchView(ViewIndex.HomeView);
            });
        });
    }
    // Update is called once per frame
    void Update()
    {

    }
}
