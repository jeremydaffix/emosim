
// an organ can be set in a specific state
// basically it's juste a name + that current state
public class Organ
{
    string name;
    int state;

    public Organ(string name, int state = 0)
    {
        this.name = name;
        this.state = state;
    }


    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public int State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
        }
    }
}
