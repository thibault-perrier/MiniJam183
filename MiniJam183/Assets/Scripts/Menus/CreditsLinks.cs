using UnityEngine;

public class CreditsLinks : MonoBehaviour
{
    public void OpenBrowser(string url)
    {
        Application.OpenURL(url);
    }
}
