using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [Header("��ȣ �ۿ� ����")]
    public string objectName = "������ ";
    public string interactionText = "[E] ��ȣ �ۿ�";
    public InteractionType interactionType = InteractionType.Item;

    [Header("���̶���Ʈ ����")]
    public Color highlightColor = Color.yellow;
    public float highlightIntensity = 1.5f;

    public Renderer objectRenderer;
    private Color originalColor;
    private bool isHighlighted = false;

    public enum InteractionType
    {
        Item,                                   //������ (����, ���� ��)
        Machine,                                //��� (����, ��ư ��)
        Building,                               //�ǹ� (��, ���� ��)
        NPC,                                    //NPC
        Cellectible                             //����ǰ
    }


    protected virtual void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if(objectRenderer !=null )
        {
            originalColor = objectRenderer.material.color;
        }
        gameObject.layer = 8;                                                   //(Layer 8 = Interactalbe) �������� �Ѵ�.
    }

    public virtual void OnPlayerEnter()
    {
        Debug.Log($"[{objectName}) ������");
        HighlightObject();
    }

    public virtual void OnPlayerExit()
    {
        Debug.Log($"[{objectName}] �������� ���");
        RemoveHighlight();
    }
   
    protected virtual void HighlightObject()                                    //���� �Լ��� ���̶���Ʈ ����
    {
        if (objectRenderer != null && !isHighlighted)
        {
            objectRenderer.material.color = highlightColor;
            objectRenderer.material.SetFloat("Emission", highlightIntensity);
            isHighlighted = true;
        }
    }

    protected virtual void RemoveHighlight()                                    //���� �Լ��� ���̶���Ʈ ���� ����
    {
        if (objectRenderer != null && !isHighlighted)
        {
            objectRenderer.material.color = originalColor;
            objectRenderer.material.SetFloat("Emission", 0f);
            isHighlighted = false;
        }
    }

    protected virtual void CollectItem()
    {
        Destroy(gameObject);
    }

    protected virtual void OperateMachine()
    {
        if(objectRenderer !=null)
        {
            objectRenderer.material.color = Color.green;
        }
    }

    protected virtual void AccessBuilding()                                 //���� ����
    {
        transform.Rotate(Vector3.up * 90f);                                 //�켱 ȸ�� �Ѵ�.                                 
    }

    protected virtual void TalkNPC()                                        //NPC�� ��ȭ
    {
        Debug.Log($"{objectName}�� ��ȭ�� �����մϴ�.");                    //�켱 ����� �α׸� �Ѵ�.
    }

    public virtual void Interact()
    {
        //��ȣ �ۿ� Ÿ�Կ� ���� �⺻ ����
        switch(interactionType)
        {
            case InteractionType.Item:
                CollectItem();
                break;
            case InteractionType.Machine:
                OperateMachine();
                break;
            case InteractionType.Building:
                AccessBuilding();
                break;
            case InteractionType.Cellectible:
                CollectItem();
                break;
            case InteractionType.NPC:
                TalkNPC();
                break;
        }
    }

    public string GetInteractionText()                      //UI�� ������ Text �۾� ���� �Լ�
    {
        return interactionText;
    }
}
