using System.Collections;
using System.Collections.Generic;
using Atomic.Pathfinding.Core.Helpers;
using Atomic.Pathfinding.Core.Interfaces;
using TMPro;
using UnityEngine;

public class FieldTile : MonoBehaviour, IGridCell
{
    
    public bool IsWalkable
    {
        get => _isWalkable;
        private set => SetWalkable(value);
    }

    public bool IsOccupied { get; set; }
    public double Weight => _weight;

    public (int, int) Coordinates
    {
        get => (_x, _y);
        set
        {
            _x = value.X();
            _y = value.Y();
        }
    }

    [SerializeField] private int _x;
    [SerializeField] private int _y;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private bool _isWalkable = true;
    [SerializeField] private double _weight = 1;
    [SerializeField] private TextMeshProUGUI _stepNumberText;


    private static readonly int IsHovered = Shader.PropertyToID("_IsHovered");
    private static readonly int IsPathHighlighted = Shader.PropertyToID("_IsPathHighlighted");


    private void Awake()
    {
        IsWalkable = _isWalkable;
    }

    public void OnPathHighlight(int position)
    {
        if (position > 0)
        {
            _stepNumberText.gameObject.SetActive(true);
            _stepNumberText.text = position.ToString();
        }

        _renderer.material.SetInt(IsPathHighlighted, 1);
    }

    public void OnPathCleared()
    {
        _stepNumberText.gameObject.SetActive(false);
        _renderer.material.SetInt(IsPathHighlighted, 0);
    }

    public void OnHover()
    {
        _renderer.material.SetInt(IsHovered, 1);
    }

    public void OnHoverLeave()
    {
        _renderer.material.SetInt(IsHovered, 0);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.cyan;
        var position = transform.position + (transform.localScale * 0.5f);
        position.y -= 0.5f;
        UnityEditor.Handles.Label(position, $"{_x}:{_y}");
    }
#endif

    private void SetWalkable(bool value)
    {
        _isWalkable = value;
    }
}
