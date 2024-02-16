using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Изменяет параметры шейдеров, чтобы чанки искривлялись в дугу и уходили от центра
/// </summary>
public class LaneCurveController : MonoBehaviour
{
    [SerializeField] private List<Material> _curvedMaterials;
    [SerializeField] private float _curvingXSpeed = 1f;
    [SerializeField] private float _maxCurveXValue;
    [SerializeField] private float _minCurveXValue;

    [SerializeField] private float _maxCurveYValue;
    [SerializeField] private float _minCurveYValue;
    [SerializeField] private float _curvingYSpeed = 1f;

    private float _xValue  = 0.0f;
    private float _yValue  = 0.0f;
    private float _speedKoef = 0.000001f;

    private void Update()
    {
        foreach(var mat in _curvedMaterials)
        {
            mat.SetFloat("_Sideway_Strength",_xValue);
            _xValue += _curvingXSpeed * _speedKoef;
            if(_xValue > _maxCurveXValue || _xValue <= _minCurveXValue)
            {
                _curvingXSpeed *= -1;
            }

            mat.SetFloat("_Backward_Strength",_yValue);
            _yValue += _curvingYSpeed * _speedKoef;
            if(_yValue > _maxCurveYValue || _yValue <= _minCurveYValue)
            {
                _curvingYSpeed *= -1;
            }
        }
    }
}
