namespace _01_Work.LCM._01.Scripts.BuildResources.Resource
{
    public interface IResource
    {
        public bool CanTakeResource();
        public void HitResource(int damage);
        public void DragResource();
    }
}