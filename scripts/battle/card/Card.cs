using Godot;

namespace TeicsoftSpectacleCards.scripts.battle.card;

public partial class Card
{
    public virtual Card Initialize(string id, bool targetRequired = true, int attack = 0, int defenseLower = 0,
        int defenseUpper = 0, int health = 0, int draw = 0, int discard = 0, int spectaclePoints = 0, string name = "",
        string description = "", string lore = "", string tooltip = "", string imagePath = "",
        string animationPath = "", string soundPath = "") {
        this.Id = id;
        this.TargetRequired = targetRequired;

        this.Attack = attack;
        this.DefenseLower = defenseLower;
        this.DefenseUpper = defenseUpper;
        this.Health = health;
        this.CardDraw = draw;
        this.Discard = discard;
        this.SpectaclePoints = spectaclePoints;

        this.CardName = name;
        this.Description = description;
        this.Lore = lore;
        this.Tooltip = tooltip;

        this.ImagePath = imagePath;
        this.AnimationPath = animationPath;
        this.SoundPath = soundPath;

        // todo this section is just for testing, remove later
        // Colour is a test feature, to help with debugging
        // setting targetRequired here is temporary, as datafiles do not track this information,
        // and there is not yet another means of setting this flag
        uint randint = GD.Randi() % 3;   
        switch (randint)
        {
            case 0:
                color = new Color(1, 1, 1);
                targetRequired = true;
                break;
            case 1:
                color = new Color(1, 0.5f, 0.5f);
                targetRequired = false;
                break;
            case 2:
                color = new Color(0, 0, 0);
                targetRequired = true;
                break;
        }
        return this;
    }
    
    public void Play(GameState gameState) {
        if (Attack != 0) {
            if (TargetRequired) { gameState.GetSelectedEnemy().Damage(Attack); } else {
                foreach (Enemy enemy in gameState.enemies) { enemy.Damage(Attack); }
            }
        }

        if (DefenseLower != 0) {
            // gameState.ModifyPlayerDefenseLower(DefenseLower);
        }

        if (DefenseUpper != 0) {
            // gameState.ModifyPlayerDefenseUpper(DefenseUpper);
        }

        if (Health != 0) {
            gameState.HealPlayer(Health);
        }

        if (CardDraw > 0) {
            gameState.Draw(CardDraw);
        }

        if (TargetRequired) { Effect(Id,gameState.GetSelectedEnemy(),gameState); } else {
                foreach (Enemy enemy in gameState.enemies) { Effect(Id,enemy,gameState); }
            }

        if (Discard > 0) {
            // swalsh TODO: Emit Event?
            // swalsh TODO: Choice Discard by default, I think, but still needs an interface etc.
            // gameState.DiscardCards(Health);
        }

        gameState.ComboCheck(this);
    }
    
    public Color color { get; set; }

    public string Id { get; set; } // card_id

    public bool TargetRequired { get; set; }
    public Enemy.ModifierEnum Modifier { get; set; }
    public Enemy.PositionEnum TargetPosition { get; set; }
    
    // main stats
    public int Attack { get; set; }
    public int DefenseLower { get; set; }
    public int DefenseUpper { get; set; }
    public int Health { get; set; }
    public int CardDraw { get; set; }
    public int Discard { get; set; }
    public int SpectaclePoints { get; set; }

    //text
    public string CardName { get; set; }
    public string Description { get; set; }
    public string Lore { get; set; }
    public string Tooltip { get; set; }

    //design
    public string ImagePath { get; set; } //path to image
    public string AnimationPath { get; set; } //path to animation
    public string SoundPath { get; set; } //path to sound
    
    
    public Card Clone()
    {
        Card card = new Card();

        card.Initialize(Id, TargetRequired, Attack, DefenseLower, DefenseUpper, Health, CardDraw, Discard, SpectaclePoints,
            CardName, Description, Lore, Tooltip, ImagePath, AnimationPath, SoundPath);
        card.color = new Color(this.color.R, this.color.G, this.color.B);
        return card;
    }
    
    public override string ToString()
    {
        return $"Card: {CardName} ({Id}), {Attack} attack, {DefenseLower}-{DefenseUpper} defense, {Health} health, {CardDraw} draw, {Discard} discard, {SpectaclePoints} spectacle points";
    }

    public void Effect(string id, Enemy enemy, GameState gameState){
        switch (id)
        {
            //card cases
            case "card_05":
                enemy.Stun(1);
                break;
            //combo cases
            case "combo_02":
                gameState.Multiplier *= 2;
                break;

            default:
                break;
        }
    }
}