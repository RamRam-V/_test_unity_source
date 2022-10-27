#pragma strict

// Scroll main texture based on time
var scrollSpeed : float = 0.5;
var scrollTime : float = 1; // scroll duration in seconds
private var endScroll: float = 0;
private var offset: float = 0; // offset now is permanent instead of temporary

function Update() {

    
        endScroll = Time.time + scrollTime;
    
    if (Time.time < endScroll)
    {
        offset += Time.deltaTime * scrollSpeed; 
        // use offset modulo 1 to avoid a known problem with big offsets
        GetComponent.<Renderer>().material.mainTextureOffset = Vector2 (offset % 1, 0);
    }
}
