using UnityEngine;
using UnityEngine.UI;

public class UI_ItemStatus : MonoBehaviour
{
    public ItemStatus targetItem; // referencer til det objekt, knappen repræsenterer
    public Button button;
    public Image iconImage;

    private Image thisImage;
    public Sprite ventSprite;
    public Sprite lightSprite;

    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();
       // if (iconImage == null) iconImage = transform.Find("Icon")?.GetComponent<Image>();
       thisImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        if (targetItem != null)
        {
            targetItem.OnActionEvent += HandleStatusChanged;

            // Sæt ikon
            SetIcon(targetItem.type);

            HandleStatusChanged(targetItem.LastKnownState); 
            // Brug targetItems seneste tilstand i stedet for altid grøn
            //SetButtonColor(ItemStatus.errorColor.green);
        }
    }
    //public void OnEnable() 
    //{
    //    HandleStatusChanged(targetItem.LastKnownState);
    //}
        

    private void OnDisable()
    {
        if (targetItem != null)
            targetItem.OnActionEvent -= HandleStatusChanged;
    }

    private void HandleStatusChanged(ItemStatus.errorColor state)
    {
        SetButtonColor(state);
    }

    private void SetButtonColor(ItemStatus.errorColor state)
    {
        if (button == null) return;

        Color color = Color.green;
        switch (state)
        {
            case ItemStatus.errorColor.green: color = Color.green; break;
            case ItemStatus.errorColor.yellow: color = Color.yellow; break;
            case ItemStatus.errorColor.red: color = Color.red; break;
            case ItemStatus.errorColor.blue: color = Color.blue; break;
            case ItemStatus.errorColor.grey: color = Color.grey; break;
        }

        
        thisImage.color = color;
    }



    public void SetIcon(ItemStatus.ItemType type)
    {
        if (thisImage == null) return;

        switch(type)
        {
            case ItemStatus.ItemType.Vent:
                thisImage.sprite = ventSprite;
                break;
            case ItemStatus.ItemType.Light:
                thisImage.sprite = lightSprite;
                break;
        }
    }
}
