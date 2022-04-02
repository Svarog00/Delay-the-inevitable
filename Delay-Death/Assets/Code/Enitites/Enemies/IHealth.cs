namespace Assets.Code.Enitites.Enemies
{
    public interface IHealth
    {
        public void Hurt(int damage);
        public void Reset();
    }
}