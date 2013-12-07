using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;
namespace Assets.Scripts.Editor
{
    static class RefClass
    {
        private static Type[] UnityViewableType
        {
            get
            {
                return
                    new[] { typeof(float),typeof(double), typeof(string), typeof(int) };
            }
        }
        public static bool WatchableType(MemberInfo m)
        {
            var prop = m as PropertyInfo;
            if (prop == null) return false;
            return UnityViewableType.Any(t => t == prop.PropertyType);
        }
        public static T As<T>(GameObject obj, Vector3 pos) where T : Component
        {
            var o = GameObject.Instantiate(obj, pos, Quaternion.identity) as GameObject;
            return o.GetComponent<T>();
        }
        public static IEnumerable< MemberInfo > AllValueInClass<T>()where T:class
        {//あるクラスの値すべての中からエディタで扱える型のをすべて帰す
          //t.FindMembers(MemberTypes.Field|MemberTypes.Property,BindingFlags.Public,
          //  f=>true,
            return typeof(T).GetMembers().Where(WatchableType);
        }
    }
}
