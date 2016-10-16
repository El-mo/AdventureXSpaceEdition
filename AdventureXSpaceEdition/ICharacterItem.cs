namespace AdventureXSpaceEdition
{
    public interface ICharacterItem
    {
        string ItemName { get; }
        void Use(Character character);
    }
}