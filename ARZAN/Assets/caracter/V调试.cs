using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VelocityDebugger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool showVelocity;
    [SerializeField] private TextMeshProUGUI debugText; // 引用 TMP 文本组件
    [SerializeField] private bool showAngularVelocity;
    [SerializeField] private Color highSpeedColor = Color.red;

    private Rigidbody rb; // 假设你要显示刚体的速度

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // 获取刚体组件（可选）
        if (debugText != null)
        {
            debugText.enabled = showVelocity; // 初始时根据 showVelocity 设置显示状态
        }
    }

    private void Update()
    {
        if (showVelocity && debugText != null)
        {
            // 示例：显示刚体速度（可根据需求修改）
            Vector2 velocity = rb != null ? rb.velocity : Vector2.zero;
            debugText.text = $"Velocity: {velocity:F2}";
        }
    }
}
