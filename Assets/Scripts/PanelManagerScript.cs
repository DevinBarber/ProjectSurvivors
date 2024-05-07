using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManagerScript : MonoBehaviour
{

    [SerializeField] private GameObject skillTreePanel;

    // Start is called before the first frame update
    void Start()
    {
        skillTreePanel.SetActive(false);
    }

    public void OpenSkillTreePanel()
    {
        skillTreePanel.SetActive(true);
    }

    public void CloseSkillTreePanel()
    {
        skillTreePanel.SetActive(false);
    }
}