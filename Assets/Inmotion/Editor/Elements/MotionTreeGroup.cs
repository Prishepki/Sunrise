using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace InMotion.EditorOnly.GraphElements
{
    public class MotionTreeGroup : Group
    {
        public string ID { get; set; }
        public string OldTitle { get; set; }

        private Color _defaultBorderColor;
        private float _defaultBorderWidth;

        public MotionTreeGroup(string gtitle, Vector2 position)
        {
            ID = Guid.NewGuid().ToString();

            title = gtitle;
            OldTitle = gtitle;

            SetPosition(new Rect(position, Vector2.zero));

            _defaultBorderColor = contentContainer.style.borderBottomColor.value;
            _defaultBorderWidth = contentContainer.style.borderBottomWidth.value;
        }

        public void SetErrorStyle(Color color)
        {
            contentContainer.style.borderBottomColor = color;
            contentContainer.style.borderBottomWidth = 2f;
        }

        public void ResetStyle()
        {
            contentContainer.style.borderBottomColor = _defaultBorderColor;
            contentContainer.style.borderBottomWidth = _defaultBorderWidth;
        }
    }
}
