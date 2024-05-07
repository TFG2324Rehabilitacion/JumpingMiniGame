using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image panel;


    public void LoadUser()
    {
        if (RealmController.Instance.UserLogin())
        {
            GameManager.Instance.PlayerId = RealmController.Instance.PlayerId;
            GameManager.Instance.LoadConfig();

            infoText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            infoText.color = Color.green;
            infoText.text = "Nombre de usuario correcto. Empezando...";

            Invoke("PlayGame", 2f);
        }
        else
        {
            infoText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            infoText.color = Color.red;
            infoText.text = "Nombre de usuario incorrecto.";
        }
    }

    private void PlayGame()
    {
        SceneManager.LoadScene("Jumping");
    }
}
