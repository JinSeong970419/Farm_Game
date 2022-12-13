using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spliter : MonoBehaviour
{
    // 아이템을 몇개로 나눌지 결정하는 변수
    public uint itemSplitCount = 1;

    /// 아이템을 나눌 대상 슬롯
    private InventorySlot slotUI;
    private InventoryObject inventoryObj;
    private InventoryObject toolBarObj;

    private TMP_InputField inputField = null;

    public uint ItemSplitCount
    {
        private get => itemSplitCount;
        set
        {
            itemSplitCount = value;
            itemSplitCount = (uint)Mathf.Max(1, itemSplitCount);    // 최소값은 1이고 
            if (slotUI != null)
            {
                // 최대값은 슬롯에 들어있는 아이템 수 - 1
                itemSplitCount = (uint)Mathf.Min(itemSplitCount, slotUI.amount - 1);
            }
            inputField.text = itemSplitCount.ToString();            // 인풋 필드의 글자 변경
            inputField.pointSize = 40;
        }
    }

    private void Awake()
    {
        // UI들을 찾아서 이벤트 연결
        inputField = transform.GetChild(1).GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(OnInputChage);

        Button ok = transform.GetChild(2).GetComponent<Button>();
        ok.onClick.AddListener(SelectOK);

        Button cancel = transform.GetChild(3).GetComponent<Button>();
        cancel.onClick.AddListener(SelectCancel);

        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        inventoryObj = transform.parent.GetChild(1).GetComponentInChildren<InventoryInterface>()?.inventory;
        toolBarObj = transform.parent.GetChild(2).GetComponentInChildren<ToobarInterface>()?.inventory;
    }

    // InputField의 글자가 바뀔 때 실행
    void OnInputChage(string change)
    {
        int num;
        bool _Isnumber = int.TryParse(change, out num);
        if (_Isnumber)
            ItemSplitCount = uint.Parse(change); // 입력받은 문자를 숫자로 변경해서 저장
        else
            inputField.text = "";
    }

    // OK 버튼을 눌렀을 때 실행될 함수
    private void SelectOK()
    {
        // 아이템 나누기 시작 함수 실행
        if(inventoryObj != null)
        {
            slotUI.UpdateSlot(slotUI.item, slotUI.amount - (int)itemSplitCount);
            inventoryObj.GetEmptySlot().UpdateSlot(slotUI.item, (int)itemSplitCount);
        }
        else
        {
            slotUI.UpdateSlot(slotUI.item, slotUI.amount - (int)itemSplitCount);
            toolBarObj.GetEmptySlot().UpdateSlot(slotUI.item, (int)itemSplitCount);
        }
        Init();
    }

    // Cancel 버튼을 눌렀을 때 실행될 함수
    private void SelectCancel()
    {
        Init();
    }

    public void Show(InventorySlot slot)
    {
        gameObject.SetActive(true);
        GameManager.instance._SpliterUI = true;
        slotUI = slot;
    }

    private void Init()
    {
        slotUI = null;                      // 들어있던 값을 비우기
        inputField.text = "";
        GameManager.instance._SpliterUI = false;
        this.gameObject.SetActive(false);
    }
}
