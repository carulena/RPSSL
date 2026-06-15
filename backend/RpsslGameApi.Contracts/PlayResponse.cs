namespace RpsslGameApi.Contracts;

public class PlayResponse
{
    public required string Results { get; set; }
    public required int Player {get; set;}
    public required int Computer {get; set;}
   
}