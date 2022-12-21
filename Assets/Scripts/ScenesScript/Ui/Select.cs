using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Collections;

// 0 = load 1 = save 2 = clear enum만들기
public class Select : MonoBehaviour
{
    public GameObject creat;
    public TextMeshProUGUI[] slotText;
    public TextMeshProUGUI newPlayerName;

    [SerializeField] private SaveSystem _saveSystem;

    [SerializeField] private VoidEventChannelSO _startNewGameEvent;
    [SerializeField] private SaveEventChannelSO _onSaveEvent;

    bool[] savefile = new bool[3];

    private void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단.
        for (int i = 0; i < 3; i++)
        {
            if(File.Exists(_saveSystem.path + $"/slot{i}.json"))
            {
                savefile[i] = true;
                _saveSystem.nowSlot = i;
                _onSaveEvent.RaiseEvent(0, i);
                slotText[i].text = _saveSystem.nowPlayer.name;
            }
            else
            {
                slotText[i].text = $"빈 슬롯 {i}";
            }
        }
        _onSaveEvent.RaiseEvent(2);
    }

    public void Slot(int number)
    {
        _saveSystem.nowSlot = number;
        if (savefile[number])
        {
            _onSaveEvent.RaiseEvent(0, _saveSystem.nowSlot);
            GoGame();
        }
        else
        {
            Creat();
        }
    }

    public void Creat()
    {
        creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!savefile[_saveSystem.nowSlot])
        {
            _saveSystem.nowPlayer.name = newPlayerName.text;
            _onSaveEvent.RaiseEvent(1, _saveSystem.nowSlot);
        }
        _startNewGameEvent.RaiseEvent();
    }
}
