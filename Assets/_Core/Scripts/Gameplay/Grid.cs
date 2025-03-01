using Muchwood.Utils;
using UnityEngine;

namespace Muchwood
{
    public class Grid : MonoBehaviour
    {
        [field: SerializeField, ReadOnly] public Vector2Int Coordinate { get; private set; }
        public bool isOccupied;

        private MeshRenderer meshRenderer;
        [SerializeField] private Material gridMaterial;
        [SerializeField] private Material occupiedMaterial;
        [SerializeField] private Material avaliableMaterial;

        private void OnEnable()
        {
            BuildingSelector.OnBuildingSelected += OnBuildingSelected;
            GridManager.OnConstructed += OnGridUpdated;
        }

        private void OnDisable()
        {
            BuildingSelector.OnBuildingSelected -= OnBuildingSelected;
            GridManager.OnConstructed -= OnGridUpdated;
        }

        private void Awake()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        public void SetCoordinate(Vector2Int coordinate)
        {
            Coordinate = coordinate;
        }

        private void UpdateMeshMaterial(Material material)
        {
            meshRenderer.sharedMaterial = material;

        }

        private void OnBuildingSelected (int _) 
        {
            UpdateMeshMaterial(isOccupied ? occupiedMaterial : avaliableMaterial);
        }

        private void OnGridUpdated()
        {
            UpdateMeshMaterial(gridMaterial);
        }
    }
}