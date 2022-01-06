using UnityEngine;

namespace com.king011
{
    /// <summary>
    /// 使字段在 Inspector 中顯示自定義名稱
    /// </summary>
    public class LabelAttribute : PropertyAttribute
    {
        public string name;

        /// <summary>
        /// 使字段在 Inspector 中顯示自定義名稱
        /// </summary>
        /// <param name="name">自定義名稱</param>
        public LabelAttribute(string name)
        {
            this.name = name;
        }
    }
}

