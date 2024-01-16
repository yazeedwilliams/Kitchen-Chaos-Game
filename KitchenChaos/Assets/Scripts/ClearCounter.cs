using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public void Interact()
    {
        // clear counter knows if there is an object on it
        if (kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(this.kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.localPosition = Vector3.zero;

            // assign spawned kitchen object
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            // object knows where it is
            kitchenObject.SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
        
    }
}
