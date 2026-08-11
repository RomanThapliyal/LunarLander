using System.Collections.Generic;
using UnityEngine;
public class KeyUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private KeyIconUI iconPrefab;

    private Dictionary<KeyGiver, KeyIconUI> iconDictionary =
        new Dictionary<KeyGiver, KeyIconUI>();
    private void Start()
    {
        Lander.Instance.onKeyCollected += Lander_onKeyCollected;
        Lander.Instance.onKeyUsed += Lander_onKeyUsed;
    }
    private void Lander_onKeyCollected(object sender, KeyGiver key)
    {
        KeyIconUI icon = Instantiate(iconPrefab, container);

        icon.SetIcon(key.GetHUDSprite());

        iconDictionary.Add(key, icon);
    }
    private void Lander_onKeyUsed(object sender, KeyGiver key)
    {
        if (!iconDictionary.ContainsKey(key))
            return;

        Destroy(iconDictionary[key].gameObject);

        iconDictionary.Remove(key);
    }
}