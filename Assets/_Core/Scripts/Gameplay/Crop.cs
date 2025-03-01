using System.Threading;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private CropData[] CropData;

    float timer = 0f;
    int currentPhase;

    private void Start()
    {
        currentPhase = 0;

        foreach (var cropData in CropData)
        {
            cropData.PhaseModel.gameObject.SetActive(false);
        }

        CropData[0].PhaseModel.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (currentPhase >= CropData.Length)
            return;

        timer += Time.deltaTime;

        if (timer > CropData[currentPhase].PhaseStart)
        {
            CropData[currentPhase].PhaseModel.gameObject.SetActive(true);
            currentPhase += 1;
        }    
    }

    public bool TryHarvest() 
    {
        if (currentPhase >= CropData.Length)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class CropData 
{
    public float PhaseStart;
    public Transform PhaseModel;
}
