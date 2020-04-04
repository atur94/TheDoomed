using UnityEngine.EventSystems;


public partial class ItemSlot
{
    private void SlotEventsConfiguration()
    {
        EventTrigger ent = _itemSlotInstance.AddComponent<EventTrigger>();
        EventTrigger.Entry onDragBegin = new EventTrigger.Entry();
        EventTrigger.Entry onDragEnd = new EventTrigger.Entry();
        EventTrigger.Entry onMouseButtonRelease = new EventTrigger.Entry();

        onDragBegin.eventID = EventTriggerType.BeginDrag;
        onDragEnd.eventID = EventTriggerType.EndDrag;
        onMouseButtonRelease.eventID = EventTriggerType.Drop;

        onDragBegin.callback.AddListener(OnDragBegin);
        onDragEnd.callback.AddListener(OnDragEnd);
        onMouseButtonRelease.callback.AddListener(OnMouseRelease);

        ent.triggers.Add(onDragBegin);
        ent.triggers.Add(onDragEnd);
        ent.triggers.Add(onMouseButtonRelease);
    }

    private void OnMouseRelease(BaseEventData arg0)
    {
        if (_uiController == null) return;
        _uiController.ItemSlotEventHandler(this, EventTriggerType.PointerUp);
    }

    private void OnDragEnd(BaseEventData arg0)
    {
        if (_uiController == null) return;
        _uiController.ItemSlotEventHandler(this, EventTriggerType.EndDrag);
    }

    private void OnDragBegin(BaseEventData arg0)
    {
        if (_uiController == null) return;
        _uiController.ItemSlotEventHandler(this, EventTriggerType.BeginDrag);
    }
}