
// a perception is just an organ in a specific state
public class Perception
{
    Organ organ;
    int state;
    int strength;

    public Perception(Organ organ, int state, int st = 1)
    {
        this.organ = organ;
        this.state = state;
        this.strength = st;
    }

    public Organ Organ
    {
        get
        {
            return organ;
        }

        set
        {
            organ = value;
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

    public int Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }
}
