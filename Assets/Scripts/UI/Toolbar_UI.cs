using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slot_UI> toolbarSlots = new List<Slot_UI>();

    private Slot_UI selectedSlot;
    public static UnityAction<string> checAlphaNumbericKeys; // �κ��丮 �¿��� ���

    private void Awake()
    {
        checAlphaNumbericKeys = (string number) => { SelectSlot(number); };
    }

    private void Start()
    {
        SelectSlot("1"); // ���� ����
    }

    // ���õ� ����â ǥ��
    public void SelectSlot(string number)
    {
        int index = int.Parse(number);
        if(toolbarSlots.Count == 9)
        {
            if (selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = toolbarSlots[index-1];
            selectedSlot.SetHighlight(true);
            //Debug.Log("���õ� ���� : " + selectedSlot.name);
        }
    }
}
