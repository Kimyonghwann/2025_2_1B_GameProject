using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    [Header("�� ����")]
    public bool isOpen = false;
    public Vector3 openPisition;
    public float openSpeeed = 2f;

    private Vector3 closedPosition;

    protected override void Start()
    {
        base.Start();                                           //���� ��� ���� ��ŸƮ �Լ��� �ѹ� ���� ��Ų��.
        objectName = "��";
        interactionText = "[E] �� ����";
        interactionType = InteractionType.Building;

        closedPosition = transform.position;
        openPisition = closedPosition + Vector3.right * 3f;                 //���������� 3���� �̵�
    }

    protected override void AccessBuilding()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            interactionText = "[E] �� �ݱ�";
            StartCoroutine(MoveDoor(closedPosition));
        }
        else
        {
            interactionText = "[E] �� ����";
            StartCoroutine(MoveDoor(openPisition));
        }
    }

    IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) >0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }
}
