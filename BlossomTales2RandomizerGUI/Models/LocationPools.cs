namespace BlossomTales2Randomizer.Models;

[Flags]
public enum LocationPools
{
    None = 0,
    TreasureChests = 1 << 0,
    KeyPieces = 1 << 1,
    Traders = 1 << 2,
    Shops = 1 << 3,
    FreeGifts = 1 << 4,
    Sidequests = 1 << 5,
    LongFetchQuests = 1 << 6,
    AnimalStatues = 1 << 7,
    MailDelivery = 1 << 8,
    AllExceptKeyPieces = ~KeyPieces,
}
