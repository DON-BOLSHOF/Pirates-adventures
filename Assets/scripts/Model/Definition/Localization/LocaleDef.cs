using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.scripts.Model.Definition.Localization
{
    [CreateAssetMenu(menuName = "Defs/LocaleDef", fileName ="Locale")]
    public class LocaleDef : ScriptableObject
    {
        [SerializeField] private string _url;
        [SerializeField] private List<LocaleItem> _localItems;

        private UnityWebRequest _request;

        public Dictionary<string, string> GetData()
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var locale in _localItems)
            {
                dictionary.Add(locale.Key, locale.Value);
            }

            return dictionary;
        }

        [ContextMenu("UpdateLocale")]
        public void UpdateLocale()
        {
            if (_localItems != null)
                _localItems.Clear();

            _request = UnityWebRequest.Get(_url);
            _request.SendWebRequest().completed += OnCompletedRequest;
        }

        private void OnCompletedRequest(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                var rows = _request.downloadHandler.text.Split('\n');

                foreach(var row in rows)
                    AddLocalItem(row);
            }
        }

        private void AddLocalItem(string row)
        {
            try
            {
                var parts = row.Split('\t');
                _localItems.Add(new LocaleItem { Key = parts[0], Value = parts[1]});
            }
            catch(Exception e)
            {
                Debug.LogError($"Cant parse row: {row}.\n {e} ");
            }
        }

        [Serializable]
        private class LocaleItem
        {
            public string Key;
            public string Value;
        }
    }
}
