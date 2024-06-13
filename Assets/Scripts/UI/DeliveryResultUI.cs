using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failedSprite;
    [SerializeField] private Transform lookToCameraTransform;
    [SerializeField] private DeliveryCounter deliveryCounter;

    private Animator animator;

    private void Awake()
    {
        lookToCameraTransform.forward = Camera.main.transform.forward;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    } 
    
    public void OnRecipeSuccess()
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";      
    }
    
    public void OnRecipeFaild()
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "DELIVERY\nFAILED";
    }
}
