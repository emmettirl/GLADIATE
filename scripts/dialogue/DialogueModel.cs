using System;

namespace TeicsoftSpectacleCards.scripts.dialogue;

public class DialogueModel
{
    public CharacterModel[] characters_present { get; set;}
    public LocationModel[] locations_present { get; set;}
    public ShotModel[] shotlist { get; set;}
    
    public  DialogueModel(CharacterModel[] charactersPresent, LocationModel[] locationsPresent, ShotModel[] shotlist)
    {
        this.characters_present = charactersPresent;
        this.locations_present = locationsPresent;
        this.shotlist = shotlist;
    }
    
    public override string ToString()
    {
        return $"{nameof(characters_present)}: {characters_present}, {nameof(locations_present)}: {locations_present}, {nameof(shotlist)}: {shotlist}";
    }
}
