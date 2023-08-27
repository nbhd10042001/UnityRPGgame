using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Vector3 m_SizeBox;
    [SerializeField] private Transform m_Foot;
    [SerializeField] private GameObject m_MessBox;
    [SerializeField] private string m_nameNPC;

    private BoxCollider2D m_boxCol;

    private void Start()
    {
        m_boxCol = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        m_boxCol.size = new Vector2(m_SizeBox.x, m_SizeBox.y);
        float posX = m_Foot.position.x - transform.position.x;
        float posY = m_Foot.position.y - transform.position.y;
        m_boxCol.offset = new Vector2(posX, posY);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(m_Foot.position, m_SizeBox);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Foot"))
        {
            m_MessBox.SetActive(true);
            GamePlayManager.Instance.ShowBoxNameNPC(true, m_nameNPC);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Foot"))
        {
            m_MessBox.SetActive(false);
            DialogManager.Instance.ResetCurLineDialog();
            GamePlayManager.Instance.ShowDialogUI(false);
            GamePlayManager.Instance.ShowBoxNameNPC(false, "");
        }
    }
}
