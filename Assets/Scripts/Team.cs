
public class Team
{
    public string name {get; set; }
    public int defence { get; set; }
    public int midfield { get; set; }
    public int attack { get; set; }

    public Team(string name, int defence, int midfield, int attack)
    {
        this.name = name;
        this.defence = defence;
        this.midfield = midfield;
        this.attack = attack;
    }
}
