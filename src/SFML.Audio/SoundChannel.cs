namespace SFML.Audio
{
    ////////////////////////////////////////////////////////////
    /// <summary>
    /// Types of sound channels that can be read/written from sound buffers/files
    /// <para/>
    /// In multi-channel audio, each sound channel can be
    /// assigned a position. The position of the channel is
    /// used to determine where to place a sound when it
    /// is spatialised. Assigning an incorrect sound channel
    /// will result in multi-channel audio being positioned
    /// incorrectly when using spatialisation.
    /// </summary>
    ////////////////////////////////////////////////////////////
#pragma warning disable CS1591 // TODO: add documentation when available
    public enum SoundChannel
    {
        Unspecified,
        Mono,
        FrontLeft,
        FrontRight,
        FrontCenter,
        FrontLeftOfCenter,
        FrontRightOfCenter,
        LowFrequencyEffects,
        BackLeft,
        BackRight,
        BackCenter,
        SideLeft,
        SideRight,
        TopCenter,
        TopFrontLeft,
        TopFrontRight,
        TopFrontCenter,
        TopBackLeft,
        TopBackRight,
        TopBackCenter
    }
#pragma warning restore CS1591
}
