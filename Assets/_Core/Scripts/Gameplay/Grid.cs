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
            Building.OnBuildingHold += OnBuildingHold;
            Builder.OnBuildingDestroyed += OnBuildingDestroyed;
        }

        private void OnDisable()
        {
            BuildingSelector.OnBuildingSelected -= OnBuildingSelected;
            GridManager.OnConstructed -= OnGridUpdated;
            Building.OnBuildingHold -= OnBuildingHold;
            Builder.OnBuildingDestroyed -= OnBuildingDestroyed;
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

        private void OnBuildingHold(Building _)
        {
            UpdateMeshMaterial(isOccupied ? occupiedMaterial : avaliableMaterial);
        }

        private void OnBuildingDestroyed() 
        {
            UpdateMeshMaterial(gridMaterial);
        }
    }
}