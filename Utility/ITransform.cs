namespace BTB.Utility
{
    public interface ITransform
    {
        public Vector3 Position { get; }
        public Vector3 Rotation { get; }

        //public Vector3 Forward { get => Forward.Normalized; set { } }
        //public Vector3 Right { get => Vector3.CrossProduct(Forward.Normalized, new Vector3(0, 1, 0)).Normalized; }
        //public Vector3 Up { get => Vector3.CrossProduct(Forward, Right).Normalized; }
    }
}