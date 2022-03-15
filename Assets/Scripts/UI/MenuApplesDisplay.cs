using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MenuApplesDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI appleCountText;

        private void Start()
        {
            appleCountText.text = FindObjectOfType<SaveLoadManager>().AppleCount.ToString();
        }
    }
}