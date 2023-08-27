using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogManager : MonoBehaviour
{
    [SerializeField] private GameObject m_DialogBox;
    [SerializeField] private Text m_DialogTxt;

    [SerializeField] private int LettersPerSecond;

    private int curLineDialog = 0;
    private bool isWait = true;
    private Dialog m_curDialog;

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }


    public void ShowDialog(Dialog dialog, string nameNpc)
    {
        if (isWait)
        {
            m_curDialog = dialog;

            if (curLineDialog >= m_curDialog.Lines.Count)
            {
                curLineDialog = 0;
                isWait = false;
                GamePlayManager.Instance.ShowDialogUI(false);

                if (nameNpc == "Travel")
                    GamePlayManager.Instance.ShowBtnBattle(true);

                StartCoroutine(WaitOneSec());
            }
            else
            {
                GamePlayManager.Instance.ShowDialogUI(true);
                StartCoroutine(TypeDialog(m_curDialog.Lines[curLineDialog]));
                ++curLineDialog;
            }
        }
    }

    private IEnumerator WaitOneSec()
    {
        for (int i = 0; i < 10; i++)
            yield return new WaitForSeconds(0.1f);
        isWait = true;
    }

    private IEnumerator TypeDialog (string line)
    {
        isWait = false;
        m_DialogTxt.text = "";
        foreach (char letter in line.ToCharArray())
        {
            m_DialogTxt.text += letter;
            yield return new WaitForSeconds(1f / LettersPerSecond);
        }
        isWait = true;
    }
    public void ResetCurLineDialog()
    {
        curLineDialog = 0;
        isWait = true;
        StopAllCoroutines();
    }

}
