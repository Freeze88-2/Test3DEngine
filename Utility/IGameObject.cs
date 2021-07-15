using BTB.Rendering;

namespace BTB.Utility
{
    public interface IGameObject : ITransform
    {
        public virtual Mesh MeshVisuals => MeshVisuals;
        public virtual Pixel Visuals => Visuals;

        //public abstract void Update();
        public virtual void Update(float deltaTime) { }
    }
}