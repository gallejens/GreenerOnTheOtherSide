using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class SelectMenu : MonoBehaviour
{
    public static SelectMenu Instance { get; private set; }

    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private GameObject panel;

    private List<GameObject> LevelSelectButtons = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 1; i < LevelDataManager.levelDataList.Count; i++)
        {
            int temp = i;

            GameObject createdButton = (Instantiate(buttonPrefab, panel.transform) as GameObject);

            createdButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text += i;
            createdButton.GetComponentInChildren<Button>().onClick.AddListener(() => LevelButtonClicked(temp));
            createdButton.name = i.ToString();

            LevelSelectButtons.Add(createdButton);
        }
    }

    public void LevelButtonClicked(int number)
    {
        PlayingUI.Instance.Enable(number);

        Disable();
    }

    public void BackButtonPressed()
    {
        MainMenu.Instance.Enable();

        Disable();
    }

    public void Enable()
    {
        for (int i = 1; i < LevelDataManager.levelDataList.Count; i++)
        {
            if (!(SaveDataManager.LoadedSaveData.Contains(i) || SaveDataManager.LoadedSaveData.Contains(i - 1) || SaveDataManager.LoadedSaveData.Contains(i - 2)))
            {
                LevelSelectButtons[i - 1].GetComponent<Button>().interactable = false;
            }

            LevelSelectButtons[i - 1].transform.Find("Check").gameObject.SetActive(SaveDataManager.LoadedSaveData.Contains(i));

            Debug.Log(LevelDataManager.levelDataList.Count);
        }

        if (SaveDataManager.LoadedSaveData.Count == 0)
        {
            LevelSelectButtons[0].GetComponent<Button>().interactable = true;
            LevelSelectButtons[1].GetComponent<Button>().interactable = true;
        }

        GenericMethods.SetAllChildrenActive(gameObject, true);
    }

    public void Disable()
    {
        GenericMethods.SetAllChildrenActive(gameObject, false);

        for (int i = 1; i < LevelDataManager.levelDataList.Count; i++)
        {
            LevelSelectButtons[i - 1].GetComponent<Button>().interactable = true;
            LevelSelectButtons[i - 1].transform.Find("Check").gameObject.SetActive(false);
        }
    }
}
