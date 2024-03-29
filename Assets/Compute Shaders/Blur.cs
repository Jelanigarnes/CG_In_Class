using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Blur : MonoBehaviour
{
    [SerializeField]
    private Material postprocessMaterial;
    [Range(1, 32)]
    public int integerRange = 1;

    [Range(1, 16)]
    public int iterations = 1;

    //method which is automatically called by unity after the camera is done rendering
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // RenderTexture r = RenderTexture.GetTemporary( //get a hold of a temporary texture
        // 	source.width, source.height, 0, source.format //no need for depth which is why the third parameter is 0, source format is for matching the camera settings.
        // );

        int width = source.width / integerRange;
        int height = source.height / integerRange;
        RenderTextureFormat format = source.format;
        RenderTexture[] textures = new RenderTexture[16];

        //RenderTexture r =
        //	RenderTexture.GetTemporary(width, height, 0, format);

        //RenderTexture currentDestination =
        //	RenderTexture.GetTemporary(width, height, 0, format);	

        RenderTexture currentDestination = textures[0] =
            RenderTexture.GetTemporary(width, height, 0, format);

        //Graphics.Blit(source, destination);//Copies source texture into destination render texture with a shader
        // Graphics.Blit(source, r);//Blit to a temporary destination
        // Graphics.Blit(r, destination);//Blit to the destination
        // RenderTexture.ReleaseTemporary(r);//To make it available for reuse, release it by invoking RenderTexture.ReleaseTemporary.
        Graphics.Blit(source, currentDestination);
        RenderTexture currentSource = currentDestination;
        Graphics.Blit(currentSource, destination);
        RenderTexture.ReleaseTemporary(currentSource);
        int i = 1;
        for (; i < iterations; i++)
        {
            width /= 2;
            height /= 2;
            currentDestination = textures[i] =
                RenderTexture.GetTemporary(width, height, 0, format);
            if (height < 2)
            {
                break;
            }
            currentDestination =
                RenderTexture.GetTemporary(width, height, 0, format);
            Graphics.Blit(currentSource, currentDestination);
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;
        }


        for (; i < iterations; i++)
        {
            Graphics.Blit(currentSource, currentDestination);
            //			RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;

        }

        for (i -= 2; i >= 0; i--)
        {
            currentDestination = textures[i];
            textures[i] = null;
            Graphics.Blit(currentSource, currentDestination);
            RenderTexture.ReleaseTemporary(currentSource);
            currentSource = currentDestination;
        }

        Graphics.Blit(currentSource, destination);
    }
}
