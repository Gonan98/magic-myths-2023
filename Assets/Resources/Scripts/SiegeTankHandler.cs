using RTSEngine.EntityComponent;
using UnityEngine;

public class SiegeTankHandler : MonoBehaviour
{
    //[SerializeField] private GameObject MountingModel;
    //[SerializeField] private GameObject UnmountingModel;
    private UnitAttack unitAttack;
    private UnitMovement unitMovement;
    private bool flag;

    void Awake()
    {
        //MountingModel.SetActive(false);
        //UnmountingModel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        flag = true;
        unitAttack = GetComponentsInChildren<UnitAttack>()[1]; // Get the second UnitAttack Component
        unitMovement = GetComponentInChildren<UnitMovement>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (unitAttack.IsActive)
        {
            if (flag)
            {
                flag = false;
                unitMovement.SetActive(flag, true);
                //MountingModel.SetActive(true);
                //UnmountingModel.SetActive(false);
                Debug.Log("Tanque de asedio activado");
            }
        }
        else
        {
            if (!flag)
            {
                flag = true;
                unitMovement.SetActive(flag, true);
                //UnmountingModel.SetActive(true);
                //MountingModel.SetActive(false);
                Debug.Log("Tanque de asedio desactivado");
            }
        }
    }
}
