using UnityEngine;

public static class MouseData
{
    public static GameObject tempItemBeingDragged;    // 드레그 중 아이템 이미지
    public static InventorySlot slotHoveredOver;      // 시작 슬롯 정보
    public static InventorySlot interfaceMouseIsOver; // 도착 슬롯 정보
    public static UIInventoryItem selectBar;          // 선택한 Toolbar 아이템 정보 확인
}
