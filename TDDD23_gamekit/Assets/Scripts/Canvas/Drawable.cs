using System.Collections;
using UnityEngine;

using UnityEngine.Networking;
using System;
using System.Text;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.UI;

namespace FreeDraw
{

    public class DoodleImage
    {
        public string b64_image;
    }


    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]  // REQUIRES A COLLIDER2D to function
    // 1. Attach this to a read/write enabled sprite image
    // 2. Set the drawing_layers  to use in the raycast
    // 3. Attach a 2D collider (like a Box Collider 2D) to this sprite
    // 4. Hold down left mouse to draw on this texture!
    public class Drawable : MonoBehaviour
    {
        // PEN COLOUR
        public static Color Pen_Colour = Color.black;
        public static int Pen_Width = 10;

        public delegate void Brush_Function(Vector2 world_position);
        public Brush_Function current_brush;

        public LayerMask Drawing_Layers;

        public bool Reset_Canvas_On_Play = true;
        // The colour the canvas is reset to each time
        public Color Reset_Colour = new Color(0, 0, 0, 0);  // By default, reset the canvas to be transparent

        // Used to reference THIS specific file without making all methods static
        public static Drawable drawable;
        // MUST HAVE READ/WRITE enabled set in the file editor of Unity
        Sprite drawable_sprite;
        public Texture2D drawable_texture; // PUBLIC??

        public Text firstPrediction;
        public Text firstPredClass;

        public Text secondPrediction;
        public Text secondPredClass;

        public Text thirdPrediction;
        public Text thirdPredClass;

        public Text fourthPrediction;
        public Text fourthPredClass;

        public Text fifthPrediction;
        public Text fifthPredClass;

        public GameObject buttonGroup;



        private int whichPrediction;
        private bool predictionClicked;

        Vector2 previous_drag_position;
        Color[] clean_colours_array;
        Color transparent;
        Color32[] cur_colors;
        bool mouse_was_previously_held_down = false;
        bool no_drawing_on_current_drag = false;

        public RenderTexture RTexture;

        //API endpoint
        private readonly string baseURL = "http://localhost:8080/image";

        public List<GameObject> onScreenGameObjects = new List<GameObject>();
        




        public void BrushTemplate(Vector2 world_position)
        {
            // 1. Change world position to pixel coordinates
            Vector2 pixel_pos = WorldToPixelCoordinates(world_position);

            // 2. Make sure our variable for pixel array is updated in this frame
            cur_colors = drawable_texture.GetPixels32();

            ////////////////////////////////////////////////////////////////
            // FILL IN CODE BELOW HERE

            // Do we care about the user left clicking and dragging?
            // If you don't, simply set the below if statement to be:
            //if (true)

            // If you do care about dragging, use the below if/else structure
            if (previous_drag_position == Vector2.zero)
            {
                // THIS IS THE FIRST CLICK
                // FILL IN WHATEVER YOU WANT TO DO HERE
                // Maybe mark multiple pixels to colour?
                MarkPixelsToColour(pixel_pos, Pen_Width, Pen_Colour);
            }
            else
            {
                // THE USER IS DRAGGING
                // Should we do stuff between the previous mouse position and the current one?
                ColourBetween(previous_drag_position, pixel_pos, Pen_Width, Pen_Colour);
            }
            ////////////////////////////////////////////////////////////////

            // 3. Actually apply the changes we marked earlier
            // Done here to be more efficient
            ApplyMarkedPixelChanges();
            
            // 4. If dragging, update where we were previously
            previous_drag_position = pixel_pos;
        }

        // Default brush type. Has width and colour.
        // Pass in a point in WORLD coordinates
        // Changes the surrounding pixels of the world_point to the static pen_colour
        public void PenBrush(Vector2 world_point)
        {

            Debug.Log("CALLED PEN BRUSH");
            Vector2 pixel_pos = WorldToPixelCoordinates(world_point);

            cur_colors = drawable_texture.GetPixels32();

            if (previous_drag_position == Vector2.zero)
            {
                // If this is the first time we've ever dragged on this image, simply colour the pixels at our mouse position
                MarkPixelsToColour(pixel_pos, Pen_Width, Pen_Colour);
            }
            else
            {
                // Colour in a line from where we were on the last update call
                ColourBetween(previous_drag_position, pixel_pos, Pen_Width, Pen_Colour);
            }
            ApplyMarkedPixelChanges();

            //Debug.Log("Dimensions: " + pixelWidth + "," + pixelHeight + ". Units to pixels: " + unitsToPixels + ". Pixel pos: " + pixel_pos);
            previous_drag_position = pixel_pos;
        }


        // Helper method used by UI to set what brush the user wants
        // Create a new one for any new brushes you implement
        public void SetPenBrush()
        {
            // PenBrush is the NAME of the method we want to set as our current brush
            current_brush = PenBrush;
        }

        void Update()
        {
            //This will be called if the canvas is up, otherwise pause will be called from NewPlayer.cs
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
            }

            // Is the user holding down the left mouse button?
            if (firstPrediction.text == "")
            {
                buttonGroup.SetActive(false);
            } else {
                buttonGroup.SetActive(true);
            }

            bool mouse_held_down = Input.GetMouseButton(0);
            if (mouse_held_down && !no_drawing_on_current_drag)
            {

                // Convert mouse coordinates to world coordinates
                Vector2 mouse_world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Check if the current mouse position overlaps our image
                Collider2D hit = Physics2D.OverlapPoint(mouse_world_position, Drawing_Layers.value);

                Debug.Log(hit);

                if (hit != null && hit.transform != null)
                {
                    // We're over the texture we're drawing on!
                    // Use whatever function the current brush is
                    current_brush(mouse_world_position);
                    Save();
                }

                else
                {
                    // We're not over our destination texture
                    previous_drag_position = Vector2.zero;
                    if (!mouse_was_previously_held_down)
                    {
                        // This is a new drag where the user is left clicking off the canvas
                        // Ensure no drawing happens until a new drag is started
                        no_drawing_on_current_drag = true;
                    }
                }
            }
            else if (!mouse_held_down)
            {
                previous_drag_position = Vector2.zero;
                no_drawing_on_current_drag = false;
            }
            mouse_was_previously_held_down = mouse_held_down;

        }



        // Set the colour of pixels in a straight line from start_point all the way to end_point, to ensure everything inbetween is coloured
        public void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
        {
            // Get the distance from start to finish
            float distance = Vector2.Distance(start_point, end_point);
            Vector2 direction = (start_point - end_point).normalized;

            Vector2 cur_position = start_point;

            // Calculate how many times we should interpolate between start_point and end_point based on the amount of time that has passed since the last update
            float lerp_steps = 1 / distance;

            for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
            {
                cur_position = Vector2.Lerp(start_point, end_point, lerp);
                MarkPixelsToColour(cur_position, width, color);
            }
        }

        public void MarkPixelsToColour(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
        {
            // Figure out how many pixels we need to colour in each direction (x and y)
            int center_x = (int)center_pixel.x;
            int center_y = (int)center_pixel.y;
            //int extra_radius = Mathf.Min(0, pen_thickness - 2);

            for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
            {
                // Check if the X wraps around the image, so we don't draw pixels on the other side of the image
                if (x >= (int)drawable_sprite.rect.width || x < 0)
                    continue;

                for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
                {
                    MarkPixelToChange(x, y, color_of_pen);
                }
            }
        }

        public void MarkPixelToChange(int x, int y, Color color)
        {
            // Need to transform x and y coordinates to flat coordinates of array
            int array_pos = y * (int)drawable_sprite.rect.width + x;

            // Check if this is a valid position
            if (array_pos >= cur_colors.Length || array_pos < 0)
                return;

            cur_colors[array_pos] = color;
        }

        public void ApplyMarkedPixelChanges()
        {
            drawable_texture.SetPixels32(cur_colors);
            drawable_texture.Apply();
        }

        // Directly colours pixels. This method is slower than using MarkPixelsToColour then using ApplyMarkedPixelChanges
        // SetPixels32 is far faster than SetPixel
        // Colours both the center pixel, and a number of pixels around the center pixel based on pen_thickness (pen radius)
        public void ColourPixels(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
        {
            // Figure out how many pixels we need to colour in each direction (x and y)
            int center_x = (int)center_pixel.x;
            int center_y = (int)center_pixel.y;
            //int extra_radius = Mathf.Min(0, pen_thickness - 2);

            for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
            {
                for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
                {
                    drawable_texture.SetPixel(x, y, color_of_pen);
                }
            }

            drawable_texture.Apply();
        }


        public Vector2 WorldToPixelCoordinates(Vector2 world_position)
        {
            // Change coordinates to local coordinates of this image
            Vector3 local_pos = transform.InverseTransformPoint(world_position);

            // Change these to coordinates of pixels
            float pixelWidth = drawable_sprite.rect.width;
            float pixelHeight = drawable_sprite.rect.height;
            float unitsToPixels = pixelWidth / drawable_sprite.bounds.size.x * transform.localScale.x;

            // Need to center our coordinates
            float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
            float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

            // Round current mouse position to nearest pixel
            Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

            return pixel_pos;
        }

        // Changes every pixel to be the reset colour
        public void ResetCanvas()
        {
            firstPrediction.text = "";
            secondPrediction.text = "";
            thirdPrediction.text = "";
            fourthPrediction.text = "";
            fifthPrediction.text = "";

            drawable_texture.SetPixels(clean_colours_array);
            drawable_texture.Apply();
        }

        public void Cancel()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //Reset cursor
            ResetCanvas();
            DrawCanvas.CanvasIsOpen = true;
        }

        void Awake()
        {

            drawable = this;
            // DEFAULT BRUSH SET HERE
            current_brush = PenBrush;

            drawable_sprite = this.GetComponent<SpriteRenderer>().sprite;
            drawable_texture = drawable_sprite.texture;

            // Initialize clean pixels to use
            clean_colours_array = new Color[(int)drawable_sprite.rect.width * (int)drawable_sprite.rect.height];
            for (int x = 0; x < clean_colours_array.Length; x++)
                clean_colours_array[x] = Reset_Colour;

            // Should we reset our canvas image when we hit play in the editor?
            if (Reset_Canvas_On_Play)
                ResetCanvas();
        }

        public void Save()
        {
            StartCoroutine(CoSave());
        }

        private IEnumerator CoSave()
        {
            //wait for rendering
            yield return new WaitForEndOfFrame();
            Debug.Log(Application.dataPath + "/savedImage.png");

            //set active texture
            RenderTexture.active = RTexture;

            //convert rendering texture to texture2D
            var texture2D = new Texture2D(RTexture.width, RTexture.height);
            texture2D.ReadPixels(new Rect(0, 0, RTexture.width, RTexture.height), 0, 0);
            texture2D.Apply();

            //Encode to Base 64
            var data = texture2D.EncodeToPNG();
            string b64data = Convert.ToBase64String(data);

            // Create the JsonBody string
            DoodleImage jsonBodyImg = new DoodleImage();
            jsonBodyImg.b64_image = b64data;

            string JSON_Body = JsonUtility.ToJson(jsonBodyImg);
            //Debug.Log("JSONBODY" + JSON_Body);

            yield return StartCoroutine(CallPost(baseURL, JSON_Body));

            // ResetCanvas();
        }

        //void Start()
        //{
            
        //    m_someOtherScriptOnAnotherGameObject.Test();
        //}

        public IEnumerator CallPost(string url, string logindataJsonString)
        {
            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(logindataJsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log("Im not stuck");

            if (request.error != null)
            {
                Debug.Log("Error(Have you started API yet?): " + request.error);
            }
            else
            {
                JSONNode classification_result = JSON.Parse(request.downloadHandler.text);
                Debug.Log("ML GUESSED: " + classification_result["prediction"] + " with " + classification_result["confidence"] + " % confidence in group: " + classification_result["group"]);

                firstPrediction.text = classification_result["other_pred"][0]["prediction"];
                firstPredClass.text = classification_result["other_pred"][0]["group"];

                secondPrediction.text = classification_result["other_pred"][1]["prediction"];
                secondPredClass.text = classification_result["other_pred"][1]["group"];

                thirdPrediction.text = classification_result["other_pred"][2]["prediction"];
                thirdPredClass.text = classification_result["other_pred"][2]["group"];

                fourthPrediction.text = classification_result["other_pred"][3]["prediction"];
                fourthPredClass.text = classification_result["other_pred"][3]["group"];

                fifthPrediction.text = classification_result["other_pred"][4]["prediction"];
                fifthPredClass.text = classification_result["other_pred"][4]["group"];

                if (predictionClicked)
                {
                    string predictionName = classification_result["other_pred"][whichPrediction]["prediction"];
                    string groupName = classification_result["other_pred"][whichPrediction]["group"];

                    // predictionName = "car";
                    // groupName = "vehicles";

                    //Object Spawner Script
                    GameObject playerObj = GameObject.Find("Player");
                    ObjectSpawnerFromML m_someOtherScriptOnAnotherGameObject = GameObject.FindObjectOfType(typeof(ObjectSpawnerFromML)) as ObjectSpawnerFromML;
                    GameObject newObj = m_someOtherScriptOnAnotherGameObject.MakeObject(predictionName, playerObj, groupName);
                    onScreenGameObjects.Add(newObj);

                    // TODO: FIXED AMOUNT
                    if (onScreenGameObjects.Count > 5)
                    {
                        onScreenGameObjects.RemoveAt(onScreenGameObjects.Count - 1);
                    }

                    // This closes canvas. We need to do that after the request has been send back.
                    predictionClicked = false;
                    ResetCanvas();
                    DrawCanvas.CanvasIsOpen = true;
                }
            }
        }

        public void SetFirstPred()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //Reset cursor
            whichPrediction = 0;
            predictionClicked = true;
            Save();
        }

        public void SetSecondPred()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //Reset cursor
            whichPrediction = 1;
            predictionClicked = true;
            Save();
        }

        public void SetThirdPred()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //Reset cursor
            whichPrediction = 2;
            predictionClicked = true;
            Save();
        }

        public void SetFourthPred()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //Reset cursor
            whichPrediction = 3;
            predictionClicked = true;
            Save();
        }

        public void SetFifthPred()
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); //Reset cursor
            whichPrediction = 4;
            predictionClicked = true;
            Save();
        }
    }
}