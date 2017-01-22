using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PerformanceReport
{
    Texture[] m_textures;

    float[] m_cookedRate;

    float[] m_burnRate;

    bool[] m_exploded;

    EndType m_type;

    public float totalBurn
    {
        get
        {
            var _burn = 0.0f;
            for ( int i = 0; i < m_burnRate.Length; i++ )
                _burn += m_burnRate[ i ];

            return _burn;
        }
    }

    public float avgBurn { get { return totalBurn / m_burnRate.Length; } }

    public float totalDone
    {
        get
        {
            var _done = 0.0f;
            for ( int i = 0; i < m_cookedRate.Length; i++ )
                _done += m_cookedRate[ i ];

            return _done;
        }
    }

    public float avgDone { get { return totalDone / m_cookedRate.Length; } }

    public PerformanceReport( IEnumerable<Food> _foods, EndType _endType )
    {
        var _count = _foods.Count();
        m_textures = new Texture[ _count ];
        m_cookedRate = new float[ _count ];
        m_burnRate = new float[ _count ];
        m_exploded = new bool[ _count ];

        var _index = 0;
        foreach ( var _food in _foods )
        {
            m_cookedRate[ _index ] = _food.currentCookStatus / 100;
            m_burnRate[ _index ] = _food.currentBurnStatus / 100;
            m_exploded[ _index ] = _food.m_canExplode && _food.m_hasExploded;

            m_textures[ _index ] = ( _food.m_hasBeenBurnt && _food.m_hasExploded ) ?
                _food.m_burntTexture :
                _food.m_texture;

            _index++;
        }

        m_type = _endType;
    }

    public Result GetResult()
    {
        float _value = 0;

        switch ( m_type )
        {
            case EndType.AllDone:
                for ( int i = 0; i < m_cookedRate.Length; i++ )
                {
                    _value += m_cookedRate[ i ];
                    _value -= m_burnRate[ i ] * 0.5f;
                }

                _value /= m_cookedRate.Length;
                break;
            case EndType.TimeOut:
                for ( int i = 0; i < m_cookedRate.Length; i++ )
                {
                    _value += m_cookedRate[ i ];
                    _value -= m_burnRate[ i ] * 0.75f;
                }

                break;
            case EndType.AllBurnt:
            case EndType.Explosion:
            case EndType.HandBurnt:
                return Result.Burnt;
            default:
                break;
        }

        if ( _value > 0.8f )
            return Result.Gold;
        else if ( _value > 0.6f )
            return Result.Silver;
        else if ( _value > 0.4f )
            return Result.Bronze;
        else return Result.Burnt;
    }
}

public enum EndType
{
    AllDone,
    TimeOut,
    AllBurnt,
    Explosion,
    HandBurnt
}

public enum Result
{
    Gold,
    Silver,
    Bronze,
    Burnt
}