namespace OCSFX.FMOD
{
    public enum AudioVolumeSetting
    {
        Master = 0,
        Sfx    = 1,
        Music  = 2,
        Ui     = 3,
        Amb    = 4,
        Voice  = 5
    }
    
    public enum AudioSurface
    {
        Concrete,
        Dirt,
        Grass,
        Gravel,
        Ice,
        Leaves,
        Metal,
        Snow,
        Stone,
        Tile,
        Water,
        Wood
    }

    public enum ReverbType
    {
        None,
        IntSmall,
        IntMed,
        IntLarge,
        ExtLarge
    }

    public enum MusicHorizontalParam
    {
        Int1,
        Int2,
        Int3,
        Int4,
        Int5,
        Transition
    };
}