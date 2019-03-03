namespace FineGameDesign.Utils
{
    public enum ItemType
    {
        Unknown,
        CarbonDioxide,
        CarbonDioxideInTroposphere,
        Negligible,
        CarbonDioxideExitingTroposphere

        // NOTE: The safest place to insert/delete a new type is at the end.
        // Otherwise, since serialized by index, the serialized will load at an offset.
    }
}
