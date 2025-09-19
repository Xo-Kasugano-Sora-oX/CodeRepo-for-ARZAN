using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class HealthUI : MonoBehaviour {
    public GameObject maskPrefab;
    public Transform masksContainer;
    public Color normalColor = Color.white;
    public Color damagedColor = Color.red;
    public Color blueHealthColor = Color.cyan;
    
    private Image[] maskImages;
    private int currentHealth;
    
    public void Initialize(int maxHealth) {
        // 清除现有面具
        foreach(Transform child in masksContainer) {
            Destroy(child.gameObject);
        }
        
        // 创建新面具
        maskImages = new Image[maxHealth];
        for(int i = 0; i < maxHealth; i++) {
            GameObject mask = Instantiate(maskPrefab, masksContainer);
            maskImages[i] = mask.GetComponent<Image>();
        }
        
        currentHealth = maxHealth;
    }
    
    public void UpdateHealth(int health, bool isBlueHealth = false) {
        currentHealth = health;
        for(int i = 0; i < maskImages.Length; i++) {
            bool isActive = i < health;
            maskImages[i].gameObject.SetActive(isActive);
            
            if(isActive) {
                maskImages[i].color = isBlueHealth ? blueHealthColor : normalColor;
            }
        }
    }
    
    public IEnumerator FlashDamagedMask(int lostHealth) {
        for(int i = currentHealth; i < currentHealth + lostHealth; i++) {
            if(i >= 0 && i < maskImages.Length) {
                maskImages[i].gameObject.SetActive(true);
                maskImages[i].color = damagedColor;
            }
        }
        
        yield return new WaitForSeconds(0.2f);
        
        UpdateHealth(currentHealth);
    }
}