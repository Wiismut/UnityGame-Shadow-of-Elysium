using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogue {
    public class DialogStory : MonoBehaviour {
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
        public void Start()
        {
            if (_stories == null || _stories.Count == 0)
            {
                Debug.LogError("No stories available!");
                return;
            }

            _storiesDictionary = _stories.ToDictionary(key => key.Tag, element => element);
            if (_storiesDictionary.TryGetValue(_stories[0].Tag, out Story firstStory))
            {
                ChangedStory?.Invoke(firstStory);
            }
        }
        public void ChangeStory(string tag)
        {
            if (_storiesDictionary.TryGetValue(tag, out Story story))
            {
                ChangedStory?.Invoke(story);
            }
            else
            {
                Debug.LogError($"Story with tag {tag} not found!");
            }
        }        public void Reset()
        {
            ChangeStory(_stories[0].Tag);
        }
    }

}