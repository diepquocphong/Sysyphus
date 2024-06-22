using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SnapToItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;

    public HorizontalLayoutGroup horizontalLayoutGroup;

    public Text NameLabel;
    public string[] ItemNames;

    bool isSnapped;
    bool isDragging;

    public float snapForce;
    float snapSpeed;

    // Start is called before the first frame update
    void Start()
    {
        isSnapped = false;
        isDragging = false;
    }

    // Update is called once per frame
    void Update()
    {
        int currentItem = Mathf.RoundToInt(0 - contentPanel.localPosition.x / (sampleListItem.rect.width + horizontalLayoutGroup.spacing));
        Debug.Log(currentItem);

        if (scrollRect.velocity.magnitude < 200 && !isDragging)
        {
            scrollRect.velocity = Vector2.zero;
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + horizontalLayoutGroup.spacing)), snapSpeed),
                contentPanel.localPosition.y,
                contentPanel.localPosition.z);
            NameLabel.text = ItemNames[currentItem];
            if (contentPanel.localPosition.x == 0 - (currentItem * (sampleListItem.rect.width + horizontalLayoutGroup.spacing)))
            {
                isSnapped = true;
            }
        }

        if (scrollRect.velocity.magnitude > 200)
        {
            isSnapped = false;
            snapSpeed = 0;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        isSnapped = false;
        snapSpeed = 0;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}