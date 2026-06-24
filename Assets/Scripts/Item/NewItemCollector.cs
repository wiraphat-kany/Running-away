using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class NewItemCollector : MonoBehaviour
{
    [SerializeField] private string[] items = { "SpeedBoost",  "EnergyDrink", "Shield"};
    private string currentItem;
    

    private PlayerController pc;
    private StaminaController sc;
    
    [SerializeField] private Text itemText;
    
    [SerializeField] private GameObject shieldPicture;
    public float shieldTimer = 0f;
    
    
    private void Start()
    {
        pc = GetComponent<PlayerController>();
        sc = GetComponent<StaminaController>();
        
        shieldPicture.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            // Get a random item from the items array
            currentItem = items[Random.Range(0, items.Length)];

            // Print the item that was collected
            Debug.Log($"Collected {currentItem}");
            
            // Display the name of the collected item on the screen
            itemText.text = $"{currentItem}";

            // Disable the item object so it can't be collected again
            other.gameObject.SetActive(false);
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !string.IsNullOrEmpty(currentItem))
        { 
            UseItem();
        }
        
        if (shieldTimer > 0f)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0f)
            {
                Debug.Log("Shield effect worn off");
                shieldPicture.SetActive(false); // Deactivate the shield picture object
            }
        }
    }
    
    private void UseItem()
    {
        // Use the currently held item
        switch (currentItem)
        {
            case "SpeedBoost":
                Debug.Log("Used SpeedBoost");
                pc.SpeedBoost();
                break;
            
            case "EnergyDrink":
                Debug.Log("Used EnergyDrink");
                sc.playerStamina = 100;
                sc.UpdateStamina(1);
                sc.sliderCanvasGroup.alpha = 0;
                break;
                
            case "Shield":
                Debug.Log("Used Shield");
                shieldTimer = 3f; // Set the shield timer to 3 seconds
                Debug.Log("Used Shield");
                shieldPicture.SetActive(true); // Activate the shield picture object
                break;
            

            default:
                Debug.LogError($"Unknown item: {currentItem}");
                break;
        }

        // Clear the currently held item
        currentItem = null;
            
        // Clear the displayed item name
        itemText.text = "";
        
    }
    
}

