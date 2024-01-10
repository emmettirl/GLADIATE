namespace TeicsoftSpectacleCards.scripts.dialogue;

public class ShotModel
{
    public string id { get; set;}
    public string location_id { get; set;}
    
    public LineModel[] lines { get; set;}
    public OptionModel[] options { get; set;}
    
    
    public ShotModel(string id, string locationId, LineModel[] lines, OptionModel[] options)
    {
        this.id = id;
        this.location_id = locationId;
        this.lines = lines;
        this.options = options;
    }
    
    public override string ToString()
    {
        return $"{nameof(id)}: {id}, {nameof(location_id)}: {location_id}, {nameof(lines)}: {lines}, {nameof(options)}: {options}";
    }
}