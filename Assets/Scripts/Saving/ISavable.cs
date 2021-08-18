namespace RPG.Saving
{
    public interface ISavable
    {
        public object CaptureState();
        public void RestoreState(object state);
    }
}
