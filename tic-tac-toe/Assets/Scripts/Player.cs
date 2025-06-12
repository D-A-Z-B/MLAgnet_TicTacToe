using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RenderManager renderManager;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private TurnEnum myTurn;
    [SerializeField] private LayerMask whatIsSlot;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, whatIsSlot);

            if (hit.collider != null)
            {
                Slot slot = hit.collider.GetComponent<Slot>();
                if (slot != null && renderManager.CanPut(slot.Index) && turnManager.CurrentTurn == myTurn)
                {
                    renderManager.Put(slot.Index, myTurn);

                    turnManager.EndTurn();
                }
            }
        }
    }
}
