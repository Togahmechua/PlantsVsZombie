using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    private PLanting plants;
    private GameManager gameManager;
    [Header("Cherries")]
    public Image cherries;
    public float cooldown1 = 5;
    bool isCooldown1 = false;
    public Button cherriesButton;

    [Header("Flowers")]
    public Image flowers;
    public float cooldown2 = 5;
    bool isCooldown2 = false;
    public Button flowersButton;

    [Header("OcChos")]
    public Image occhos;
    public float cooldown3 = 5;
    bool isCooldown3 = false;
    public Button occhosButton;

    [Header("Pea")]
    public Image pea;
    public float cooldown4 = 5;
    bool isCooldown4 = false;
    public Button peaButton;

    [Header("FirePea")]
    public Image firepea;
    public float cooldown5 = 5;
    bool isCooldown5 = false;
    public Button firepeaButton;

    [Header("Ice")]
    public Image ice;
    public float cooldown6 = 5;
    bool isCooldown6 = false;
    public Button iceButton;

    private void Start()
    {
        plants = GetComponent<PLanting>();
        gameManager = GetComponent<GameManager>();
        cherries.fillAmount = 1;
        flowers.fillAmount = 1;
        occhos.fillAmount = 1;
        pea.fillAmount = 1;
        firepea.fillAmount = 1;
        ice.fillAmount =1;

        cherriesButton.onClick.AddListener(StartCooldown1);
        flowersButton.onClick.AddListener(StartCooldown2);
        occhosButton.onClick.AddListener(StartCooldown3);
        peaButton.onClick.AddListener(StartCooldown4);
        firepeaButton.onClick.AddListener(StartCooldown5);
        iceButton.onClick.AddListener(StartCooldown6);
    }

    private void Update()
    {
        UpdateCooldowns();
    }

    private void StartCooldown1()
    {
        if (!isCooldown1)
        {
            isCooldown1 = true;
            cherries.fillAmount = 0;
            cherriesButton.interactable = false;
        }
    }

    private void StartCooldown2()
    {
        if (!isCooldown2)
        {
            isCooldown2 = true;
            flowers.fillAmount = 0;
            flowersButton.interactable = false; // Vô hiệu hóa nút flowersButton khi bắt đầu cooldown
        }
    }

    private void StartCooldown3()
    {
        if (!isCooldown3)
        {
            isCooldown3 = true;
            occhos.fillAmount = 0;
            occhosButton.interactable = false; // Vô hiệu hóa nút occhosButton khi bắt đầu cooldown
        }
    }

    private void StartCooldown4()
    {
        if (!isCooldown4)
        {
            isCooldown4 = true;
            pea.fillAmount = 0;
            peaButton.interactable = false; // Vô hiệu hóa nút peaButton khi bắt đầu cooldown
        }
    }

    private void StartCooldown5()
    {
        if (!isCooldown5)
        {
            isCooldown5 = true;
            firepea.fillAmount = 0;
            firepeaButton.interactable = false; // Vô hiệu hóa nút peaButton khi bắt đầu cooldown
        }
    }

    private void StartCooldown6()
    {
        if (!isCooldown6)
        {
            isCooldown6 = true;
            ice.fillAmount = 0;
            iceButton.interactable = false; // Vô hiệu hóa nút peaButton khi bắt đầu cooldown
        }
    }

    private void UpdateCooldowns()
    {
        UpdateCooldown(ref isCooldown1, cherries, cherriesButton, cooldown1);
        UpdateCooldown(ref isCooldown2, flowers, flowersButton, cooldown2);
        UpdateCooldown(ref isCooldown3, occhos, occhosButton, cooldown3);
        UpdateCooldown(ref isCooldown4, pea, peaButton, cooldown4);
        UpdateCooldown(ref isCooldown5, firepea, firepeaButton, cooldown5);
        UpdateCooldown(ref isCooldown6, ice, iceButton, cooldown6);
    }

    private void UpdateCooldown(ref bool isCooldown, Image fillImage, Button cooldownButton, float cooldown)
    {
        if (isCooldown)
        {
            fillImage.fillAmount += 1 / cooldown * Time.deltaTime;

            if (fillImage.fillAmount >= 1)
            {
                fillImage.fillAmount = 1;
                isCooldown = false;
                cooldownButton.interactable = true; // Cho phép người chơi nhấn vào nút khi cooldown kết thúc
            }
        }
    }
}
