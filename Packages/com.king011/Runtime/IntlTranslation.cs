using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.king011
{
    public delegate void OnLanguageChange();

    /// <summary>
    /// 創建翻譯資源
    /// </summary>
    [CreateAssetMenu(fileName = "I18N", menuName = "ScriptableObjects/I18N", order = 1)]
    public class IntlTranslation : ScriptableObject
    {
        private OnLanguageChange _languageChange;
        public void AddListener(OnLanguageChange listener)
        {
            _languageChange += listener;
        }
        public void RemoveListener(OnLanguageChange listener)
        {
            _languageChange -= listener;
        }
        private void LanguageChange()
        {
            if (_languageChange != null)
            {
                _languageChange();
            }
        }
        [System.Serializable]
        public class Localization
        {
            [Label("支持的語言")]
            public SystemLanguage language;
            [Label("翻譯資源-json")]
            public TextAsset asset;
            [Label("顯示文本")]
            public string name = "";
            public bool valid
            {
                get
                {
                    return language != null && asset != null && name != "";
                }
            }
            public static Localization Create(SystemLanguage language, TextAsset asset, string name)
            {
                var result = new Localization();
                result.language = language;
                result.asset = asset;
                result.name = name;
                return result;
            }
        }
        public Localization[] supported;

        [System.Serializable]
        public class Mapping
        {
            [Label("映射結果")]
            public SystemLanguage language;
            [Label("被映射的值")]
            public SystemLanguage[] languages;
            public static Mapping Create(SystemLanguage language, SystemLanguage[] languages)
            {
                var result = new Mapping();
                result.language = language;
                result.languages = languages;
                return result;
            }
        }
        public Mapping[] mapping;

        [Label("默認語言")]
        public SystemLanguage defaultLanguage = SystemLanguage.English;

        /// <summary>
        /// 當前使用的語言，如果爲 null 則使用系統語言 或 默認語言
        /// </summary>
        private SystemLanguage _language;

        public SystemLanguage language
        {
            get
            {
                return _language;
            }
            set
            {
                if (_language == value)
                {
                    return;
                }
                if (value != null)
                {
                    _init();
                    if (!_supported.ContainsKey(value))
                    {
                        Debug.Log($"set language not supported: {value}");
                        return;
                    }
                }
                _language = value;
                LanguageChange();
            }
        }
        [Label("用於開發時重載配置")]
        public bool reset = false;
        protected IntlTranslation()
        {
            supported = new Localization[] {
                 Localization.Create(SystemLanguage.ChineseTraditional,null,"正體中文"),
                 Localization.Create(SystemLanguage.ChineseSimplified,null,"简体中文"),
                 Localization.Create(SystemLanguage.Japanese,null,"日本"),
                 Localization.Create(SystemLanguage.English,null,"English"),
            };
            mapping = new Mapping[] {
                Mapping.Create(SystemLanguage.ChineseTraditional,
                    new SystemLanguage[]{SystemLanguage.Chinese}
                ),
            };
        }

        private void _init(bool signal = false)
        {
            if (!reset)
            {
                return;
            }
            _supported.Clear();
            _keys.Clear();
            if (supported != null)
            {
                foreach (var item in supported)
                {
                    if (item.valid)
                    {
                        _supported[item.language] = item;
                    }
                }
                if (mapping != null)
                {
                    foreach (var m in mapping)
                    {
                        var val = _supported[m.language];
                        if (val == null)
                        {
                            continue;
                        }
                        var languages = m.languages;
                        if (languages != null)
                        {
                            foreach (var l in languages)
                            {
                                _supported[l] = val;
                            }
                        }
                    }
                }
            }
            reset = false;
            if (signal)
            {
                LanguageChange();
            }
        }
        private Dictionary<SystemLanguage, Localization> _supported = new Dictionary<SystemLanguage, Localization>();

        /// <summary>
        /// 存儲翻譯字典
        /// </summary>
        private Dictionary<SystemLanguage, Dictionary<string, string>> _keys = new Dictionary<SystemLanguage, Dictionary<string, string>>();
        private string GetKey(SystemLanguage language, TextAsset asset, string key)
        {
            var keys = _keys.GetValueOrDefault(language);
            if (keys == null)
            {
                try
                {
                    keys = JsonUtility.FromJson<Dictionary<string, string>>(asset.text);
                }
                catch (System.Exception e)
                {
                    Debug.Log($"IntlTranslation {language} FromJson error: {e}");
                    _supported.Remove(language);
                    return key;
                }

                _keys.Add(language, keys);
            }
            var val = keys.GetValueOrDefault(key);
            if (val != null)
            {
                return val;
            }
            Debug.Log($"IntlTranslation GetKey({language},{key}) not found");
            return key;
        }

        /// <summary>
        /// 獲取 key 的翻譯文本
        /// </summary>
        public string Get(string key)
        {
            _init(true);

            // settings
            var supported = _supported.GetValueOrDefault(_language);
            if (supported != null)
            {
                return GetKey(supported.language, supported.asset, key);
            }
            // system
            supported = _supported.GetValueOrDefault(Application.systemLanguage);
            if (supported != null)
            {
                return GetKey(supported.language, supported.asset, key);
            }
            // default
            if (defaultLanguage != null)
            {
                supported = _supported.GetValueOrDefault(defaultLanguage);
                if (supported != null)
                {
                    return GetKey(supported.language, supported.asset, key);
                }
            }

            // not found
            Debug.Log($"IntlTranslation Get({key}) not found");
            return key;
        }
    }
}