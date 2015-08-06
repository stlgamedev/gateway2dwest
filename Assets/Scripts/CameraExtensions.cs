using UnityEngine;
using System.Collections;

public static class CameraExtensions
{
    //******Orthographic Camera Only******//
    //This code was found from a unity answers webpage
    //http://answers.unity3d.com/questions/501893/calculating-2d-camera-bounds.html
    //It extends the camera object to contain bounds and extents for orthographic cameras.
    //This will be useful for limiting the camera to certain positions like the edges of a room.

    public static Vector2 BoundsMin(this Camera camera)
    {
        return (Vector2)camera.transform.position - camera.Extents();
    }

    public static Vector2 BoundsMax(this Camera camera)
    {
        return (Vector2)camera.transform.position + camera.Extents();
    }

    public static Vector2 Extents(this Camera camera)
    {
        if (camera.orthographic)
            return new Vector2(camera.orthographicSize * Screen.width / Screen.height, camera.orthographicSize);
        else
        {
            Debug.LogError("Camera is not orthographic!", camera);
            return new Vector2();
        }
    }
    //*****End of Orthographic Only*****//
}