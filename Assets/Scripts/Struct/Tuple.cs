
public class Tuple<T, U> {

    public T Item1 { get; private set; }
    public U Item2 { get; private set; }

    public Tuple(T i1, U i2) {
        Item1 = i1;
        Item2 = i2;
    }

}
