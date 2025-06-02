using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Dialogue {
    public class Pele_DialogStory : MonoBehaviour {
        [SerializeField] public List<Story> _stories;
        public Dictionary<string, Story> _storiesDictionary;
        public event Action<Story> ChangedStory;
        [System.Serializable]
        public class Story {
            public string Tag;
            public string TextKey;
            public List<Answer> Answers;
        }

        [System.Serializable]
        public class Answer {
            public string TextKey;
            public string ReposeTextKey;
        }


        public void Pele_ChangeStory(string tag)
        {
            if (_storiesDictionary == null)
            {
                return;
            }

            if (_storiesDictionary.TryGetValue(tag, out Story story))
            {
                ChangedStory?.Invoke(story);
            }
            else
            {
                Debug.LogError($"фраза {tag} не найдена");
            }
        }

        public void Reset()
        {
            if (_stories == null || _stories.Count == 0)
            {
                return;
            }

            Pele_ChangeStory(_stories[0].Tag);
        }

        public void Start()
        {
            if (_stories == null || _stories.Count == 0)
            {
                return;
            }

            _storiesDictionary = _stories.ToDictionary(key => key.Tag, element => element);

            Invoke(nameof(InvokeFirstStory), 0.1f);
        }

        private void InvokeFirstStory()
        {
            if (_storiesDictionary != null && _storiesDictionary.TryGetValue(_stories[0].Tag, out Story firstStory))
            {
                ChangedStory?.Invoke(firstStory);
            }
            else
            {
                Debug.LogError("первая фраза не найдена");
            }
        }

    }
}
