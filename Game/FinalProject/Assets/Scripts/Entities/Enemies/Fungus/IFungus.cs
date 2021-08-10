using System.Collections.Generic;
public interface IFungus
{
    public int SporesForState { get; }
    public List<State> States { get;}
}