
// a perception is an organ in a specific state
public class Perception
{
    Organ organ;
    int state;

    public Perception(Organ organ, int state)
    {
        this.organ = organ;
        this.state = state;
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
}
