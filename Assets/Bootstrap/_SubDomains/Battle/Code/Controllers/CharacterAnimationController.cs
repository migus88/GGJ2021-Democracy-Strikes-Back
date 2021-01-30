using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Spine.Unity;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _upAnimation;
    [SerializeField] private SkeletonAnimation _downAnimation;
    
    [Header("Animation Names")] 
    [SerializeField] private string _idleName;
    [SerializeField] private string _walkName;
    [SerializeField] private string _fallName;
    
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = _downAnimation.transform.localScale;
        _downAnimation.Initialize(true);
        _upAnimation.Initialize(true);
    }

    public void Turn(Direction direction)
    {
        Vector3 vector;
        
        if (direction.HasFlag(Direction.Left))
        {
            vector = new Vector3(-_originalScale.x, _originalScale.y, _originalScale.z);
        }
        else
        {
            vector = new Vector3(_originalScale.x, _originalScale.y, _originalScale.z);
        }
        
        _downAnimation.transform.localScale = vector;
        _upAnimation.transform.localScale = vector;

        if (direction.HasFlag(Direction.Up))
        {
            _upAnimation.gameObject.SetActive(true);
            _downAnimation.gameObject.SetActive(false);
        }
        else
        {
            _upAnimation.gameObject.SetActive(false);
            _downAnimation.gameObject.SetActive(true);
        }
    }

    public void Walk()
    {
        try
        {
            _downAnimation.AnimationState.SetAnimation(0, _walkName, true);
            _upAnimation.AnimationState.SetAnimation(0, _walkName, true);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    
    public void Stand()
    {
        //_upAnimation.AnimationName = _idleName;
        _downAnimation.AnimationName = _idleName;
        _downAnimation.AnimationState.SetAnimation(0, _idleName, true);
        Turn(Direction.Right | Direction.Down);
    }
    
    public void Fall()
    {
        _upAnimation.AnimationState.SetAnimation(0, _fallName, true);
        _downAnimation.AnimationState.SetAnimation(0, _fallName, true);
    }
    
    #region Tests

    [Button]
    public void LeftUp()
    {
        Turn(Direction.Left | Direction.Up);
    }
    
    [Button]
    public void LeftDown()
    {
        Turn(Direction.Left | Direction.Down);
    }

    [Button]
    public void RightUp()
    {
        Turn(Direction.Right | Direction.Up);
    }
    
    [Button]
    public void RightDown()
    {
        Turn(Direction.Right | Direction.Down);
    }
    
    #endregion
    

    [Flags]
    public enum Direction
    {
        None = 0,
        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8
    }
}
