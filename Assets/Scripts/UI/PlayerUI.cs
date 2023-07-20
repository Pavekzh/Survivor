using UnityEngine;
using UnityEngine.UI;

public class PlayerUI:MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private TMPro.TMP_Text hp;
    [SerializeField] private TMPro.TMP_Text bulletsLeft;

    private Health health;
    private Gun gun;

    public void InitDependencies(Health playerHealth)
    {
        this.health = playerHealth;
        this.health.ChangedCurrentHalth += UpdateHealthBar;
    }

    public void InitDependencies(Gun playerGun)
    {
        this.gun = playerGun;
        this.gun.ChangedBulletsLeft += UpdateBulletsLeft;
    }

    private void UpdateBulletsLeft(int left)
    {
        this.bulletsLeft.text = left.ToString();
    }

    private void UpdateHealthBar(float hp)
    {
        this.hp.text = hp.ToString();

        float hpPerHeart = health.MaxHealth / hearts.Length;
        int filledHearts = (int)(hp / hpPerHeart);
        float incompleteHeartFill = (hp % hpPerHeart) / hpPerHeart;

        for(int i = 0; i < hearts.Length; i++)
        {
            if (i < filledHearts)
                hearts[i].fillAmount = 1;
            else if (i == filledHearts)
                hearts[i].fillAmount = incompleteHeartFill;
            else
                hearts[i].fillAmount = 0;
        }
    }
}