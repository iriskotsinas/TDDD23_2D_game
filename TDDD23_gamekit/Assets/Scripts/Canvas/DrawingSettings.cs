using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    // Helper methods used to set drawing settings
    public class DrawingSettings : MonoBehaviour
    {
        public static bool isCursorOverUI = false;
        public float Transparency = 1f;

        public void SetMarkerColour(Color new_color)
        {
            Drawable.Pen_Colour = new_color;
        }

        public void SetMarkerWidth(int new_width)
        {
            Drawable.Pen_Width = new_width;
        }

        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
        }

        public void SetEraser()
        {
            Color c = Color.white;
            c.a = Transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();        
        }

        public void SetMarkerBlack()
        {
            Color c = Color.black;
            c.a = Transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();     
        }
    }
}