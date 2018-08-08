using UnityEngine;
using System.Collections;

public class CycleBannerManager : MonoBehaviour 
{
    private Texture[] _tCycleBanners;
    //need to have cycle banners update based on the state of hte slot engine
    public Texture[] tCycleBanners
    {
        get
        {
            if(_tCycleBanners == null)
                _tCycleBanners = ReturnBanners();
            else if (_tCycleBanners.Length < 1 || _tCycleBanners != ReturnBanners())
            {
                _tCycleBanners = ReturnBanners();
            }
            return _tCycleBanners;
        }
    }
    public int iCycleBannerNumber = -1;
    [Range(1, 60)]
    public int iCycleOnDuration;
    [Range(1, 60)]
    public int iCycleOffDuration;

    [Range(0, 10)]
    public float fFadeLength;

    public bool bCycleEnabled = true;

    private bool bBannerObjectEnabled
    {
        get
        {
            if (GetComponent<GUITexture>().color.a < 1)
                return false;
            else
                return true;
        }
    }

    void OnEnable()
    {
        SetDefaultValues();
        StartCoroutine(CycleMessageBanners());
    }

    private void SetDefaultValues()
    {
        iCycleBannerNumber = 0;
        GetComponent<GUITexture>().color = new Color(GetComponent<GUITexture>().color.r, GetComponent<GUITexture>().color.g, GetComponent<GUITexture>().color.b, 0);
        GetComponent<GUITexture>().texture = tCycleBanners[iCycleBannerNumber];
        bCycleEnabled = true;
    }

    //To cycle banners have gameobject that is dedicated to showing banners fade the alpha of the material then switch when material alpha is at 1
    private IEnumerator IncrementCycleBanners()
    {
        if (iCycleBannerNumber < 0)
        {
            iCycleBannerNumber = 0;

        }
        iTween.ColorTo(gameObject, new Color(GetComponent<GUITexture>().color.r, GetComponent<GUITexture>().color.g, GetComponent<GUITexture>().color.b, 0), fFadeLength);
        yield return new WaitForSeconds(fFadeLength);

    }

    private void InterruptBannerCycle()
    {
        StopCoroutine("CycleBanners");
    }

    private Texture[] ReturnBanners()
    {
        Texture[] ReturnValue;
        ReturnValue = Resources.LoadAll<Texture>("Skins/" + SlotEngine._instance.eSkin.ToString() + "/"+StateManager.enCurrentMode.ToString()+"/Cycle Banners");
        ReturnValue = Resources.LoadAll<Texture>("Skins/" + SlotEngine._instance.eSkin.ToString() + "/BaseGame/Cycle Banners");
        return ReturnValue;
    }

    private GUITexture ReturnCycleBannerObject()
    {
        return GameObject.FindGameObjectWithTag("CycleBannerObject").GetComponent<GUITexture>();
    }

    private void SwitchCycleBannerTexture(Texture tTexture)
    {
        GetComponent<GUITexture>().texture = tTexture;
    }


    /*Main Function()
     * 
     */
    IEnumerator CycleMessageBanners()
    {
        //Increment CycleBanner if the banner is off
        while (bCycleEnabled)
        {
            if (!bBannerObjectEnabled)
            {
                IncrementCycleBanners();
                iTween.ColorTo(gameObject, new Color(GetComponent<GUITexture>().color.r, GetComponent<GUITexture>().color.g, GetComponent<GUITexture>().color.b, 1), fFadeLength);
                yield return new WaitForSeconds(fFadeLength);
                yield return new WaitForSeconds(iCycleOnDuration);
            }
            else
            {
                iTween.ColorTo(gameObject, new Color(GetComponent<GUITexture>().color.r, GetComponent<GUITexture>().color.g, GetComponent<GUITexture>().color.b, 0), fFadeLength);
                yield return new WaitForSeconds(fFadeLength);
                yield return new WaitForSeconds(iCycleOffDuration);
            }
        }
    }
}
