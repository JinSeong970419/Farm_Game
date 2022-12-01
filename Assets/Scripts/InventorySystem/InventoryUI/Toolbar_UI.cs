using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    private Slot_UI selectedSlot;
    public static UnityAction<int> checAlphaNumbericKeys; // 인벤토리 온오프 기능

    private void Awake()
    {
        checAlphaNumbericKeys = (int number) => { SelectSlot(number); };
    }

    private void Start()
    {
        SelectSlot(); // 추후 변경
    }

    // 선택된 슬롯창 표시
    public void SelectSlot(int number = 1)
    {
        if (toolbarSlots.Count == 9)
        {
            if (selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = toolbarSlots[number - 1];
            selectedSlot.SetHighlight(true);
            //Debug.Log("선택된 슬롯 : " + selectedSlot.name);
        }
    }
}