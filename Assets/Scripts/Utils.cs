using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
public class IntCounts
{
    public int Count
    {
        get;
	private set;
    }
    int _delta;
    public IntCounts(int delta)
    {
        _delta = delta;
    }
    public int Step()
    {
        return Count += _delta;
    }
}
public class FloatCounts
{
    public float Count
    {
        get;
        private set;
    }
    float _delta;
    float _max, _min;
    bool _hasMax;
    public FloatCounts(float delta)
    {
        _delta = delta;
        _hasMax = false;
    }
    public FloatCounts(float delta, float min ,float max)
    {
        _delta = delta;
        _hasMax = true;
        _min = min; _max = max;
    }
    public float Step()
    {
        var c= Count += _delta;
        if(_hasMax)return Mathf.Clamp(c, _min, _max);
        return c;
    }
}
public class Tuple<T, T2>
{
   // sta
}
static class Utils
{
  public static Vector3 addX(this Vector3 v,float x)
  {
    return new Vector3(v.x + x, v.y, v.z);
  }
    public static IEnumerable<T> Pop<T>(this IEnumerable<T> s)
    {
            var fi = true;
            foreach (var i in s)
            { if (fi) { fi = false; continue; } else yield return i; }
    }
    public static IEnumerable<T> CountDo<T>(this IEnumerable<T> s, Action<int> f)
    {
        f(s.Count());
        return s;
    }
    public static IEnumerable<T> ExceptMySelf<T>(this IEnumerable<T> s,T self)
    {
        foreach (var i in s) { if (!i.Equals( self)) yield return i; }
    }
    public static void DoubleDo<T>(this IEnumerable<T> s, Action<T, T> f)
    {
        var enums = s.GetEnumerator();
        while (enums.MoveNext())
        {
            var c = enums.Current;
            if(!enums.MoveNext())break;
            f(c, enums.Current);
        }
    }
    public static void Iter2<T, TT>(IEnumerable<T> s, IEnumerable<TT> ss, Action<T, TT> f)
    {
            var rest = ss;
            foreach (var i in s)
            {
                var ii = rest.FirstOrDefault();
                if (ii == null) break;
                rest = rest.Pop();
                f(i, ii);
            }
    }
    public static void ForEach<T>(this IEnumerable<T> s, Action<T> f)
    {
        foreach (var item in s)
        {
            f(item);
        }
    }
    
    public static IEnumerable<T> Do<T>(this IEnumerable<T> s, Action<T> f)
    {
        foreach (var item in s)
        {
            f(item);
            yield return (item);
        }
    }
    public static T Last< T>(this IEnumerable<T> s) 
    {
        return s.Last();
    }
    //public static T First<T>(this IEnumerable<T> s) { yield return s[0]; }
    public static CollideObjs collideWith(GameObject col)
    {
        CollideObjs o = CollideObjs.None;
        if (col.tag == "enemy") { o = CollideObjs.Enemy; }
        if (col.tag == "bullet") { o = CollideObjs.Bullet; }
        return o;
    }
   
}
